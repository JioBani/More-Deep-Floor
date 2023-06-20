using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame
{
    public class TileManager : MonoBehaviour
    {
        private ObjectPooler defenderPooler;
        
        public GameObject battleField;
        public GameObject waitingRoom;
        public GameObject monsterSpawnPoint;

        public Tile[][] battleFieldTiles;
        public Tile[] waitingRoomTiles;
        public Tile[] monsterSpawnTiles;
        public List<Tile> routeTiles;
        public List<Tile> roadTiles;
        public Vector2Int battleFieldSize = new Vector2Int(15,5);
        public int waitingRoomSize;
        public int monsterSpawnPointSize;
        
        void Awake()
        {
            defenderPooler = ReferenceManager.instance.objectPoolingManager.defenderPooler;
            
            int tileIndex = 0;
            battleFieldTiles = new Tile[battleFieldSize.y][];
                
            for (int y = 0; y < battleFieldSize.y; y++)
            {
                battleFieldTiles[y] = new Tile[battleFieldSize.x];
                for (int x = 0; x < battleFieldSize.x; x++)
                {
                    Tile tile = battleField.transform.GetChild(tileIndex).GetComponent<Tile>();
                    tile.index = new Vector2Int(x, y);
                    tile.type = TileType.BattleField;
                    battleFieldTiles[y][x] = tile;
                    tileIndex++;
                    Logger.Log(tileIndex);
                }
            }

            waitingRoomTiles = new Tile[waitingRoomSize];
                
            for (int i = 0; i < waitingRoom.transform.childCount; i++)
            {
                Tile tile = waitingRoom.transform.GetChild(i).GetComponent<Tile>();
                tile.index = new Vector2Int(i, 0);
                tile.type = TileType.WaitingRoom;
                waitingRoomTiles[i] = tile;
            }

            monsterSpawnTiles = new Tile[monsterSpawnPointSize];
            for(int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
            {
                Tile tile = monsterSpawnPoint.transform.GetChild(i).GetComponent<Tile>();
                tile.index = new Vector2Int(0, i);
                tile.type = TileType.SpawnPoint;
                monsterSpawnTiles[i] = tile;
            }
        }

        public Tile GetEmptyTile()
        {
            for (int i = 0; i < waitingRoomTiles.Length; i++)
            {
                if (!waitingRoomTiles[i].isFull)
                {
                    Debug.Log($"[TileManager.GetEmptyTile()] index : {i}");
                    return waitingRoomTiles[i];
                }
            }

            return null;
        }

        public void SetRoute(List<Vector2Int> vertices)
        {
            routeTiles = new List<Tile>();
            for (int i = 0; i < vertices.Count; i++)
            {
                routeTiles.Add(battleFieldTiles[vertices[i].y][vertices[i].x]);
            }

            SetRoadTiles(vertices);
        }

        void SetRoadTiles(List<Vector2Int> vertices)
        {
            Tile tile0;
            Tile tile1;
            
            roadTiles = new List<Tile>();
            
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                tile0 = routeTiles[i];
                tile1 = routeTiles[i + 1];
                roadTiles.AddRange(GetTilesBetween(tile0,tile1));
            }
            
            tile0 = routeTiles[vertices.Count - 1];
            tile1 = routeTiles[0];
            roadTiles.AddRange(GetTilesBetween(tile0,tile1));
            
            for (int i = 0; i < roadTiles.Count; i++)
            {
                roadTiles[i].type = TileType.Road;
                roadTiles[i].SetDebugColor(Color.green);
            }
        }
        
        List<Tile> GetTilesBetween(Tile tile1 , Tile tile2)
        {
            Vector2Int index1 = tile1.index;
            Vector2Int index2 = tile2.index;
            List<Tile> tiles = new List<Tile>();
            
            if (index1.x == index2.x)
            {
                int x = index1.x;
                int startY;
                int endY;
                
                if (index1.y < index2.y)
                {
                    startY = index1.y;
                    endY = index2.y;
                }
                else
                {
                    startY = index2.y;
                    endY = index1.y;
                }
                
                for (int y = startY; y <= endY; y++)
                {
                    tiles.Add(battleFieldTiles[y][x]);
                }
            }
            else
            {
                int y = index1.y;
                int startX;
                int endX;
                
                if (index1.x < index2.x)
                {
                    startX = index1.x;
                    endX = index2.x;
                }
                else
                {
                    startX = index2.x;
                    endX = index1.x;
                }
                
                //Debug.Log($"[TileManager.GetTilesBetween()] x : {startX}->{endX}");

                for (int x = startX; x <= endX; x++)
                {
                    tiles.Add(battleFieldTiles[y][x]);
                }
            }

            return tiles;
        }
    }
}


