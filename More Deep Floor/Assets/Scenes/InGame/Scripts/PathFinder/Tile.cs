using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.PathFinder
{
public class Tile : IComparable
{
    public Vector2Int location;
    public Tile parent = null;
    public double f = 0;
    public double g = 0;
    public double h = 0;
    public bool isWall;

    public Tile(int x, int y)
    {
        location = new Vector2Int(x, y);
    }

    public void CalcCost(Tile target)
    {
        g = parent.g + 1;
        
        int disX = target.location.x > location.x ? 
            target.location.x - location.x : 
            location.x -  target.location.x;
        
        int disY = target.location.y > location.y ? 
            target.location.y - location.y : 
            location.y -  target.location.y;
        
        h = disX + disY;
        f = h + g;
    }
    
    public void CompareG(Tile otherParent){
        double newG = otherParent.g + 1;
        if(newG < g){
            g = newG;
            f = g + h;
            parent = otherParent;
        }
    }

    public String Print()
    {
        return $"({location.x} ,{location.y} )";
    }
    
    public int CompareTo(object obj)
    {
        Tile target = obj as Tile;
        if (this.f > target.f)
            return 1;
        else if (this.f < target.f)
            return -1;
        return 0;
    }

    public override bool Equals(object obj)
    {
        Tile target = obj as Tile;
        return location == target.location;
    }
}
}
