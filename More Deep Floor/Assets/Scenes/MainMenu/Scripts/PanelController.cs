using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.MainMenu
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] CanvasScaler canvasScaler;
        [SerializeField] private Button playButton;
        [SerializeField] private Button traitButton;
        [SerializeField] private Button gachaButton;

        [SerializeField] private Button[] buttons;
        private float canvasWidth;
        [SerializeField] private float startX;
        [SerializeField] private float startY;
        private float wRatio;
        private float hRatio;
        
        private void Awake()
        {
        }

        private void Start()
        {
            canvasWidth = canvasRectTransform.sizeDelta.x;
            OnClickButton(0);
        }

        void SetButtonSize(int openIndex)
        {
            float x = startX;
            float stdWidth = canvasWidth / (float)(buttons.Length + 1);

            for (var i = 0; i < buttons.Length; i++)
            {
                RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
                if (i == openIndex)
                {
                    rectTransform.sizeDelta = new Vector2(stdWidth * 2 , rectTransform.sizeDelta.y);
                    rectTransform.anchoredPosition = new Vector3(x,startY);
                    x += stdWidth * 2;
                }
                else
                {
                    rectTransform.sizeDelta = new Vector2(stdWidth, rectTransform.sizeDelta.y);
                    rectTransform.anchoredPosition = new Vector3(x,startY);
                    x += stdWidth;
                }
            }
        }

        public void OnClickButton(int index)
        {
            SetButtonSize(index);
        }
    }
}


