using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
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

    public class StateData_Trait_Coercive : DefenderStateData
    {
        public int[] percents;
        public float[] time;
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Coercive(this, defender);
        }
    }

    public class Trait_Coercive : DefenderState
    {
        private StateData_Trait_Coercive stateDataSpecific;
        public int[] percents;
        public float[] time;
        
        
        public Trait_Coercive(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            stateDataSpecific = ((StateData_Trait_Coercive)_stateData);
            percents = stateDataSpecific.percents;
            time = stateDataSpecific.time;
        }
        
        public override void ActiveAction(Defender caster, Monster target)
        {
            int level = traitController.character.synergyLevel;
            
            int trigger = Random.Range(1,101);
            if (trigger <= percents[level])
            {
                target.SetStun(true);
                TimerManager.instance.LateAction(time[level] , () =>
                {
                    target.SetStun(false);
                });
            }
           
        }
    }
}
