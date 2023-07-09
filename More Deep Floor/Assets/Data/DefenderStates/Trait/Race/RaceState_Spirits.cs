using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Challenging",
        menuName = "Scriptable Object/Defender State Data/Trait/Race/Spirits",
        order = int.MaxValue)]

    public class StateData_Race_Spirits : TraitStateData
    {

        public override DefenderState GetState(Defender defender)
        {
            
            Logger.LogWarning("[StateData_Trait_Spirits]정상적이지 않은 방법으로 생성됨");
            return new TraitState_Spirits(this, null, defender);
        }
    }

    public class TraitState_Spirits : TraitState
    {
        private RuntimeRace_Spirits runtimeData;
        private Race_SpiritsData raceData;
        private TimerManager timerManager;
        //private bool isCoolTimeOver = true;

        public TraitState_Spirits(DefenderStateData _stateData, RuntimeRace_Spirits _runtimeData, Defender _defender) :
            base(_stateData, _runtimeData, _defender)
        {
            runtimeData = _runtimeData;
            raceData = _runtimeData.traitData as Race_SpiritsData;
        }

        /*public override void OnAction(Defender caster, Monster target)
        {
            
        }*/

        //#. 이벤트 함수 구현 필요
        void OnHealthChanged(Defender defender)
        {
            if (defender.status.currentHp < defender.status.maxHp.currentValue * runtimeData.TriggerHealthPercent)
            {
                // isCoolTimeOver = false;
                // defender.AddShield(쉴드량,제거시간,()=>{
                //  timerManager.LateAction(()=>{
                //      isCoolTimeOver = false;
                //  })
                // })
            }
        }

        //#. 효과 구현
    }
}