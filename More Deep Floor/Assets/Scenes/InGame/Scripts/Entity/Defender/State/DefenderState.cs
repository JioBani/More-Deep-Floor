using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    /*public class DefenderState
    {
        public DefenderStateId id;
        public int stack;
        public DefenderStateData stateData;
        public DefenderStateActionInfoBase actionInfo;
        private DefenderStateController stateController;

        public DefenderState(DefenderStateId _id,DefenderStateData _stateData, DefenderStateActionInfoBase _actionInfo)
        {
            stateData = _stateData;
            id = _id;
            stack = 1;
            actionInfo = _actionInfo;
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

        public void Set(Defender defender, DefenderStateController _stateController)
        {
            stateController = _stateController;
            actionInfo.Set(stateData,defender,_stateController,defender.GetComponent<TraitController>());
        }

        public void RemoveState()
        {
            stateController.RemoveState(id);
        }
    }*/

    public class DefenderState
    {
        public DefenderStateId id;
        public DefenderStateData stateData;
        public Defender defender;
        public DefenderStateType type;

        public int stack { protected set; get; }
        protected DefenderStateController stateController;
        protected TraitController traitController;
        
        public DefenderState(
            DefenderStateId _id,
            DefenderStateData _stateData, 
            Defender _defender
            )
        {
            stateData = _stateData;
            id = _id;
            stack = 1;
            defender = _defender;

            stateController = defender.stateController;
            traitController = defender.GetComponent<TraitController>();
            
            OnGenerated();
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
        public virtual void OnDefenderPlaceChange(Defender target){}

        
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

