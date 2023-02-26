using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
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
            return new TraitState_Tenacious(this ,traitData , defender);
        }
    }

    public class TraitState_Tenacious : DefenderState
    {
        private StateData_Trait_Tenacious stateDataSpecific;
        private Trait_Tenacious traitData;
        private TraitType traitType;
        public TraitState_Tenacious(DefenderStateData _stateData, TraitData _traitData, Defender _defender) : base(_stateData, _defender)
        {
            traitData = _traitData as Trait_Tenacious;
            traitType = traitData.TraitType;
        }

        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            //Debug.Log("TraitState_Tenacious B: " + target.status.speed.currentValue);

            MonsterStatusBuff statusBuff = target.status.speed.AddBuff(
                -traitData.Percent[traitController.GetTraitInfo(traitType).synergyLevel] * 0.01f *  target.status.speed.currentValue, 
                id.ToString());
            
            //Debug.Log("TraitState_Tenacious A: " + target.status.speed.currentValue);

            
            TimerManager.instance.LateAction(3f , () =>
            {
                target.status.speed.RemoveBuff(statusBuff);
            });
        }
    }
}