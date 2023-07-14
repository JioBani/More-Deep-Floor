using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine;

namespace LNK.MoreDeepFloor.AstarScene
{

    public class Astar : MonoBehaviour
    {
         public List<Tile> minimumDistance(int numRows, int numColumns, int[,] area)
         {
            int result = -1; // distance
            int failResult = -1;
            int obstacle = 0;
            int target = 9;
            List<List<Tile>> tiles = new List<List<Tile>>();
            List<Tile> openList = new List<Tile>();
            List<Tile> closeList = new List<Tile>();
            List<Tile> path = new List<Tile>();
            Tile startTile = null;
            Tile targetTile = null;
            // Initial values
            for (int i = 0; i < area.GetLength(0); i++)
            {
                List<Tile> t = new List<Tile>();
                for (int j = 0; j < area.GetLength(1); j++)
                {
                    Tile temp = transform.GetChild(i + (int)tileSize.y * j).GetComponent<Tile>();
                    temp.X = i;
                    temp.Y = j;
                    t.Add(temp);
                    if (area[i, j] == target)
                    {
                        targetTile = temp;
                    }
                }
                tiles.Add(t);
            }
            // start (0, 0)
            startTile = tiles[0][0];
            openList.Add(startTile);
            if (targetTile == null)
            {
                // can not found target
                return new List<Tile>();
            }
            Tile currentTile = null;
            do
            {
                if (openList.Count == 0)
                {
                    break;
                }
                currentTile = openList.OrderBy(o => o.F).First();
                openList.Remove(currentTile);
                closeList.Add(currentTile);
                if (currentTile == targetTile)
                {
                    break;
                }
                for (int i = 0; i < area.GetLength(0); i++)
                {
                    for (int j = 0; j < area.GetLength(1); j++)
                    {
                        // 8 way
                        bool near = (Math.Abs(currentTile.X - tiles[i][j].X) <= 1)
                                 && (Math.Abs(currentTile.Y - tiles[i][j].Y) <= 1);
                        //// 4 way
                        //bool near = (Math.Abs(currentTile.X - tiles[i][j].X) <= 1)
                        //         && (Math.Abs(currentTile.Y - tiles[i][j].Y) <= 1)
                        //         && (currentTile.Y == tiles[i][j].Y || currentTile.X == tiles[i][j].X);
                        if (area[i, j] == obstacle
                         || closeList.Contains(tiles[i][j])
                         || (!near))
                        {
                            continue;
                        }
                        if (!openList.Contains(tiles[i][j]))
                        {
                            openList.Add(tiles[i][j]);
                            tiles[i][j].Execute(currentTile, targetTile);
                        }
                        else
                        {
                            if (Tile.CalcGValue(currentTile, tiles[i][j]) < tiles[i][j].G)
                            {
                                tiles[i][j].Execute(currentTile, targetTile);
                            }
                        }
                    }
                }
            } while (currentTile != null);
            if (currentTile != targetTile)
            {
                // can not found root
                return new List<Tile>();
            }
            do
            {
                path.Add(currentTile);
                currentTile = currentTile.Parent;
            }
            while (currentTile != null);
            path.Reverse();
            result = path.Count - 1;
            return path;
        }

         public Vector2 tileSize;
         
         private int[,] area;

         void SetArea()
         {
             area = new int[,]
             {
                 {1,0,1,1,1,1,0,1,1,1},
                 {1,0,1,0,1,1,1,1,1,1},
                 {1,0,1,0,1,1,0,1,1,1},
                 {1,0,1,1,1,1,0,1,1,1},
                 {1,1,1,0,0,1,1,1,1,9},
             };

             int i = 0;
             for (int y = 0; y < tileSize.y; y++)
             {
                 for (int x = 0; x < tileSize.x; x++)
                 {
                     var tile = transform.GetChild(i).GetComponent<Tile>();
                     i++;
                     if(area[y,x] == 0) tile.SetTileType(Tile.TileType.Wall);
                     else if(area[y,x] == 9) tile.SetTileType(Tile.TileType.Exit);
                 }
             }
         }

         void DisPlayRoute(List<Tile> route)
         {
             foreach (var tile in route)
             {
                 var targetTile = transform.GetChild(tile.X  * (int)tileSize.x + tile.Y);
                 tile.SetTileType(Tile.TileType.Route);
             }
         }
         
         public void Test()
         {
          
             List<Tile> routes = minimumDistance(area.GetLength(0), area.GetLength(1), area);
             
             string result = "result : ";
             
             foreach (var route in routes)
             {
                 result += $"({route.X},{route.Y})";
             }
             
             CustomLogger.Log(result);

             DisPlayRoute(routes);
         }

         private void Awake()
         {
             CustomLogger.SetLogger(true);
         }

         void Start()
         {
             CustomLogger.Log("Awake");
             SetArea();
         }
    }
}


