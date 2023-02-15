using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.InGame.StateActions;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    public class DefenderStateController : MonoBehaviour
    {
        private Dictionary<DefenderStateId, DefenderState> stateList;
        
        private Dictionary<DefenderStateType, DefenderStateList> stateSortByType = new Dictionary<DefenderStateType, DefenderStateList>()
        {
            {DefenderStateType.OnKill ,  new DefenderStateList(DefenderStateType.OnKill)},
            {DefenderStateType.Immediately ,  new DefenderStateList(DefenderStateType.Immediately)},
            {DefenderStateType.OnTargetHit ,  new DefenderStateList(DefenderStateType.OnTargetHit)},
            {DefenderStateType.OnUseSkill ,  new DefenderStateList(DefenderStateType.OnUseSkill)},
            {DefenderStateType.BeforeOriginalAttack ,  new DefenderStateList(DefenderStateType.BeforeOriginalAttack)},
            {DefenderStateType.BeforeAttack ,  new DefenderStateList(DefenderStateType.BeforeAttack)},
            {DefenderStateType.OnDefenderPlaceChange , new DefenderStateList(DefenderStateType.OnDefenderPlaceChange)}
        };
        
        [SerializeField] private Defender defender;
        [SerializeField] private TextMeshPro stateText;

        public void Awake()
        {
            defender.OnKillAction += OnKill;
            defender.OnTargetHitAciton += OnTargetHit;
            defender.OnUseSkillAction += OnUseSkill;
            defender.OnBeforeOriginalAttackAction += BeforeOriginalAttack;
            defender.OnBeforeAttackAction += BeforeAttack;
            ReferenceManager.instance.defenderManager.OnDefenderPlaceChangeAction += OnDefenderPlaceChange;
        }

        public void Init()
        {
            stateList = new Dictionary<DefenderStateId, DefenderState>();
            foreach (var defenderStateList in stateSortByType)
            {
                defenderStateList.Value.Reset();
            }
        }

        public void AddState(DefenderState state)
        {
            if (stateList.TryGetValue(state.id, out var stateInfo))
            {
                stateInfo.AddStack();
                stateInfo.actionInfo.OnAction(defender , null);
            }
            else
            {
                state.Set(defender,this);
                
                stateList[state.id] = state;
                stateSortByType[state.actionInfo.type].Add(state);
                state.actionInfo.OnAction(defender, null);
            }
        }
        
        public DefenderState AddState(DefenderStateId id)
        {
            Debug.Log($"AddState : {id}");
            if (stateList.TryGetValue(id, out var stateInfo))
            {
                stateInfo.AddStack();
                stateInfo.actionInfo.OnAction(defender , null);
                OnStateChange();
                return stateInfo;
            }
            else
            {
                DefenderState newState = ReferenceManager.instance.defenderStateActionList.Get(id);
                newState.Set(defender,this);
                
                stateList[id] = newState;
                stateSortByType[newState.actionInfo.type].Add(newState);
                newState.actionInfo.OnAction(defender, null);
                OnStateChange();
                return newState;
            }
        } 

        public void RemoveState(DefenderStateId id)
        {
            if (!stateList.TryGetValue(id, out DefenderState state))
            {
                Debug.LogWarning($"[DefenderStateController.RemoveState()] 상태 없음 : " +
                                 $"Defender = {defender.status.defenderData.spawnId}" +
                                 $"DefenderStateId = {id}");
                return;
            }
            
            if (!stateList[id].RemoveStack())
            {
                stateSortByType[state.actionInfo.type].Remove(state);
                stateList.Remove(id);
            }

            state.actionInfo.OffAction(defender, null);
            
            OnStateChange();
        }

        void OnStateChange()
        {
            string str = "";
            foreach (var state in stateList)
            {
                str += state.Value.id + ":" + state.Value.stack+ "\n";
            }

            stateText.text = str;
        }
        
        void OnKill(Monster target)
        {
            stateSortByType[DefenderStateType.OnKill].Action(defender , target);
        }

        void OnTargetHit(Monster target , int damage)
        {
            stateSortByType[DefenderStateType.OnTargetHit].OnTargetHitAction(defender , target , damage);
        }

        void OnUseSkill(Monster target , bool isFinal)
        {
            stateSortByType[DefenderStateType.OnUseSkill].SkillAction(defender , target, isFinal);
        }

        void BeforeAttack(Monster target, DefenderStateId id)
        {
            stateSortByType[DefenderStateType.BeforeAttack].BeforeAttackAction(target,id);
        }

        void BeforeOriginalAttack(Monster target, DefenderStateId id)
        {
            stateSortByType[DefenderStateType.BeforeOriginalAttack].BeforeOriginalAttackAction(target,id);
        }

        void OnDefenderPlaceChange(Defender target)
        {
            Debug.Log("DefenderStateController : OnDefenderPlaceChange");
            stateSortByType[DefenderStateType.OnDefenderPlaceChange].OnDefenderPlaceChange(target);
        }
    }
}

