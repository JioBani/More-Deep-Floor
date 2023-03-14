using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public enum HitType
    {
        None,
        CommonAttack,
        Skill,
        Etc
    }
    
    public class HitInfo
    {
        public Defender caster { get; private set; }
        public HitType HitType { get; private set; }
        public int damage {get; private set;}
    }
}


