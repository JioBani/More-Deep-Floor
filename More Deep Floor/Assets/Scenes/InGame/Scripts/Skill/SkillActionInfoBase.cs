using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defender;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.SkillSystem
{
    public abstract class SkillActionInfoBase
    {
        protected Defender caster;
        protected DefenderStateController stateController;
        protected SkillData skillData;

        public virtual void Set(SkillData _skillData, Defender _caster, DefenderStateController _stateController)
        {
            caster = _caster;
            skillData = _skillData;
            stateController = _stateController;
        }

        public abstract void Act(List<Monster> targets = null);
    }
}