using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.SkillSystem
{
    public static class SkillList
    {
        public static SkillActionInfoBase Get(
            SkillData skillData,
            Defender caster , 
            DefenderStateController stateController
        )
        {
            SkillActionInfoBase skillActionInfoBase;
                
            switch (skillData.Id)
            {
                /*case DefenderId.Knight_Cost1 : skillActionInfoBase = new Knight01(); break;
                case DefenderId.Aartrox : skillActionInfoBase = new Rock01(); break;
                case DefenderId.Bishop_Cost1 : skillActionInfoBase = new Bishop01(); break;*/
                default: skillActionInfoBase = new Rock01(); break;
            }
                
            skillActionInfoBase.Set(skillData , caster , stateController);
            return skillActionInfoBase;
        }
    }
}


