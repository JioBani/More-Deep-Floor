using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    public enum DefenderStateType
    {
        OnKill,
        Immediately,
        OnTargetHit,
        OnUseSkill,
        BeforeOriginalAttack,
        BeforeAttack,
        OnDefenderPlaceChange
    }
}
