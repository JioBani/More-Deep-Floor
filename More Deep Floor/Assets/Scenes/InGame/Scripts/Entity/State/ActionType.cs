using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.States
{
    public enum ActionType
    {
        None = 0,
        OnSpawn = 1,
        OnOff = 2,
        OnUseSkill = 3,
        OnTargetHit = 4,
        OnBeforeAttack = 5,
        OnAfterAttack = 6,
        OnKill = 7,
        OnPlaceChange = 8,
        OnShieldBreak = 9,
        OnShieldTimeOut = 9,
    }
}
