using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.SkillSystem
{
    public static class SkillList
    {
        public static SkillActionInfoBase Get(
            SkillData skillData,
            Entity caster , 
            StateController stateController
        )
        {
            SkillActionInfoBase skillActionInfoBase;
                
            switch (skillData.Id)
            {
                //case DefenderId.Knight_Cost1 : skillActionInfoBase = new Knight01(); break;
                case DefenderId.라이즈 : skillActionInfoBase = new Rock01(); break;
                //case DefenderId.Bishop_Cost1 : skillActionInfoBase = new Bishop01(); break;
                default: skillActionInfoBase = new None(); break;
            }
                
            skillActionInfoBase.Set(skillData , caster , stateController);
            return skillActionInfoBase;
        }
    }
}


