using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.Market
{
    public class MarketBoard : MonoBehaviour
    {
        private RectTransform rect;
        public GameObject openButton;
        public GameObject closeButton;
        private Canvas uiCanvas;
        private bool isOpen = true;
        public const float offset = 0;
        private float length;
        private float width;
        private CanvasScaler scaler;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            uiCanvas = ReferenceManager.instance.uiCanvas;
            scaler = uiCanvas.GetComponent<CanvasScaler>();
        }

        // Start is called before the first frame update
        void Start()
        {
            width =  uiCanvas.pixelRect.width;
            length = (width / 2 + rect.sizeDelta.x * uiCanvas.scaleFactor / 2 + offset);
            
            openButton.SetActive(!isOpen);
            closeButton.SetActive(isOpen);
            
            CustomLogger.Log($"[MarketBoard] factor = {scaler.scaleFactor} , width = {width} , length = {length}");
        }
        

        public void Close()
        {
            if(!isOpen) return;
            
            CustomLogger.Log($"[MarketBoard] length = {length}");
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

