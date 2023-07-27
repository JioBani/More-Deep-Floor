using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.RouteAiScene;
using UnityEngine;

namespace LNK.MoreDeepFloor.RouteAiScene
{
    //[Serializable]
    public class HexNode
    {
        public HexNode(bool _isWall, int _x, int _y, HexTile hexTile) { 
            isWall = _isWall; 
            x = _x;
            y = _y;
            this.hexTile = hexTile;
        }

        public List<HexNode> neighbors = new List<HexNode>();
        public HexTile hexTile;
        public bool isWall;
        public HexNode ParentNode;

        // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
        public int x, y, G, H;
        public int F { get { return G + H; } }

        public void SetNeighbors(HexNode neighbor)
        {
            neighbors.Add(neighbor);
        }
    }

}
