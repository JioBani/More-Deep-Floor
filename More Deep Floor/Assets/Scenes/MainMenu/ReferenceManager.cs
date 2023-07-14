using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.GoodsSystyem;
using UnityEngine;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class ReferenceManager : MonoBehaviour
    {
        public static ReferenceManager instance;
        
        //public TroopTraitManager troopTraitManager;
        public GoodsManager goodsManager;

        public void Awake()
        {
            if(instance == null) instance = this;
            else
            {
                Destroy(this);
            }
        }
    }
}


