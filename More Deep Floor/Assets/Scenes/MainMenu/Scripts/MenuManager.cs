using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace LNK.MoreDeepFloor.MainMenu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private BottomButtonBar bottomButtonBar;
        private int menuIndex = 1;
        [SerializeField] private RectTransform[] menuPanels;
        [SerializeField] private RectTransform background;
        [SerializeField] private float[] menuPosX;

        public void OnClickButton(int index)
        {
            bottomButtonBar.SetButtonSize(index);
            menuIndex = index;
            background.DOAnchorPosX(menuPosX[index], 1f);
        }

        public void Start()
        {
            bottomButtonBar.SetButtonSize(1);
        }

        private void OnDestroy()
        {
            DOTween.CompleteAll();
        }
    }
}

