using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Challenging",
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Challenging",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Challenging", 
        order = int.MaxValue)]

    public class StateData_Trait_Challenging : TraitStateData
    {
        public float[] maxHpPer;
        
        public override DefenderState GetState(Defender defender)
        {
            return new TraitState_Challenging(this, traitData,  defender);
        }
    }

    public class TraitState_Challenging : DefenderState
    {
        private Trait_Challenging traitData;
        private float[] maxHpPer;
        
        public TraitState_Challenging(DefenderStateData _stateData, TraitData _traitData , Defender _defender) : base(_stateData, _defender)
        {
            traitData = _traitData as Trait_Challenging;
            maxHpPer = traitData.MaxHpPer;
        }
        
        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            int level = traitController.GetTraitInfo(traitData.TraitType).synergyLevel;
            
            TimerManager.instance.LateAction(0.1f , () =>
            {
                if (target.gameObject.activeSelf)
                {
                    target.SetHitFinal(target.status.maxHp * maxHpPer[level] * 0.01f, caster);
                }
            });
        }
    }
}
