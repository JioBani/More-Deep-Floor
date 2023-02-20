using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.StateActions.Traits;
using LNK.MoreDeepFloor.InGame.StateActions.TroopTrait;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.StateActions
{
    public class DefenderStateList : MonoBehaviour
    {
        [SerializeField] private DefenderStateTable table;
        
        public DefenderState Get(
            DefenderStateId id , 
            Defender defender, 
            DefenderStateController stateController, 
            TraitController traitController
        )
        {
            DefenderState state;
            DefenderStateData stateData = table.Get(id);
            switch (id)
            {
                case DefenderStateId.None  : state = new State_None(id , stateData, defender); break;
                case DefenderStateId.Trait_Gladiator  : state = new Trait_Gladiator(id , stateData , defender); break;
                case DefenderStateId.Effect_Gladiator : state = new Effect_Gladiator(id , stateData , defender); break;
                case DefenderStateId.Trait_Challenging   : state = new Trait_Challenging(id , stateData , defender); break;
                case DefenderStateId.Trait_Researcher   : state = new Trait_Researcher(id , stateData , defender); break;
                case DefenderStateId.Trait_Coercive   : state = new Trait_Coercive(id , stateData , defender); break;
                case DefenderStateId.Trait_Encouraging   : state = new Trait_Encouraging(id , stateData , defender); break;
                case DefenderStateId.Effect_Encouraging   : state = new Effect_Encouraging(id , stateData , defender); break;
                case DefenderStateId.Trait_Greedy   : state = new Trait_Greedy(id , stateData , defender); break;
                case DefenderStateId.Trait_Sniper   : state = new Trait_Sniper(id , stateData , defender); break;
                case DefenderStateId.Trait_Circus   : state = new Trait_Circus(id , stateData , defender); break;
                
                case DefenderStateId.TroopTrait_GoldAttack   : state = new TroopTraitState_GoldAttack(id , stateData , defender); break;
                
                //case DefenderStateId.Skill_Bishop01   : state = new Bishop01_Buff(); break;
                //case DefenderStateId.Skill_Knight01   : state = new Knight01_Buff(); break;
           
                default : 
                    state = new State_None(id , stateData, defender);; 
                    Debug.LogError($"[DefenderStateActionList.Get()] 상태 없음 : {id}");
                    break;
            }

            return state;
        }
    }
}


