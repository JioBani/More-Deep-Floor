using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    public abstract class DefenderState
    {
        public DefenderStateId id;
        public DefenderStateData stateData;
        public Defender defender;
        public DefenderStateType actionType;

        public int stack { protected set; get; }
        protected DefenderStateController stateController;
        protected TraitController traitController;
        
        public DefenderState(
            DefenderStateData _stateData, 
            Defender _defender
            )
        {
            stateData = _stateData;
            stack = 1;
            defender = _defender;
            id = _stateData.Id;
            actionType = _stateData.ActionType;
            stateController = defender.stateController;
            traitController = defender.GetComponent<TraitController>();
            
            OnGenerated();
        }

        public DefenderState()
        {
            
        }
        
        public virtual void OnGenerated()
        {
            
        }

        public virtual void OnAction(Defender caster, Monster target){ }

        public virtual void OffAction(Defender caster, Monster target){ }

        public virtual void ActiveAction(Defender caster, Monster target){ }

        public virtual void OnUseSkillAction(Defender caster, Monster target, bool isFinal){}

        public virtual void OnTargetHitAction(Defender caster, Monster target, int damage) {}
        
        public virtual void OnBeforeAttackAction(Monster target, DefenderStateId from){}
        
        public virtual void OnBeforeOriginalAttackAction(Monster target, DefenderStateId from){}

        public virtual void OnDefenderPlaceChange(Defender target)
        {
            Debug.LogWarning($"[DefenderState.OnDefenderPlaceChange()] 정상적이지 않은 접근 : {id}");
        }

        
        public bool RemoveStack()
        {
            stack--;
                     
            if (stack <= 0)
            {
                return false;
            }
            return true;
        }
         
        public void AddStack()
        {
            stack++;
        }
        
        public void RemoveState()
        {
            stateController.RemoveState(id);
        }
    }
}

