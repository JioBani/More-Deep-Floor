using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.AstarScene
{
   
    
    public class Tile : MonoBehaviour
    {
        public enum TileType
        {
            None = 0,
            Default = 1,
            Wall = 2,
            Route = 3,
            Exit = 4,
        }
        
        SpriteRenderer spriteRenderer;
        
        public int X { get; set; }
        public int Y { get; set; }
        public int F { get { return G + H; } }
        public int G { get; private set; } // Start ~ Current
        public int H { get; private set; } // Current ~ End
        
        public TileType tileType = TileType.Default;
        public Tile Parent { get; private set; }
        public void Execute(Tile parent, Tile endTile)
        {
            Parent = parent;
            G = CalcGValue(parent, this);
            int diffX = Math.Abs(endTile.X - X);
            int diffY = Math.Abs(endTile.Y - Y);
            H = (diffX + diffY) * 10;
        }
        public static int CalcGValue(Tile parent, Tile current)
        {
            int diffX = Math.Abs(parent.X - current.X);
            int diffY = Math.Abs(parent.Y - current.Y);
            int value = 10;
            if (diffX == 1 && diffY == 1)
            {
                value = 14;
            }
            return parent.G + value;
        }

        public void SetTileType(TileType tileType)
        {
            if(tileType == TileType.Default) spriteRenderer.color = Color.white;
            else if(tileType == TileType.Wall) spriteRenderer.color = Color.gray;
            else if(tileType == TileType.Route) spriteRenderer.color = Color.green;
            else if(tileType == TileType.Exit) spriteRenderer.color = Color.red;
        }
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            
        }
    }
}


