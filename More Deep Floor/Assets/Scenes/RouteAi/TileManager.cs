using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LNK.MoreDeepFloor
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase[] allTiles;
        [SerializeField] private GameObject tileMother;
        public List<Vector3> tileWorldLocations;
        public Vector2Int size;
        public bool show = false;

        private void Awake()
        {
            BoundsInt bounds = tilemap.cellBounds;
            allTiles = tilemap.GetTilesBlock(bounds);
            for (int x = 0; x < bounds.size.x; x++) {
                for (int y = 0; y < bounds.size.y; y++) {
                    TileBase tilebase = allTiles[x + y * bounds.size.x];
                    Tile tile = tilemap.GetTile<Tile>(new Vector3Int(x , y , 0));
                    if (tile != null) {
                        Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    } else {
                        Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                    }
                    
                }
            }  
            
            tileWorldLocations = new List<Vector3>();
            
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {   
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    tileWorldLocations.Add(place);
                }
            }
        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;
            foreach (var tileWorldLocation in tileWorldLocations)
            {
                Gizmos.DrawSphere(tileWorldLocation , 0.1f);
            }
        }
    }
}


