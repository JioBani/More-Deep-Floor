using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Upgrade
{
    public class UpgradeCard : MonoBehaviour
    {
        private UpgradeManager upgradeManager;
        private UpgradeData upgradeData;
        [SerializeField] private Image image;

        private void Awake()
        {
            upgradeManager = ReferenceManager.instance.upgradeManager;
        }

        public void OnClick()
        {
            upgradeManager.OnClickCard(upgradeData);
        }

        public void SetUpgrade(UpgradeData _upgradeData)
        {
            upgradeData = _upgradeData;
            image.sprite = upgradeData.Image;
        }
        
        
    }
}


