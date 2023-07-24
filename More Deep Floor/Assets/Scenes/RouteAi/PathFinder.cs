using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ExtensionMethods;
using TMPro;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class PathFinder : MonoBehaviour
    {
        //1. current tile 이 현재 위치를 반영해야함
        //2. current tile 이 반드시 존재해야함
        //3. 각 current tile은 서로 유일해야함
        
        public List<Node> FinalNodeList;
        public bool allowDiagonal, dontCrossCorner;
        [SerializeField] private GameObject tileParent;
        public Tile[ , ] tiles;
        public int tileX, tileY;
        Node[,] NodeArray;
        Node StartNode, TargetNode, CurNode;
        List<Node> OpenList, ClosedList;
        private List<RouteDetectors> routeDetectorsList;
        private bool isStart = false;

        private List<Entity> entities;
        [SerializeField] private GameObject entityMother;
        private Stopwatch stopwatch = new Stopwatch();

        private long std = 0;
        private int n = 0;
        private long sum = 0;
        [SerializeField ]private TextMeshProUGUI stdText;

        private void Awake()
        {
            entities = new List<Entity>();
            
            entityMother.transform.EachChild((c) =>
            {
                entities.Add(c.GetComponent<Entity>());
            });
            
            tiles = new Tile[tileX, tileY];
            
            for (int i = 0 , y = 0; i < tileParent.transform.childCount; y++)
            {
                for (int x = 0; i < tileParent.transform.childCount && x < tileX; x++ , i++)
                {
                    tiles[x , y] = tileParent.transform.GetChild(i).GetComponent<Tile>();
                    tiles[x , y].SetIndex(new Vector2Int(x ,y));
                }
            }
        }

        public List<Tile> PathFinding(Entity entity , Tile[,] _tiles , Vector2Int startPos , Vector2Int targetPos)
        {
            int sizeX = _tiles.GetLength(0);
            int sizeY = _tiles.GetLength(1);
            Vector2Int bottomLeft = new Vector2Int(0, 0);
            Vector2Int topRight = new Vector2Int(_tiles.GetLength(0) - 1, _tiles.GetLength(1) - 1);
            
            
            NodeArray = new Node[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    bool isWall = _tiles[i, j].isWall || (!ReferenceEquals(_tiles[i, j].desOf , null) && _tiles[i, j].desOf != entity);
                    NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
                }
            }

            NodeArray[targetPos.x, targetPos.y].isWall = false;
            NodeArray[startPos.x, startPos.y].isWall = false;

            // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
            StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
            TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

            OpenList = new List<Node>() { StartNode };
            ClosedList = new List<Node>();
            FinalNodeList = new List<Node>();

            
            while (OpenList.Count > 0)
            {
                // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
                CurNode = OpenList[0];
                for (int i = 1; i < OpenList.Count; i++)
                    if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

                OpenList.Remove(CurNode);
                ClosedList.Add(CurNode);


                // 마지막
                if (CurNode == TargetNode)
                {
                    Node TargetCurNode = TargetNode;
                    while (TargetCurNode != StartNode)
                    {
                        FinalNodeList.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.ParentNode;
                    }
                    FinalNodeList.Add(StartNode);
                    FinalNodeList.Reverse();

                    //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                    return FinalNodeList.Make((node) => tiles[node.x, node.y]);
                }


                // ↗↖↙↘
                if (allowDiagonal)
                {
                    OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                    OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                    OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                    OpenListAdd(CurNode.x + 1, CurNode.y - 1);
                }

                // ↑ → ↓ ←
                OpenListAdd(CurNode.x, CurNode.y + 1);
                OpenListAdd(CurNode.x + 1, CurNode.y);
                OpenListAdd(CurNode.x, CurNode.y - 1);
                OpenListAdd(CurNode.x - 1, CurNode.y);
            }

            return new List<Tile>();
            
            void OpenListAdd(int checkX, int checkY)
            {
                try
                {
                    if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
                    {
                        // 대각선 허용시, 벽 사이로 통과 안됨
                        if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                        // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
                        if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                
                        // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
                        Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
                        int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


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

        public List<Tile> GetRoute(Entity entity , Tile currentTile , Vector2Int end)
        {
            n++;
            stopwatch.Reset();
            stopwatch.Start();
            
            if (ReferenceEquals(currentTile,null)) return new List<Tile>();
            
            List<Tile> routes = PathFinding(
                entity,
                tiles , 
                currentTile.index, 
                end);
            
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

