using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.CameraSystem
{
    public class CameraSize : MonoBehaviour
    {
        public float targetWidth;
        
        private void Awake()
        {
            Camera camera = Camera.main;
            Debug.Log($"{Screen.width} x {Screen.height} : {Screen.width / (float)Screen.height}");
            if (Screen.width / (float)Screen.height <= 1440 / (float)3200)
            {
                camera.orthographicSize = targetWidth * (float)Screen.height / (float)Screen.width / 2;
            }
            
        }
    }
}


