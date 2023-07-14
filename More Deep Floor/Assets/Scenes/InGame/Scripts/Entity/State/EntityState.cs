using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LNK.MoreDeepFloor.Data.EntityStates;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.States
{

    [Serializable]
    public abstract class EntityState
    {
        //public EntityStateId id { protected set; get; }
        public Entity self { protected set; get; }
        public int stack { protected set; get; }

        public EntityStateData entityStateData { get; private set; }

        [SerializeField] public List<ActionType> actionTypes { protected set; get; }
        private StateController stateController;

        public EntityState(EntityStateData _entityStateData ,Entity _self)
        {
            self = _self;
            entityStateData = _entityStateData;
            actionTypes = entityStateData.ActionTypes;
            stack = 1;
            stateController = _self.stateController;
        }


        #region #. 이벤트 함수

        public virtual void OnOnAction(Entity _self){ }

        public virtual void OnOffAction(){ }

        //public virtual void ActiveAction(Defender caster, Monster target){ }

        public virtual void OnUseSkillAction(List<Entity> targets){}

        public virtual void OnTargetHitAction(Entity target, float damage) {}
        
        public virtual void OnBeforeAttackAction(Entity target , float damage){}
        
        public virtual void OnAfterAttackAction(Entity target , float damage){}
        
        public virtual void OnKillAction(Entity target){}
        
        /*public virtual void OnDefenderPlaceChange()
        {
            Debug.LogWarning($"[DefenderState.OnDefenderPlaceChange()] 정상적이지 않은 접근 : {id}");
        }*/

        /*public virtual void OnShieldBreakAction(float maxAmount)
        {
            
        }

        public virtual void OnShieldTimeOutAction(float maxAmount , float leftAmount)
        {
            
        }*/
        
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
            self.stateController.RemoveState(this);
            //TODO self.stateController.RemoveState(this);
        }

        #endregion
    }
}


