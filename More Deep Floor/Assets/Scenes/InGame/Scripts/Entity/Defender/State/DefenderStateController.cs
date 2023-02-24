using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.InGame.StateActions;
using LNK.MoreDeepFloor.InGame.TraitSystem;
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

        private DefenderStateController stateController;
        private TraitController traitController;

        public void Awake()
        {
            defender.OnKillAction += OnKill;
            defender.OnTargetHitAciton += OnTargetHit;
            defender.OnUseSkillAction += OnUseSkill;
            defender.OnBeforeOriginalAttackAction += BeforeOriginalAttack;
            defender.OnBeforeAttackAction += BeforeAttack;
            ReferenceManager.instance.defenderManager.OnDefenderPlaceChangeAction += OnDefenderPlaceChange;
            
            stateController = GetComponent<DefenderStateController>();
            traitController = GetComponent<TraitController>();
        }

        public void Init()
        {
            stateList = new Dictionary<DefenderStateId, DefenderState>();
            foreach (var defenderStateList in stateSortByType)
            {
                defenderStateList.Value.Reset();
            }
        }

        public DefenderState AddState(DefenderState newState)
        {
            if (stateList.TryGetValue(newState.id, out var state))
            {
                state.AddStack();
                state.OnAction(defender , null);
                OnStateChange();
                return state;
            }
            else
            {
                stateList[newState.id] = newState;
                stateSortByType[newState.actionType].Add(newState);
                newState.OnAction(defender, null);
                OnStateChange();
                return newState;
            }
        }
        
        public DefenderState AddState(DefenderStateId id)
        {
            Debug.Log($"AddState : {id}");
            if (stateList.TryGetValue(id, out var stateInfo))
            {
                stateInfo.AddStack();
                stateInfo.OnAction(defender , null);
                OnStateChange();
                return stateInfo;
            }
            else
            {
                DefenderState newState = ReferenceManager.instance.defenderStateList.Get(
                    id , 
                    defender , 
                    stateController,
                    traitController
                    );
                //newState.Set(defender,this);
                
                stateList[id] = newState;
                stateSortByType[newState.actionType].Add(newState);
                newState.OnAction(defender, null);
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
                stateSortByType[state.actionType].Remove(state);
                stateList.Remove(id);
            }

            state.OffAction(defender, null);
            
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
            stateSortByType[DefenderStateType.OnDefenderPlaceChange].OnDefenderPlaceChange(target);
        }
    }
}

