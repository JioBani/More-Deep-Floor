using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Upgrades;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Upgrade
{
    public class UpgradeManager : MonoBehaviour
    {
        private List<UpgradeData> upgrades;
        public DefenderManager defenderManager;
        [SerializeField] private UpgradeDataTable upgradeDataTable;
        
        [SerializeField] private GameObject cardUi;
        [SerializeField] private UpgradeCard[] upgradeCards;
        [SerializeField] private TextMeshProUGUI upgradeRemainText;

        private int upgradeRemain;
        private Queue<List<UpgradeData>> generatedUpgrades;
        
        private void Awake()
        {
            defenderManager = ReferenceManager.instance.defenderManager;
            cardUi.SetActive(false);
        }

        private void Start()
        {
            Init();
        }

        void Init()
        {
            upgrades = new List<UpgradeData>();
            generatedUpgrades = new Queue<List<UpgradeData>>();
            
            GenerateUpgrades();
            GenerateUpgrades();
        }

        public void AddUpgrade(UpgradeData upgradeData)
        {
            upgrades.Add(upgradeData);
            
            upgradeData.OnAddAction(this);
        }

        public void OnClickCard(UpgradeData upgradeData)
        {
            Debug.Log("OnClickCard");
            AddUpgrade(upgradeData);
            upgradeRemain--;
            
            generatedUpgrades.Dequeue();
            
            if (upgradeRemain > 0)
            {
                SetCard();
            }
            else
            {
                cardUi.SetActive(false);
            }
            upgradeRemainText.text = upgradeRemain.ToString();
        }

        public void SetCard()
        {
            List<UpgradeData> datas = generatedUpgrades.Peek();
            
            for (var i = 0; i < upgradeCards.Length; i++)
            {
                upgradeCards[i].SetUpgrade(datas[i]);
            }
            
            cardUi.SetActive(true);
        }

        void GenerateUpgrades()
        {
            List<UpgradeData> datas = new List<UpgradeData>();
            
            datas.Add(upgradeDataTable.GetRandomUpgrade());
            datas.Add(upgradeDataTable.GetRandomUpgrade());
            datas.Add(upgradeDataTable.GetRandomUpgrade());
            datas.Add(upgradeDataTable.GetRandomUpgrade());
            
            generatedUpgrades.Enqueue(datas);
            upgradeRemain++;
            upgradeRemainText.text = upgradeRemain.ToString();
        }

        public void OnClickUpgradeButton()
        {
            if (cardUi.activeSelf)
            {
                cardUi.SetActive(false);
            }
            else if(upgradeRemain > 0)
            {
                SetCard();
            }
        }
    }
}


