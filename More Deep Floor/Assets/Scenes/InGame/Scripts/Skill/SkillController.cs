using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.SkillSystem
{
    public class SkillController : MonoBehaviour
    {
        private SkillData skillData;
        private Defender caster;
        private SkillActionInfoBase skillAction;
    
        public void SetSkillData(Defender _caster ,SkillData _skillData , DefenderStateController stateController)
        {
            caster = _caster;
            skillData = _skillData;
            skillAction = SkillList.Get(skillData, caster,stateController);
        }
        
        public void UseSkill(List<Monster> target = null)
        {
            skillAction.Act(target);
        }
    }
}


