using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    public class DefenderStateList
    {
        private DefenderStateType type;
        private List<DefenderState> stateList;

        public DefenderStateList(DefenderStateType _type)
        {
            type = _type;
            stateList = new List<DefenderState>();
        }

        public void Add(DefenderState state)
        {
            stateList.Add(state);
        }

        public void Remove(DefenderState state)
        {
            stateList.Remove(state);
        }

        public void Action(Defender caster, Monster target)
        {
            for (var i = 0; i < stateList.Count; i++)
            {
                stateList[i].ActiveAction(caster,target);
            }
        }

        public void SkillAction(Defender caster, Monster target, bool isFinal)
        {
            for (var i = 0; i < stateList.Count; i++)
            {
                stateList[i].OnUseSkillAction(caster,target,isFinal);
            }
        }

        public void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            for (var i = 0; i < stateList.Count; i++)
            {
                stateList[i].OnTargetHitAction(caster,target,damage);
            }
        }

        public void BeforeAttackAction(Monster target, DefenderStateId id)
        {
            for (var i = 0; i < stateList.Count; i++)
            {
                stateList[i].OnBeforeAttackAction(target,id);
            }
        }
        
        public void BeforeOriginalAttackAction(Monster target, DefenderStateId id)
        {
            for (var i = 0; i < stateList.Count; i++)
            {
                stateList[i].OnBeforeOriginalAttackAction(target,id);
            }
        }

        public void OnDefenderPlaceChange(Defender target)
        {
            for (var i = 0; i < stateList.Count; i++)
            {
                stateList[i].OnDefenderPlaceChange(target);
            }
        }

        public void Reset()
        {
            stateList = new List<DefenderState>();
        }
    }
}

