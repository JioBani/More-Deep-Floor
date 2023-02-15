using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class GoldText : MonoBehaviour
    {
        [SerializeField] private Text goldText;
        private void Awake()
        {
            ReferenceManager.instance.goodsManager.OnGoodsInitAction += OnGoodsInit;
            ReferenceManager.instance.goodsManager.OnGoldChangeAction += OnGoldChange;
        }

        void OnGoodsInit(int gold)
        {
            goldText.text = gold.ToString();
        }

        void OnGoldChange(int change, int gold)
        {
            goldText.text = gold.ToString();
        }
    }
}


