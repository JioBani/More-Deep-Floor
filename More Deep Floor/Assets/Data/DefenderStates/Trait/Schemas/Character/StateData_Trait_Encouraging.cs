using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits //.
{
    [CreateAssetMenu(
        fileName = "Trait_Encouraging",
        //menuName = "Scriptable Object/Defender State Data/Trait/Job/"Trait_Encouraging,
        menuName = "Scriptable Object/Defender State Data/Trait/Character/Trait_Encouraging", 
    order = int.MaxValue)]

    public class StateData_Trait_Encouraging : TraitStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new TraitData_Encouraging(this, traitData , defender);
        }
    }

    public class TraitData_Encouraging : DefenderState
    {
        private Trait_Encouraging traitData;
        private StateData_Effect_Encouraging effectData;
        private int[] percent;
        private int level;
        
        private DefenderManager defenderManager;
        private List<Defender> defenders = null;
        
        public TraitData_Encouraging(DefenderStateData _stateData, TraitData _traitData, Defender _defender) : base(_stateData, _defender)
        {
            traitData = _traitData as Trait_Encouraging;
            effectData = traitData.Effect;
            percent = traitData.Percent;
        }
        public override void OnGenerated()
        {
            defenderManager = ReferenceManager.instance.defenderManager;
        }
        
        public override void OnDefenderPlaceChange(Defender target)
        {
            level = traitController.GetTraitInfo(traitData.TraitType).synergyLevel;
            
            if (defenders != null)
            {
                for (var i = 0; i < defenders.Count; i++)
                {
                    defenders[i].stateController.RemoveState(DefenderStateId.Effect_Encouraging);   
                }
            }

            defenders = defenderManager.GetNearDefenders(defender , 2);
            
            for (var i = 0; i < defenders.Count; i++)
            {
                Effect_Encouraging state = effectData.GetState(defenders[i]) as Effect_Encouraging;
                state.SetPercent(percent[level]);
                defenders[i].stateController.AddState(state);   
            }
        }

        public override void OffAction(Defender caster, Monster target)
        {
            for (var i = 0; i < defenders.Count; i++)
            {
                defenders[i].stateController.RemoveState(DefenderStateId.Effect_Encouraging);   
            }
        }
        
    }
}
