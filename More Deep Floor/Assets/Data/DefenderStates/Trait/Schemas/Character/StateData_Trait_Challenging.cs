using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
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

    public class StateData_Trait_Challenging : DefenderStateData
    {
        public float[] maxHpPer;
        
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Challenging(this, defender);
        }
    }

    public class Trait_Challenging : DefenderState
    {
        private StateData_Trait_Challenging stateDataSpecific;
        private float[] maxHpPer;
        
        public Trait_Challenging(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            stateDataSpecific = ((StateData_Trait_Challenging)_stateData);
            maxHpPer = stateDataSpecific.maxHpPer;
        }
        
        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            TimerManager.instance.LateAction(0.1f , () =>
            {
                if (target.gameObject.activeSelf)
                {
                    target.SetHitFinal(target.status.maxHp * 0.04f, caster);
                }
            });
        }
    }
}
