using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Bullets
{
    public class AttackInfo
    {
        public delegate void OnMonsterHitEventHandler(Monster monster);
        
        public int damage;
        public Defender caster;
        public OnMonsterHitEventHandler OnMonsterHitAction = null;

        public AttackInfo(Defender _caster , int _damage , OnMonsterHitEventHandler _OnMonsterHitAction)
        {
            caster = _caster;
            damage = _damage;
            OnMonsterHitAction = _OnMonsterHitAction;
        }
    }
}


