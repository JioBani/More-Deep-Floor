using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.ResolutionControl
{
    public class ResolutionController : MonoBehaviour
    {
        public int width; // 화면 너비
        public int height; // 화면 높이
        
        private void Awake()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                Screen.SetResolution(width, height, false);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}


