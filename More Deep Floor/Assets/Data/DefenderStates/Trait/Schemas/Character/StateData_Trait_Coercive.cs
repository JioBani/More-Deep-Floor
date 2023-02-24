using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Coercive",
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Coercive",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class StateData_Trait_Coercive : TraitStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new TraitData_Coercive(this, traitData ,  defender);
        }
    }

    public class TraitData_Coercive : DefenderState
    {
        //private StateData_Trait_Coercive stateDataSpecific;
        private Trait_Coercive traitData;
        private float[] percents;
        private float[] time;
        
        
        public TraitData_Coercive(DefenderStateData _stateData, TraitData _traitData ,Defender _defender) : base(_stateData, _defender)
        {
            traitData = _traitData as Trait_Coercive;
            percents = traitData.Percent;
            time = traitData.Time;
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            int level = traitController.character.synergyLevel;
            if (!ProbabilityCheck.Check((int)percents[level], 100))
                return;
            
            target.SetStun(true);
            
            TimerManager.instance.LateAction(time[level] , () =>
            {
                target.SetStun(false);
            });
        }
    }
}
