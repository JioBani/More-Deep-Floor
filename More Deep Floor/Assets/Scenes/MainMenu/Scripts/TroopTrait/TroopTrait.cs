using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public enum TroopTraitType
    {
        None,
        OnDataLoad,
        OnRoundEnd,
    }
    
    public class TroopTrait
    {
        public TroopTraitData traitData;
        public int level;
        public TroopTraitType type = TroopTraitType.None;

        public TroopTrait(TroopTraitData _traitData , int _level)
        {
            traitData = _traitData;
            level = _level;
        }

        /*public virtual void OnStageStartAction()
        {
            
        }*/

        public virtual void OnDataLoadAction()
        {
            
        }

        public virtual void OnDefenderSpawn(Defender defender)
        {
            
        }

        public string GetDescription(int _level) => traitData.GetDescription(_level);
        
    }
}

