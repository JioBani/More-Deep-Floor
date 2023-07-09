using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders.States
{
    public abstract class DefenderState : EntityState
    {
        public DefenderStateId id;
        public DefenderStateData stateData;
        public Defender defender;
        public DefenderStateType actionType;

        //public int stack { protected set; get; }
        //protected StateController stateController;
        protected TraitController traitController;

        public DefenderState(string id, Entity _self) : base(id , _self)
        {
            
        }
        
        public DefenderState(
            DefenderStateData _stateData, 
            Defender _defender
            ) : base(_id:_stateData.StateName , _self:_defender)
        {
            stateData = _stateData;
            stack = 1;
            defender = _defender;
            id = _stateData.Id;
            actionType = _stateData.ActionType;
            //stateController = defender.stateController;
            traitController = defender.GetComponent<TraitController>();
            
            OnGenerated();
        }

        public virtual void OnGenerated()
        {
            
        }

        /*public virtual void OnAction(Defender caster, Monster target){ }

        public virtual void OffAction(Defender caster, Monster target){ }

        public virtual void ActiveAction(Defender caster, Monster target){ }

        public virtual void OnUseSkillAction(Defender caster, Monster target, bool isFinal){}

        public virtual void OnTargetHitAction(Defender caster, Monster target, int damage) {}
        
        public virtual void OnBeforeAttackAction(Monster target, DefenderStateId from){}
        
        public virtual void OnBeforeOriginalAttackAction(Monster target, DefenderStateId from){}

        public virtual void OnKillAction(Monster target){}
        
        public virtual void OnDefenderPlaceChange(Defender target)
        {
            Debug.LogWarning($"[DefenderState.OnDefenderPlaceChange()] 정상적이지 않은 접근 : {id}");
        }

        public virtual void OnShieldBreakAction(float maxAmount)
        {
        }

        public virtual void OnShieldTimeOutAction(float maxAmount , float leftAmount)
        {
            
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
        }*/
        
        /*public void RemoveState()
        {
            stateController.RemoveState(id);
        }*/
    }
}

