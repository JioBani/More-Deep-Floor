using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders.States
{
    public abstract class TraitState : DefenderState
    {
        public TraitState(DefenderStateData _stateData, RuntimeTraitData _runtimeTraitData, Defender _defender) : base(_stateData, _defender)
        {
            
        }
        
        /*public override void OnDefenderPlaceChange(Defender target)
        {
            Debug.LogWarning($"[DefenderState.OnDefenderPlaceChange()] 정상적이지 않은 접근 : {id}");
        }*/
    }
}


