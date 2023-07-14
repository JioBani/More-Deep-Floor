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
        [SerializeField] private Text jobText;
        [SerializeField] private Text characterText;
        [SerializeField] private Image jobSprite;
        [SerializeField] private Image characterSprite;

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
            try
            {
                content.SetActive(true);
                defenderData = _defenderData;
                image.sprite = defenderData.sprite;
                image.enabled = true;
                backgroundImage.color = uiAssetManager.merchandiseBackgroundColors[defenderData.cost];
                nameText.text = defenderData.name;
                goldText.text = defenderData.cost.ToString();
                //jobText.text = defenderData.job.TraitName;
                //characterText.text = defenderData.character.TraitName;
                //jobSprite.sprite = defenderData.job.Image;
                //characterSprite.sprite = defenderData.character.Image;
            }
            catch (Exception e)
            {
                Debug.Log($"[{defenderData.name}]" + e);
            }
            
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


