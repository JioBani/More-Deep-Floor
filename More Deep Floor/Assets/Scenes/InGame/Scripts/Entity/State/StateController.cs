using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.Entitys.States
{
    public class StateController : MonoBehaviour
    {
        private Entity self;

        private List<EntityState> states;

        private Dictionary<ActionType, EntityStateList> stateDic;

        public void Awake()
        {
            stateDic = new Dictionary<ActionType, EntityStateList>
            {
                [ActionType.OnSpawn] = new EntityStateList(),
                [ActionType.OnOff] = new EntityStateList(),
                [ActionType.OnUseSkill] = new EntityStateList(),
                [ActionType.OnTargetHit] = new EntityStateList(),
                [ActionType.OnBeforeAttack] = new EntityStateList(),
                [ActionType.OnAfterAttack] = new EntityStateList(),
                [ActionType.OnShieldBreak] = new EntityStateList(),
                [ActionType.OnShieldTimeOut] = new EntityStateList(),
                [ActionType.OnKill] = new EntityStateList() //TODO 이벤트 연결
            };
        }

        public EntityStateList EntityStateList(ActionType actionType)
        {
            return stateDic[actionType];
        }

        public void Reset()
        {
            states = new List<EntityState>();
            foreach (var entityStateList in stateDic)
            {
                entityStateList.Value.Reset();
            }
        }

        public void AddState(EntityState state)
        {
            states.Add(state);
            foreach (var actionType in state.actionTypes)
            {
                stateDic[actionType].Add(state);
            }
        }

        public void RemoveState(EntityState state)
        {
            states.Remove(state);
            foreach (var entityStateList in stateDic)
            {
                entityStateList.Value.Remove(state);
            }
        }
        
        public void RemoveState(string id)
        {
            EntityState entityState = states.Find((state) => state.id == id);
            states.Remove(entityState);
            foreach (var entityStateList in stateDic)
            {
                entityStateList.Value.Remove(entityState);
            }
        }


    }
}
