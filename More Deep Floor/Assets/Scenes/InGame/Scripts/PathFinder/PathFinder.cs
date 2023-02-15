using System;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.PathFinder
{
    public class PathFinder {
        
        private Vector2Int maxSize;
        private Tile[][] tileMap;

        public PathFinder()
        {
            maxSize = new Vector2Int(10, 10);
            tileMap = new Tile[maxSize.y][];
            for (var i = 0; i < tileMap.Length; i++)
            {
                tileMap[i] = new Tile[maxSize.x];
                for (var j = 0; j < tileMap[i].Length; j++)
                {
                    tileMap[i][j] = new Tile(j, i);
                }
            }
        }
        public List<Tile> Find(Vector2Int startTile, Vector2Int endTile)
        {
            return Find(tileMap[startTile.y][startTile.x], tileMap[endTile.y][endTile.x]);
        }

        public List<Tile> Find(Tile startTile, Tile endTile)
        {
            PriorityQueue<Tile> openList = new PriorityQueue<Tile>();
            List<Tile> closedList = new List<Tile>();
            List<Tile> neighbors;
            Tile currentTile;
            
            currentTile = startTile;
            neighbors = FindNeighbors(currentTile);
            
            foreach (var neighbor in neighbors)
            {
                neighbor.parent = currentTile;
                neighbor.CalcCost(startTile);
            }
            
            foreach (var neighbor in neighbors)
            {
                openList.Add(neighbor);
            }
            closedList.Add(currentTile);

            while (openList.Count != 0)
            {
                currentTile = openList.Pop();
                closedList.Add(currentTile);
                if(endTile.Equals(currentTile)) break;
                neighbors = FindNeighbors(currentTile);
                
                foreach (var neighbor in neighbors)
                {
                    if (!closedList.Contains(neighbor))
                    {
                        neighbor.parent = currentTile;
                        neighbor.CalcCost(startTile);
                        if(openList.Contains(neighbor)){
                            neighbor.CompareG(currentTile);
                        }
                        else{
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            List<Tile> path = new List<Tile>();
            Tile tile = endTile;
            String pathStr = "";
            while (tile != null)
            {
                path.Add(tile);
                tile = tile.parent;
            }
            
            path.Reverse();

            return path;
            
            
            void PrintMap(List<Tile> path)
            {
                String[][] mapString = new String[maxSize.y][];
                String result = "";
                for (int y = 0; y < maxSize.y; y++)
                {
                    mapString[y] = new String[maxSize.x];
                    for (int x = 0; x < maxSize.x; x++)
                    {
                        mapString[y][x] = "#";
                    }
                }
                
                foreach (var element in path)
                {
                    mapString[element.location.y][element.location.x] = "O";
                }

                for (int y = 0; y < maxSize.y; y++)
                {
                    for (int x = 0; x < maxSize.x; x++)
                    {
                        result += mapString[y][x];
                    }
                    result += "\n";
                }
                
                Debug.Log(result);
            }
        }

        List<Tile> FindNeighbors(Tile target)
        {
            List<Tile> neighbors = new List<Tile>();

            int x = target.location.x;
            int y = target.location.y;
            
            if(x > 0){
                neighbors.Add(tileMap[y][x - 1]);
            }
            if(x < maxSize.x - 1){
                neighbors.Add(tileMap[y][x + 1]);
            }
            if(y > 0){
                neighbors.Add(tileMap[y - 1][x]);
            }
            if(y < maxSize.y - 1){
                neighbors.Add(tileMap[y + 1][x]);
            }
            
            return neighbors;
        }
    }
}


