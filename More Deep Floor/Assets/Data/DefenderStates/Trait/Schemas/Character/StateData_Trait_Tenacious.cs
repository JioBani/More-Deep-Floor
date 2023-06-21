using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas //.
{
    [CreateAssetMenu(
        fileName = "Trait_Tenacious",
        //menuName = "Scriptable Object/Defender State Data/Trait/Job/Trait_Tenacious",
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Tenacious", 
        order = int.MaxValue)]

    public class StateData_Trait_Tenacious : TraitStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            Debug.LogWarning("[StateData_Trait_Tenacious] 정상적이지 않은 방법으로 생성됨");
            return new TraitState_Tenacious(this ,null , defender);
        }
    }

    public class TraitState_Tenacious : TraitState
    {
        private StateData_Trait_Tenacious stateDataSpecific;
        private Trait_Tenacious traitData;
        private RuntimeTrait_Tenacious runtimeTraitData;
        private TraitType traitType;
        public TraitState_Tenacious(DefenderStateData _stateData, RuntimeTrait_Tenacious _runtimeTraitData, Defender _defender) 
            : base(_stateData,_runtimeTraitData ,_defender)
        {
            runtimeTraitData = _runtimeTraitData;
            traitData = _runtimeTraitData.traitData as Trait_Tenacious;
            traitType = traitData.TraitType;
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            //Debug.Log("TraitState_Tenacious B: " + target.status.speed.currentValue);

            MonsterStatusBuff statusBuff = target.status.speed.AddBuff(
                -runtimeTraitData.currentPercent[traitController.GetTraitInfo(traitType).synergyLevel] * 0.01f *  target.status.speed.currentValue, 
                id.ToString());
            
            //Debug.Log("TraitState_Tenacious A: " + target.status.speed.currentValue);

            
            TimerManager.instance.LateAction(3f , () =>
            {
                target.status.speed.RemoveBuff(statusBuff);
            });
        }
    }
}