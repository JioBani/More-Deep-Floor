using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field instance = null;
    
    public Vector2[][] tilePositions;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        
            tilePositions = new Vector2[10][];
            for (var i = 0; i < tilePositions.Length; i++)
            {
                tilePositions[i] = new Vector2[10];
                for (var j = 0; j < tilePositions[i].Length; j++)
                {
                    tilePositions[i][j] = new Vector2(-4.5f + j, 4.5f - i);
                }
            }
        }
    }
}
