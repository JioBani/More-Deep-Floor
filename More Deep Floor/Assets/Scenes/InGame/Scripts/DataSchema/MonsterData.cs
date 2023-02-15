using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class MonsterData
    {
        public string name;
        public int hp;
        public float moveSpeed;
        public int gold;
        public AnimatorOverrideController animatorOverrideController;
        
        public MonsterData(MonsterOriginalData monsterOriginalData)
        {
            name = monsterOriginalData.Name;
            hp = monsterOriginalData.Hp;
            moveSpeed = monsterOriginalData.MoveSpeed;
            gold = monsterOriginalData.Gold;
            animatorOverrideController = monsterOriginalData.animatorOverrideController;
        }

        public MonsterData(string _name, int _hp, float _moveSpeed, int _gold)
        {
            name = _name;
            hp = _hp;
            moveSpeed = _moveSpeed;
            gold = _gold;
        }

        public MonsterData Clone()
        {
            return new MonsterData(name, hp, moveSpeed,gold);
        }
    }
}
