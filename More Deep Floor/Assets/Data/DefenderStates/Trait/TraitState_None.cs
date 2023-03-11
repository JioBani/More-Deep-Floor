using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;


namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits
{
    public class TraitState_None : TraitState
    {
        public TraitState_None(DefenderStateData _stateData, RuntimeTraitData _runtimeTraitData, Defender _defender) 
            : base(_stateData, _runtimeTraitData, _defender)
        {
            
        }
    }
}