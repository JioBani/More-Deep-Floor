using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.Market
{
    public class GoldText : MonoBehaviour
    {
        private MarketManager marketManager;
        private Text textUi;

        private void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            textUi = GetComponent<Text>();
        }

        private void OnEnable()
        {
            marketManager.OnGoldChangeAction += RefreshGoldText;

        }

        void Start()
        {
            textUi.text = marketManager.gold + " gold";
        }

        private void RefreshGoldText(int gold, int change)
        {
            textUi.text = gold + " gold";
        }

        private void OnDisable()
        {
            marketManager.OnGoldChangeAction -= RefreshGoldText;
        }
    }
}


