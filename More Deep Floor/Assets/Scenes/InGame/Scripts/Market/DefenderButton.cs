using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.MarketSystem
{
    public class DefenderButton : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Image image;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Text goldText;

        private MarketManager marketManager;
        private UiAssetManager uiAssetManager;
        
        private DefenderData defenderData;

        public void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            uiAssetManager = ReferenceManager.instance.uiAssetManager;
        }

        public void SetDefender(DefenderData _defenderData)
        {
            content.SetActive(true);
            defenderData = _defenderData;
            image.sprite = defenderData.sprite;
            image.enabled = true;
            backgroundImage.sprite = uiAssetManager.merchandiseBackground[defenderData.cost];
            nameText.text = defenderData.name;
            goldText.text = defenderData.cost + " G";
        }

        public void OnClick()
        {
            if (defenderData == null)
            {
                return;
            }
            
            if (marketManager.TryBuyDefender(defenderData))
            {
                defenderData = null;
                image.enabled = false;
                content.SetActive(false);
            }
        }
    }
}


