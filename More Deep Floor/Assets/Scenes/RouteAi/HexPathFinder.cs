using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.Loggers;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class HexPathFinder : MonoBehaviour
    {
        //#. 참조
        [SerializeField] private GameObject tileMother;
        [SerializeField] private GameObject entityMother;
        [SerializeField] private GameObject routeTileMother;

        [SerializeField ]private TextMeshProUGUI stdText;
        [SerializeField] private Tilemap tilemap;

        //#. 변수
        [SerializeField] private Vector3Int size;
        private List<Vector3> tileWorldLocations = new List<Vector3>();

        private HexTile[ , ] tiles;
        private HexNode[,] nodeArray;
        List<HexNode> openList = new List<HexNode>(), closedList = new List<HexNode>();
        
        private List<HexTile> hexTileList = new List<HexTile>();
        private Dictionary<HexTile, HexNode> nodeDic;

        private Stopwatch stopwatch = new Stopwatch();
        private long std = 0;
        private int n = 0;
        private long sum = 0;
        
        //#. 프로퍼티
        private bool isStart = false;




        private void Awake()
        {
            SetTile();
            
            nodeArray = new HexNode[tiles.GetLength(0), tiles.GetLength(1)];
            nodeDic = new Dictionary<HexTile, HexNode>();

            SetNode();
        }

        #region #. 타일 설정

        void SetTile()
        {
            tilemap.CompressBounds();
            size = tilemap.size;
            tiles = new HexTile[size.x, size.y];
            
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {   
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    tileWorldLocations.Add(place);
                }
            }
            
            routeTileMother.transform.EachChild((child) =>
            {
                hexTileList.Add(child.gameObject.GetComponent<HexTile>());
            });

            for (var i = 0; i < hexTileList.Count; i++)
            {
                hexTileList[i].transform.position = tileWorldLocations[i];
            }
            
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    tiles[x, y] = hexTileList[size.x * y + x];
                    tiles[x, y].SetIndex(new Vector2Int(x , y));
                }
            }
            
            SetNeighbors();
        }
        
        void SetNeighbors()
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    HexTile tile = tiles[x , y];
                    if ((y + size.y + 1) % 2 == 1)
                    {
                        //#. y + 1 : x - 1, x
                        if (y != size.y - 1)
                        {
                            if(x != 0) tile.AddNeighbor(tiles[x - 1, y + 1]);
                            tile.AddNeighbor(tiles[x, y + 1]);
                        }

                        //#. y : x - 1, x + 1 
                        if (y != 0)
                        {
                            if(x != 0) tile.AddNeighbor(tiles[x - 1, y - 1]);
                            tile.AddNeighbor(tiles[x, y - 1]);
                        }
                        
                        //#. y - 1 : x - 1 , x 
                        if(x != 0) tile.AddNeighbor(tiles[x - 1, y]);
                        if(x != size.x - 1) tile.AddNeighbor(tiles[x + 1, y]);
                    }
                    else
                    {
                        //#. y + 1 : x + 1, x
                        if (y != size.y - 1)
                        {
                            if(x != size.x - 1) tile.AddNeighbor(tiles[x + 1, y + 1]);
                            tile.AddNeighbor(tiles[x, y + 1]);
                        }

                        //#. y : x - 1, x + 1 
                        if (y != 0)
                        {
                            if(x != size.x - 1) tile.AddNeighbor(tiles[x + 1, y - 1]);
                            tile.AddNeighbor(tiles[x, y - 1]);
                        }
                        
                        //#. y - 1 : x + 1 , x 
                        if(x != 0) tile.AddNeighbor(tiles[x - 1, y]);
                        if(x != size.x - 1) tile.AddNeighbor(tiles[x + 1, y]);
                    }
                }
            }
        }

        void SetNode()
        {
            for (int i = 0 , y = 0; i < tileMother.transform.childCount; y++)
            {
                for (int x = 0; i < tileMother.transform.childCount && x < size.x; x++ , i++)
                {
                    nodeArray[x, y] = new HexNode(
                        tiles[x, y].isWall,
                        x,y,
                        tiles[x, y]);
                    
                    nodeDic[tiles[x, y]] = nodeArray[x, y];
                }
            }
            
            for (int i = 0 , y = 0; i < tileMother.transform.childCount; y++)
            {
                for (int x = 0; i < tileMother.transform.childCount && x < size.x; x++ , i++)
                {
                    foreach (var hexTile in tiles[x, y].neighbors)
                    {
                        nodeArray[x, y].SetNeighbors(nodeArray[hexTile.index.x, hexTile.index.y]);
                    }
                }
            }
        }


        #endregion

        public List<HexTile> PathFinding(Mover mover , Vector2Int targetPos, int mode = 0)
        {
            
            Debug.Log(mode);
            List<HexNode> finalNodeList = new List<HexNode>();
            HexNode startNode, targetNode, curNode;
            
            int sizeX = size.x , sizeY = size.y;
            if (ReferenceEquals(mover.currentTile , null))
            {
                CustomLogger.LogWarning("[PathFindingManager.PathFinding()] currentNode 가 존재하지 않음" , mover.gameObject);
            }
            Vector2Int startPos = mover.currentTile.index;
            Vector2Int bottomLeft = new Vector2Int(0, 0);
            Vector2Int topRight = new Vector2Int(sizeX - 1, sizeY - 1);
            

            if (mode == 0)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        nodeArray[i, j].isWall = 
                            tiles[i,j].isWall || (!ReferenceEquals(tiles[i,j].desOf , null) && tiles[i,j].desOf != mover);;
                    }
                }
            }
            else if (mode == 1)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        HexNode node = nodeArray[i, j];
                        HexTile tile = tiles[i,j];
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

            nodeArray[targetPos.x, targetPos.y].isWall = false;
            nodeArray[startPos.x, startPos.y].isWall = false;

            // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
            startNode = nodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
            targetNode = nodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

            openList.Clear();
            openList.Add(startNode);
            closedList.Clear();
            //FinalNodeList.Clear();

            
            while (openList.Count > 0)
            {
                //Debug.Log("while");
                // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
                curNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                    if (openList[i].F <= curNode.F && openList[i].H < curNode.H) curNode = openList[i];

                openList.Remove(curNode);
                closedList.Add(curNode);


                // 마지막
                if (curNode == targetNode)
                {
                    HexNode TargetCurNode = targetNode;
                    while (TargetCurNode != startNode)
                    {
                        finalNodeList.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.ParentNode;
                    }
                    finalNodeList.Add(startNode);
                    finalNodeList.Reverse();

                    return finalNodeList.Make((node) => node.hexTile);
                }

                for (int i = 0; i < curNode.neighbors.Count; i++)
                {
                    OpenListAdd(curNode.neighbors[i].x, curNode.neighbors[i].y);
                }

            }

            return new List<HexTile>();
            
            void OpenListAdd(int checkX, int checkY)
            {
                //Debug.Log("OpenListAdd");
                try
                {
                    if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !closedList.Contains(nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
                    {
                        // 대각선 허용시, 벽 사이로 통과 안됨
                        //if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                        // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
                        //if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                
                        // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
                        HexNode NeighborNode = nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
                        int MoveCost = 10;


                        // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
                        if (MoveCost < NeighborNode.G || !openList.Contains(NeighborNode))
                        {
                            NeighborNode.G = MoveCost;
                            NeighborNode.H = (Mathf.Abs(NeighborNode.x - targetNode.x) + Mathf.Abs(NeighborNode.y - targetNode.y)) * 10;
                            NeighborNode.ParentNode = curNode;

                            openList.Add(NeighborNode);
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

        public List<HexTile> PathFindingWithPerformanceTest(Mover mover , Vector2Int targetPos, int mode = 0)
        {
            n++;
            stopwatch.Reset();
            stopwatch.Start();

            var result = PathFinding(mover, targetPos, mode);

            sum += stopwatch.ElapsedTicks;
            std = sum / n;

            stdText.text = std + " ticks / " + n;
            return result;
        }


        /*public List<HexTile> PathFinding(
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
                        nodeArray[i, j].isWall = 
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
                        HexNode node = nodeArray[i, j];
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

            nodeArray[targetPos.x, targetPos.y].isWall = false;
            nodeArray[startPos.x, startPos.y].isWall = false;

            // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
            StartNode = nodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
            TargetNode = nodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

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
                    if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
                    {
                        // 대각선 허용시, 벽 사이로 통과 안됨
                        //if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                        // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
                        //if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                
                        // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
                        HexNode NeighborNode = nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
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
            }#1#

            sum += stopwatch.ElapsedTicks;
            std = sum / n;

            
            stdText.text = std + " ticks / " + n;
            return routes;
        }*/
    }
}

