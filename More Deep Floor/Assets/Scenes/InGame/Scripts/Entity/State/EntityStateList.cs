using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public class EntityStateList
    {
        private List<EntityState> list;

        public void Reset()
        {
            list = new List<EntityState>();
        }
        public void Add(EntityState entityState)
        {
            list.Add(entityState);
        }

        public void Remove(EntityState state)
        {
            list.Remove(state);
        }

        #region #. 이벤트 함수

        public void OnSpawnAction()
        {
            foreach (var entityState in list)
            {
                entityState.OnSpawnAction();
            }
        }

        public virtual void OnOffAction(Entity self , Entity killer)
        {
            foreach (var entityState in list)
            {
                entityState.OnOffAction(killer);
            }
        }

        //public virtual void ActiveAction(Defender caster, Monster target){ }

        public virtual void OnUseSkillAction(List<Entity> targets)
        {
            foreach (var entityState in list)
            {
                entityState.OnUseSkillAction(targets);
            }
        }
        public virtual void OnTargetHitAction(Entity firer, Entity target, float damage)
        {
            foreach (var entityState in list)
            {
                entityState.OnTargetHitAction(target, damage);
            }
        }

        public virtual void OnBeforeAttackAction(Entity firer, Entity target , float damage)
        {
            foreach (var entityState in list)
            {
                entityState.OnBeforeAttackAction(target, damage);
            }
        }

        public virtual void OnAfterAttackAction(Entity firer, Entity target, float damage)
        {
            foreach (var entityState in list)
            {
                entityState.OnAfterAttackAction(target, damage);
            }
        }

        public virtual void OnKillAction(Entity killer, Entity target)
        {
            foreach (var entityState in list)
            {
                entityState.OnKillAction(target);
            }
        }
        
        /*public virtual void OnDefenderPlaceChange()
        {
            //TODO defenderManager의 PlaceChange에 연결
            foreach (var entityState in list)
            {
                entityState.OnDefenderPlaceChange();
            }
        }*/

        /*public virtual void OnShieldBreakAction(Entity self, float maxAmount)
        {
            foreach (var entityState in list)
            {
                entityState.OnShieldBreakAction(maxAmount);
            }
        }

        public virtual void OnShieldTimeOutAction(Entity self, float maxAmount , float leftAmount)
        {
            foreach (var entityState in list)
            {
                entityState.OnShieldTimeOutAction(maxAmount , leftAmount);
            }
        }*/

        #endregion
    }
}


