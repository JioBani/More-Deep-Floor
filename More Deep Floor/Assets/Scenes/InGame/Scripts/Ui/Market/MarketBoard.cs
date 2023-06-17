using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.Ui.Market
{
    public class MarketBoard : MonoBehaviour
    {
        private RectTransform rect;
        public GameObject openButton;
        public GameObject closeButton;
        private Canvas uiCanvas;
        private bool isOpen = true;
        public const float offset = -30;
        private float length;
        private float width;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            uiCanvas = ReferenceManager.instance.uiCanvas;
        }

        // Start is called before the first frame update
        void Start()
        {
            width =  uiCanvas.pixelRect.width;
            length = width / 2 + rect.sizeDelta.x / 2 + offset;
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }

        public void Close()
        {
            if(!isOpen) return;
            
            Logger.Log($"[MarketBoard] width = {width/2 + rect.sizeDelta.x/2}");
            transform.DOMove(transform.position + new Vector3(length,0,0) , 1f);
            openButton.SetActive(true);
            closeButton.SetActive(false);

            isOpen = false;
        }

        public void Open()
        {
            if(isOpen) return;
          
            transform.DOMove(transform.position - new Vector3(length,0,0) , 1f);
            openButton.SetActive(false);
            closeButton.SetActive(true);
            isOpen = true;
        }
    }

}

