using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Field.Ui
{
    public class DefenderNumberText : MonoBehaviour
    {
        private DefenderManager defenderManager;
        private TextMeshPro limitText;

        private void Awake()
        {
            limitText = GetComponent<TextMeshPro>();
            defenderManager = ReferenceManager.instance.defenderManager;
            defenderManager.OnBattleLimitInitAction += OnInit;
            defenderManager.OnBattleFieldDefenderChangeAction += Refresh;
        }

        void OnInit(int limit)
        {
            limitText.text = "0 / " + limit;
        }

        void Refresh(int limit, List<Defender> defenders)
        {
            limitText.text = defenders.Count + " / " + limit;
        }
    }

}
