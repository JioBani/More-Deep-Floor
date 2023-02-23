using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas //.
{
    [CreateAssetMenu(
        fileName = "TroopTrait_GoldAttack",
        menuName = "Scriptable Object/Defender State Data/TroopTrait/GoldAttack",
    //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
    order = int.MaxValue)]

    public class StateData_TroopTrait_GoldAttack : DefenderStateData
    {
        public float[] percentList;
        
        public override DefenderState GetState(Defender defender)
        {
            return new TroopTrait_GoldAttack(this, defender);
        }
    }

    public class TroopTrait_GoldAttack : DefenderState
    {
        private StateData_TroopTrait_GoldAttack stateDataSpecific;
        private MarketManager marketManager;
        public float[] percents;
        
        public TroopTrait_GoldAttack(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            stateDataSpecific = ((StateData_TroopTrait_GoldAttack)_stateData);
        }
        
        public override void OnGenerated()
        {
            marketManager = ReferenceManager.instance.marketManager;
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            if (ProbabilityCheck.Check(1,1))
            {
                marketManager.GoldChange(1 , "TroopTraitState_GoldAttack");
            }
        }
    }
}
