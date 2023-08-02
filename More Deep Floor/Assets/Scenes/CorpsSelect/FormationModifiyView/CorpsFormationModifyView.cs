using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class CorpsFormationModifyView : MonoBehaviour
    {
        private Canvas uiCanvas;
        private RectTransform rect;
        private CanvasScaler scaler;

        public GameObject openButton;
        public GameObject closeButton;
        
        [SerializeField] private bool isOpen = true;
        public const float offset = 0;
        private float length;
        private float width;
        
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            uiCanvas = ReferenceManager.instance.uiCanvas;
            scaler = uiCanvas.GetComponent<CanvasScaler>();
        }

        private void OnEnable()
        {
            closeButton.SetActive(isOpen);
            openButton.SetActive(!isOpen);
        }

        private void Start()
        {
            width =  uiCanvas.pixelRect.width;
            //length = (width / 2 + rect.sizeDelta.x * uiCanvas.scaleFactor / 2 + offset);
            length = width + rect.sizeDelta.x * uiCanvas.scaleFactor;
        }

        public void Close()
        {
            if(!isOpen) return;
            
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
