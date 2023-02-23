using System.Collections;
using System.Collections.Generic;
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

    public class StateData_Trait_Encouraging : DefenderStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Encouraging(this, defender);
        }
    }

    public class Trait_Encouraging : DefenderState
    {
        private StateData_Trait_Encouraging stateDataSpecific;
        
        private DefenderManager defenderManager;
        private List<Defender> defenders = null;
        
        public Trait_Encouraging(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {
            stateDataSpecific = ((StateData_Trait_Encouraging)_stateData);
        }
        public override void OnGenerated()
        {
            defenderManager = ReferenceManager.instance.defenderManager;
        }
        
        public override void OnDefenderPlaceChange(Defender target)
        {
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
                defenders[i].stateController.AddState(DefenderStateId.Effect_Encouraging);   
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
