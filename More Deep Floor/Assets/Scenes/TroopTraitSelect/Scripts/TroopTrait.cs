using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using UnityEngine;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class TroopTrait
    {
        public TroopTraitData traitData;
        public int level;

        public TroopTrait(TroopTraitData _traitData , int _level)
        {
            traitData = _traitData;
            level = _level;
        }

        public virtual void OnStageStartAction()
        {
            
        }

        public string GetDescription(int _level) => traitData.GetDescription(_level);
        
    }
}

