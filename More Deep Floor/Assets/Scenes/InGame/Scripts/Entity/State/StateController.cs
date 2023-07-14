using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.States
{
    [Serializable]
    public class StateController : MonoBehaviour
    {
        private Entity self;

        private List<EntityState> states;

        private Dictionary<ActionType, EntityStateList> stateDic;

        public delegate void OnStateChangeEventHandler(List<EntityState> states);
        public OnStateChangeEventHandler OnStateChangeAction;

        public void Awake()
        {
            stateDic = new Dictionary<ActionType, EntityStateList>
            {
                [ActionType.OnOn] = new EntityStateList(),
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

        public EntityState AddState(EntityState state)
        {
            CustomLogger.Log($"[StateController.AddState()] state = {state.entityStateData.StateId}");
            states.Add(state);
            foreach (var actionType in state.actionTypes)
            {
                stateDic[actionType].Add(state);
            }

            if (state.actionTypes.Contains(ActionType.OnOn))
            {
                state.OnOnAction(self);
            }
            
            OnStateChangeAction?.Invoke(states);
            return state;
        }

        public void RemoveState(EntityState state)
        {
            states.Remove(state);
            foreach (var entityStateList in stateDic)
            {
                entityStateList.Value.Remove(state);
            }
            
            state.OnOffAction();
            
            OnStateChangeAction?.Invoke(states);
            CustomLogger.Log($"[StateController.RemoveState()] {states.Count}");
        }
        
        public void RemoveState(string id)
        {
            /*EntityState entityState = states.Find((state) => state.id == id);
            states.Remove(entityState);
            foreach (var entityStateList in stateDic)
            {
                entityStateList.Value.Remove(entityState);
            }*/
        }


    }
}
