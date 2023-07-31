using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.PathFinding;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.PathFinding
{
    //[Serializable]
    public class HexNode
    {
        public HexNode(bool _isWall, int _x, int _y, RouteTile routeTile) { 
            isWall = _isWall; 
            x = _x;
            y = _y;
            this.RouteTile = routeTile;
        }

        public List<HexNode> neighbors = new List<HexNode>();
        public RouteTile RouteTile;
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
