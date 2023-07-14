using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.SkillSystem
{
    public abstract class SkillActionInfoBase
    {
        protected Entity caster;
        protected StateController stateController;
        protected SkillData skillData;

        public virtual void Set(SkillData _skillData, Entity _caster, StateController _stateController)
        {
            caster = _caster;
            skillData = _skillData;
            stateController = _stateController;
        }

        public abstract void Act(List<Monster> targets = null);
    }
}