using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.States
{

    public abstract class EntityState
    {
        public string id { protected set; get; }
        public Entity self { protected set; get; }
        public int stack { protected set; get; }

        public List<ActionType> actionTypes { protected set; get; }
        private StateController stateController;

        public EntityState(string _id , Entity _self)
        {
            id = _id;
            self = _self;
            stack = 1;
            stateController = self.stateController;
        }

        #region #. 이벤트 함수

        public virtual void OnSpawnAction(){ }

        public virtual void OnOffAction(Entity killer){ }

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


