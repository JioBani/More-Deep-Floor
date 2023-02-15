using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{

    public abstract class DefenderStateActionInfoBase
    {
        public DefenderStateId id;
        public DefenderStateType type;
        protected DefenderStateController stateController;
        protected DefenderStateData stateData;
        protected TraitController traitController;
        public Defender defender;

        public void Set(
            DefenderStateData _stateData,
            Defender _defender, 
            DefenderStateController _stateController,
            TraitController _traitController
            )
        {
            stateData = _stateData;
            stateController = _stateController;
            defender = _defender;
            traitController = _traitController;
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

        public void RemoveState()
        {
            stateController.RemoveState(id);
        }
    }
}


