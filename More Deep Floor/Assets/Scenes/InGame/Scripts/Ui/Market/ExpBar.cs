using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;
using UnityEngine.UI;


namespace LNK.MoreDeepFloor.InGame.Ui.Market
{
    public class ExpBar : MonoBehaviour
    {
        private MarketManager marketManager;
        public float barMaxSize = 300;
        [SerializeField] private Text expText;
        [SerializeField] private RectTransform innerBarTransform; 

        private void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            marketManager.OnExpUpAction += OnExpUp;
            marketManager.OnInitEventAction += OnInit;
        }

        void OnInit(int level, int currentExp, int maxExp)
        {
            RefreshData(currentExp, maxExp);
        }

        void OnExpUp(int currentExp, int maxExp)
        {
            RefreshData(currentExp, maxExp);
        }

        void RefreshData(int currentExp, int maxExp)
        {
            expText.text = currentExp + " / " + maxExp;
            if (currentExp == 0)
            {
                innerBarTransform.gameObject.SetActive(false);
            }
            else
            {
                innerBarTransform.gameObject.SetActive(true);
                innerBarTransform.sizeDelta = new Vector2(barMaxSize * (float)currentExp / maxExp, innerBarTransform.sizeDelta.y);
            }
            
        }
    }

}
