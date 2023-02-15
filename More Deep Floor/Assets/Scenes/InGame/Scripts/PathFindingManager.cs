using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.PathFinder;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    public static PathFindingManager instance = null;
    public PathFinder pathFinder;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            pathFinder = new PathFinder();
        }
    }
}
