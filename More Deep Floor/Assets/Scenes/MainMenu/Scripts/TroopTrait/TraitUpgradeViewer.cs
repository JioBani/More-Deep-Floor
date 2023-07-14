/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class TraitUpgradeViewer : MonoBehaviour
    {
        private TroopTraitManager troopTraitManager;
        
        public TroopTrait currentData;
        private int level;
        
        
        [SerializeField] private Text currentLevel;
        [SerializeField] private Text currentDescription;
        
        [SerializeField] private Text nextLevel;
        [SerializeField] private Text nextDescription;


        public void Awake()
        {
            troopTraitManager = ReferenceManager.instance.troopTraitManager;
        }

        public void SetData(TroopTrait _currentData)
        {
            currentData = _currentData;
            Refresh();
        }

        void Refresh()
        {
            level = currentData.level;

            currentLevel.text = "Lv " + level;
            nextLevel.text = "Lv " + (level + 1);
            
            currentDescription.text = currentData.GetDescription(level);
            nextDescription.text = currentData.GetDescription(level + 1);
        }

        public void OnClickUpgrade()
        {
            if (troopTraitManager.TryUpgrade(currentData))
            {
                Refresh();
            }
        }
        
        public void OnClickUpgrade10()
        {
            
        }
    }
}
*/


