using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.StateActions.Skills;
using LNK.MoreDeepFloor.InGame.StateActions.Traits;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.StateActions
{
    public class DefenderStateActionList : MonoBehaviour
    {
        [SerializeField] private DefenderStateTable table;
        
        public DefenderState Get(DefenderStateId id)
        {
            DefenderStateActionInfoBase actionInfoBase;
            switch (id)
            {
                case DefenderStateId.None  : actionInfoBase = new State_None(); break;
                case DefenderStateId.Trait_Gladiator  : actionInfoBase = new Trait_Gladiator(); break;
                case DefenderStateId.Effect_Gladiator : actionInfoBase = new Effect_Gladiator(); break;
                case DefenderStateId.Trait_Challenging   : actionInfoBase = new Trait_Challenging(); break;
                case DefenderStateId.Trait_Researcher   : actionInfoBase = new Trait_Researcher(); break;
                case DefenderStateId.Trait_Coercive   : actionInfoBase = new Trait_Coercive(); break;
                case DefenderStateId.Trait_Encouraging   : actionInfoBase = new Trait_Encouraging(); break;
                case DefenderStateId.Effect_Encouraging   : actionInfoBase = new Effect_Encouraging(); break;
                case DefenderStateId.Trait_Greedy   : actionInfoBase = new Trait_Greedy(); break;
                case DefenderStateId.Trait_Sniper   : actionInfoBase = new Trait_Sniper(); break;
                case DefenderStateId.Trait_Circus   : actionInfoBase = new Trait_Circus(); break;
                
                case DefenderStateId.Skill_Bishop01   : actionInfoBase = new Bishop01_Buff(); break;
                case DefenderStateId.Skill_Knight01   : actionInfoBase = new Knight01_Buff(); break;
                
                default : 
                    actionInfoBase = new State_None(); 
                    Debug.LogError($"[DefenderStateActionList.Get()] 상태 없음 : {id}");
                    break;
            }
            
            return new DefenderState(id, table.Get(id) ,actionInfoBase);
        }
    }
}


