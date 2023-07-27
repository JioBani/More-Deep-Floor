using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExtensionMethods;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class HexPathFinder : MonoBehaviour
    {
        //1. current tile 이 현재 위치를 반영해야함
        //2. current tile 이 반드시 존재해야함
        //3. 각 current tile은 서로 유일해야함
        
        public List<HexNode> FinalNodeList = new List<HexNode>();
        public bool allowDiagonal, dontCrossCorner;
        [SerializeField] private GameObject tileParent;
        public HexTile[ , ] tiles;
        public int tileX, tileY;
        HexNode[,] NodeArray;
        HexNode StartNode, TargetNode, CurNode;
        List<HexNode> OpenList = new List<HexNode>(), ClosedList = new List<HexNode>();
        private List<RouteDetectors> routeDetectorsList;
        private bool isStart = false;

        private List<Mover> entities;
        [SerializeField] private GameObject entityMother;
        private Stopwatch stopwatch = new Stopwatch();
        [SerializeField] private HexTileManager hexTileManager;
        private Dictionary<HexTile, HexNode> nodeDic;

        private long std = 0;
        private int n = 0;
        private long sum = 0;
        [SerializeField ]private TextMeshProUGUI stdText;

        private void Awake()
        {
            tiles = hexTileManager.tiles;
            entities = new List<Mover>();
            NodeArray = new HexNode[tiles.GetLength(0), tiles.GetLength(1)];
            nodeDic = new Dictionary<HexTile, HexNode>();
            
            entityMother.transform.EachChild((c) =>
            {
                entities.Add(c.GetComponent<Mover>());
            });

            for (int i = 0 , y = 0; i < tileParent.transform.childCount; y++)
            {
                for (int x = 0; i < tileParent.transform.childCount && x < tileX; x++ , i++)
                {
                    NodeArray[x, y] = new HexNode(
                        tiles[x, y].isWall,
                        x,y,
                        tiles[x, y]);
                    
                    nodeDic[tiles[x, y]] = NodeArray[x, y];
                }
            }
            
            for (int i = 0 , y = 0; i < tileParent.transform.childCount; y++)
            {
                for (int x = 0; i < tileParent.transform.childCount && x < tileX; x++ , i++)
                {
                    foreach (var hexTile in tiles[x, y].neighbors)
                    {
                        NodeArray[x, y].SetNeighbors(NodeArray[hexTile.index.x, hexTile.index.y]);
                    }
                }
            }
        }

        public void Start()
        {
            /*List<HexTile> hexTiles = PathFinding(null, tiles, new Vector2Int(0, 0), new Vector2Int(7, 3));
            //Debug.Log(hexTiles.Count);
            foreach (var hexTile in hexTiles)
            {
                //Debug.Log(hexTile.index);
                hexTile.isRoute = true;
            }*/

            foreach (var entity in entities)
            {
                entity.SetRoute();
            }
        }

        //해야할일
        // 벽 처리의 옵션화를 통한 실시간 반응 감지 : 함수에 mode 넣기
        public List<HexTile> PathFinding(
            Mover mover ,
            HexTile[,] _tiles , 
            Vector2Int startPos , 
            Vector2Int targetPos,
            int mode = 0
            )
        {
            int sizeX = _tiles.GetLength(0);
            int sizeY = _tiles.GetLength(1);
            Vector2Int bottomLeft = new Vector2Int(0, 0);
            Vector2Int topRight = new Vector2Int(_tiles.GetLength(0) - 1, _tiles.GetLength(1) - 1);


            if (mode == 0)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        NodeArray[i, j].isWall = 
                            _tiles[i,j].isWall || (!ReferenceEquals(_tiles[i,j].desOf , null) && _tiles[i,j].desOf != mover);;
                    }
                }
            }
            else if (mode == 1)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        HexNode node = NodeArray[i, j];
                        HexTile tile = _tiles[i,j];
                        int l = Math.Abs(mover.currentTile.index.x - i) + Math.Abs(mover.currentTile.index.y - j);
                        if (l <= 4)
                            node.isWall = tile.isWall;
                        if (l <= 2 &&  node.isWall == false)
                        {
                            node.isWall = !tile.desNotNeeded && !ReferenceEquals(tile.desOf , null) && tile.desOf != mover;
                        }
                    }
                }
            }
            else if (mode == 2)
            {
                for (var i = 0; i < mover.currentTile.neighbors.Count; i++)
                {
                    HexTile neighbor = mover.currentTile.neighbors[i];
                    nodeDic[neighbor].isWall = 
                        neighbor.isWall || (!ReferenceEquals(neighbor.desOf , null) && neighbor.desOf != mover);
                }
            }
            
            
            
            /*for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    NodeArray[i, j].isWall = 
                            _tiles[i,j].isWall || (!ReferenceEquals(_tiles[i,j].desOf , null) && _tiles[i,j].desOf != entity);;
                }
            }*/
            
            /*for (var i = 0; i < entity.currentTile.neighbors.Count; i++)
            {
                HexTile neighbor = entity.currentTile.neighbors[i];
                nodeDic[neighbor].isWall = 
                    neighbor.isWall || (!ReferenceEquals(neighbor.desOf , null) && neighbor.desOf != entity);
            }*/

            NodeArray[targetPos.x, targetPos.y].isWall = false;
            NodeArray[startPos.x, startPos.y].isWall = false;

            // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
            StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
            TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

            OpenList.Clear();
            OpenList.Add(StartNode);
            ClosedList.Clear();
            FinalNodeList.Clear();

            
            while (OpenList.Count > 0)
            {
                //Debug.Log("while");
                // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
                CurNode = OpenList[0];
                for (int i = 1; i < OpenList.Count; i++)
                    if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

                OpenList.Remove(CurNode);
                ClosedList.Add(CurNode);


                // 마지막
                if (CurNode == TargetNode)
                {
                    HexNode TargetCurNode = TargetNode;
                    while (TargetCurNode != StartNode)
                    {
                        FinalNodeList.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.ParentNode;
                    }
                    FinalNodeList.Add(StartNode);
                    FinalNodeList.Reverse();

                    //Debug.Log("Last");
                    //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                    return FinalNodeList.Make((node) => tiles[node.x, node.y]);
                }

                for (int i = 0; i < CurNode.neighbors.Count; i++)
                {
                    //Debug.Log(i);
                    OpenListAdd(CurNode.neighbors[i].x, CurNode.neighbors[i].y);
                }
                
                
                
            }

            return new List<HexTile>();
            
            void OpenListAdd(int checkX, int checkY)
            {
                //Debug.Log("OpenListAdd");
                try
                {
                    if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
                    {
                        // 대각선 허용시, 벽 사이로 통과 안됨
                        //if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                        // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
                        //if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                
                        // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
                        HexNode NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
                        int MoveCost = 10;


                        // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
                        if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
                        {
                            NeighborNode.G = MoveCost;
                            NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                            NeighborNode.ParentNode = CurNode;

                            OpenList.Add(NeighborNode);
                        }
                    }

                }
                catch (Exception e)
                {
                    Debug.LogWarning($"[Exception] checkX : {checkX} , checkY : {checkY} , bottomLeft.y : {bottomLeft.y} , topRight.y : {topRight.y}");
                }
                // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
            }
        }

        public List<HexTile> GetRoute(Mover mover , HexTile currentTile , Vector2Int end,int mode)
        {
            n++;
            stopwatch.Reset();
            stopwatch.Start();
            
            if (ReferenceEquals(currentTile,null)) return new List<HexTile>();
            
            List<HexTile> routes = PathFinding(
                mover,
                tiles , 
                currentTile.index, 
                end
                ,mode
                );
            
            /*for (var i = 0; i < routes.Count; i++)
            {
                routes[i].SetRoute();
            }*/

            sum += stopwatch.ElapsedTicks;
            std = sum / n;

            
            stdText.text = std + " ticks / " + n;
            return routes;
        }
    }
}

