using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.MainMenu
{
    public class BottomButtonBar : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] CanvasScaler canvasScaler;
        [SerializeField] private Button[] buttons;
        private float canvasWidth;
        [SerializeField] private float startX;
        [SerializeField] private float startY;
        
        private void Start()
        {
            
        }

        public void SetButtonSize(int openIndex)
        {
            canvasWidth = canvasRectTransform.sizeDelta.x;
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
    }
}


