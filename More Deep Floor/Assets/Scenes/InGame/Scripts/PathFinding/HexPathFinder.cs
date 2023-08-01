using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.InGame;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace LNK.MoreDeepFloor.InGame.PathFinding
{
    public class HexPathFinder : MonoBehaviour
    {
        //#. 참조
        [SerializeField] private GameObject tileMother;
        [SerializeField] private GameObject routeTileMother;

        [SerializeField] private TextMeshProUGUI stdText;
        [SerializeField] private Tilemap tilemap;

        //#. 변수
        [SerializeField] private Vector3Int size;
        [SerializeField] private int useTileNums;
        [SerializeField] private List<Vector3> tileWorldLocations = new List<Vector3>();

        private RouteTile[,] tiles;
        private HexNode[,] nodeArray;
        List<HexNode> openList = new List<HexNode>(), closedList = new List<HexNode>();

        private List<RouteTile> hexTileList = new List<RouteTile>();
        private Dictionary<RouteTile, HexNode> nodeDic;

        private Stopwatch stopwatch = new Stopwatch();
        private long std = 0;
        private int n = 0;
        private long sum = 0;

        //#. 프로퍼티
        private bool isStart = false;




        private void Awake()
        {
            SetTile();

            nodeArray = new HexNode[size.x, size.y];
            nodeDic = new Dictionary<RouteTile, HexNode>();

            SetNode();
        }

        #region #. 타일 설정

        public TileBase[] allTiles;
        public List<Vector3> availablePlaces;
        void Temp()
        {
            /*tilemap.CompressBounds();
            
            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                if (!tilemap.HasTile(position))
                {
                    continue;
                }

                else if(position == activeTileCoordinate)
                {
                    Sprite otherTiles = tilemap.GetSprite(position);
                    Debug.Log("Active tile is at " + position + " and is " + otherTiles.name);
                }

                else
                {
                    Sprite otherTile = tilemap.GetSprite(position);

                    if(otherTile.name == tilemap.name)
                    {
                        tilemap.SetTile(position, null);
                    }

                }
            }*/
        }

        void SetTile()
        {
            Temp();
            
            hexTileList = new List<RouteTile>();
            
            tilemap.CompressBounds();
            size = tilemap.size;
            tiles = new RouteTile[size.x, size.y];
            useTileNums = size.x * size.y;

            BoundsInt tmpSize = tilemap.cellBounds;
            allTiles = tilemap.GetTilesBlock(tmpSize);
            
            
            
            //#. 타일맵 타일 위치 리스트 만들기
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
                hexTileList.Add(child.gameObject.GetComponent<RouteTile>());
            });

            for (var i = 0; i < hexTileList.Count; i++)
            {
                if (i < useTileNums)
                {
                    hexTileList[i].transform.position = tileWorldLocations[i];
                    hexTileList[i].gameObject.SetActive(true);
                }
                else
                {
                    hexTileList[i].gameObject.SetActive(false);
                }
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
                    RouteTile tile = tiles[x , y];
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
            for (int i = 0 , y = 0; i < useTileNums; y++)
            {
                for (int x = 0; i < useTileNums && x < size.x; x++ , i++)
                {
                    nodeArray[x, y] = new HexNode(
                        tiles[x, y].isWall,
                        x,y,
                        tiles[x, y]);
                    
                    nodeDic[tiles[x, y]] = nodeArray[x, y];
                }
            }
            
            for (int i = 0 , y = 0; i < useTileNums; y++)
            {
                for (int x = 0; i < useTileNums && x < size.x; x++ , i++)
                {
                    foreach (var hexTile in tiles[x, y].neighbors)
                    {
                        nodeArray[x, y].SetNeighbors(nodeArray[hexTile.index.x, hexTile.index.y]);
                    }
                }
            }
        }


        #endregion

        

        public List<RouteTile> PathFinding(EntityBehavior entityBehavior , Vector2Int targetPos, int mode = 0)
        {
            //Debug.Log(mode);
            HexNode startNode, targetNode, curNode;
            List<HexNode> finalNodeList = new List<HexNode>();

            int sizeX = size.x , sizeY = size.y;
            if (ReferenceEquals(entityBehavior.currentTile , null))
            {
                CustomLogger.LogWarning("[PathFindingManager.PathFinding()] currentNode 가 존재하지 않음" , entityBehavior.gameObject);
            }
            Vector2Int startPos = entityBehavior.currentTile.index;
            Vector2Int bottomLeft = new Vector2Int(0, 0);
            Vector2Int topRight = new Vector2Int(sizeX - 1, sizeY - 1);
            

            if (mode == 0)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        nodeArray[i, j].isWall = 
                            tiles[i,j].isWall || (!ReferenceEquals(tiles[i,j].desOf , null) && tiles[i,j].desOf != entityBehavior);;
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
                        RouteTile tile = tiles[i,j];
                        int l = Math.Abs(entityBehavior.currentTile.index.x - i) + Math.Abs(entityBehavior.currentTile.index.y - j);
                        if (l <= 4)
                            node.isWall = tile.isWall;
                        if (l <= 2 &&  node.isWall == false)
                        {
                            node.isWall = !tile.desNotNeeded && !ReferenceEquals(tile.desOf , null) && tile.desOf != entityBehavior;
                        }
                    }
                }
            }
            else if (mode == 2)
            {
                for (var i = 0; i < entityBehavior.currentTile.neighbors.Count; i++)
                {
                    RouteTile neighbor = entityBehavior.currentTile.neighbors[i];
                    nodeDic[neighbor].isWall = 
                        neighbor.isWall || (!ReferenceEquals(neighbor.desOf , null) && neighbor.desOf != entityBehavior);
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

            
            while (openList.Count > 0)
            {
                
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

                    return finalNodeList.Make((node) => node.RouteTile);
                }

                for (int i = 0; i < curNode.neighbors.Count; i++)
                {
                    OpenListAdd(curNode.neighbors[i].x, curNode.neighbors[i].y);
                }

            }

            return new List<RouteTile>();
            
            void OpenListAdd(int checkX, int checkY)
            {
                try
                {
                    if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !closedList.Contains(nodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
                    {
                     
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

        public List<RouteTile> PathFindingWithPerformanceTest(EntityBehavior entityBehavior , Vector2Int targetPos, int mode = 0)
        {
            n++;
            stopwatch.Reset();
            stopwatch.Start();

            var result = PathFinding(entityBehavior, targetPos, mode);

            sum += stopwatch.ElapsedTicks;
            std = sum / n;

            stdText.text = std + " ticks / " + n;
            return result;
        }
    }
}

