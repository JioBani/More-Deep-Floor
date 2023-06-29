using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Challenging",
        menuName = "Scriptable Object/Defender State Data/Trait/Personality/Personality_Moody",
        order = int.MaxValue)]

    public class StateData_Personality_Moody : TraitStateData
    {

        public override DefenderState GetState(Defender defender)
        {
            Logger.LogWarning("[StateData_Trait_Moody]정상적이지 않은 방법으로 생성됨");
            return new PersonalityState_Moody(this, null, defender);
        }
    }

    public class PersonalityState_Moody : TraitState
    {
        private Personality_Moody traitData;
        private RuntimePersonality_Moody runtimeData;
         

        public PersonalityState_Moody(DefenderStateData _stateData, RuntimePersonality_Moody _runtimeData, Defender _defender) :
            base(_stateData, _runtimeData, _defender)
        {
            runtimeData = _runtimeData;
            traitData = _runtimeData.traitData as Personality_Moody;
        }

        //#. 효과 구현

        public override void OnKillAction(Monster target)
        {
            
        }
    }
}