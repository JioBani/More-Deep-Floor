using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States
{
    public enum DefenderStateId
    {
        None = 0,
        //#. 특성
        Trait_Gladiator = 1,
        Effect_Gladiator = 2,
        Trait_Challenging = 3,
        Trait_Coercive = 4,
        Trait_Researcher = 5,
        Trait_Encouraging = 6,
        Trait_Greedy = 7,
        Trait_Sniper = 8,
        Trait_Circus = 9,
        Effect_Encouraging = 10,
        Trait_Tenacious = 11,
        TroopTrait_GoldAttack = 12,
        
        Upgrade_Gladiator_Furious = 13,

        #region 특성

        //#. 성격 
        Personality_None = 1000, //
        Personality_Moody = 1001, //#. 기분파
        Personality_Careful = 1002, //#. 꼼꼼한
        Personality_Genius = 1003, //#. 천재
        Personality_Prickly = 1004, //#. 가시돋친
        Personality_Menacing = 1005, //#. 공포스러운
        Personality_Oppressive = 1006, //#. 강압적인 
        
        
        //#. 직업
        Job_None = 2000, // 
        Job_Circus = 2001, //#. 서커스
        Job_Alchemist = 2002, //#. 연금술사
        Job_Magician = 2003, //#. 마술사
        Job_Guard = 2004, //#. 기사
        Job_Knight = 2005, //#. 기사
        Job_Thief = 2006 //#. 도적

        #endregion

        //#. 성격
        


    }
}


