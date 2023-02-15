using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.GoodsSystyem;
using UnityEngine;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class TroopTraitManager : MonoBehaviour
    {
        private GoodsManager goodsManager;

        private void Awake()
        {
            goodsManager = ReferenceManager.instance.goodsManager;
        }

        public bool TryUpgrade(TroopTrait data)
        {
            if (goodsManager.TryBuy(data.traitData.GetPrice(data.level + 1)))
            {
                data.level++;
                return true;
            }
            else
            {
                return false;
            }
        } 
        
        /*public bool TryUpgrade10(TroopTraitSaveData data)
        {
            
        }*/
    }
}


