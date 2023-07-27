using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    public class HexTileManager : MonoBehaviour
    {
        [SerializeField] private GameObject tileMother;
        [SerializeField] private GameObject customTileMother;
        [SerializeField] private Tilemap tilemap;
        public List<Vector3> tileWorldLocations;
        public List<HexTile> hexTileList = new List<HexTile>();
        public HexTile[,] tiles;
        [SerializeField] private int column;
        [SerializeField] private Vector3Int size;

        private void Awake()
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
            
            /*tileWorldLocations.Sort(((vectorA, vectorB) =>
            {
                if (vectorA.y < vectorB.y)
                {
                    return 1;
                }
                if (vectorA.y > vectorB.y)
                {
                    return -1;
                }
                else
                {
                    if (vectorA.x > vectorB.x)
                    {
                        return 1;
                    }
                    if (vectorA.x < vectorB.x)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }));*/
            
            customTileMother.transform.EachChild((child) =>
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
    }
}

