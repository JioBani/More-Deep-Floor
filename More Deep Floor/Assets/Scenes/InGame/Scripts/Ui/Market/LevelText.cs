using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.Market
{
    public class LevelText : MonoBehaviour
    {
        Text levelText;
        
        private void Awake()
        {
            ReferenceManager.instance.marketManager.OnLevelUpAction += OnLevelUp;
            levelText = GetComponent<Text>();
        }

        void OnLevelUp(int level)
        {
            levelText.text = "Lv " + level;
        }
    }
}

