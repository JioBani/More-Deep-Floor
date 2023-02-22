using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class MonsterNumberText : MonoBehaviour
    {
        private Text numberText;

        private void Awake()
        {
            numberText = GetComponent<Text>();
            ReferenceManager.instance.monsterManager.OnMonsterNumberChangeAction += RefreshText;
        }

        void RefreshText(int number)
        {
            numberText.text = $"{number} / 100";
        }
    }
}

