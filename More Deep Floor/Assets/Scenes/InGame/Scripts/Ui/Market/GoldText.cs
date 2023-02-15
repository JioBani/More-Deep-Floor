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
        private MarketManager marketManeger;
        private Text textUi;

        private void Awake()
        {
            marketManeger = ReferenceManager.instance.marketManager;
            textUi = GetComponent<Text>();
            marketManeger.OnGoldChangeAction += RefreshGoldText;
        }

        void Start()
        {
            textUi.text = marketManeger.gold + " gold";
        }

        private void RefreshGoldText(int gold, int change)
        {
            textUi.text = gold + " gold";
        }
    }
}


