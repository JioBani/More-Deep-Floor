using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.DataSchema
{
    public class MonsterData : EntityData
    {
        public int hp;
        public int gold;
        public AnimatorOverrideController animatorOverrideController;

        public MonsterData(MonsterOriginalData monsterOriginalData) : base(monsterOriginalData)
        {
            //name = monsterOriginalData.Name;
            //hp = monsterOriginalData.Hp;
            //moveSpeed = monsterOriginalData.MoveSpeed;
            gold = monsterOriginalData.Gold;
            animatorOverrideController = monsterOriginalData.animatorOverrideController;
        }

        /*public static MonsterData MakeTempMonster(string _name , float _hp, float _moveSpeed, float _gold)
        {
            MonsterData monsterData = new MonsterData();
        }*/

        /*public MonsterData(string _name, int _hp, float _moveSpeed, int _gold)
        {
            name = _name;
            hp = _hp;
            moveSpeed = _moveSpeed;
            gold = _gold;
        }*/

        /*public MonsterData Clone()
        {
            return new MonsterData(name, hp, moveSpeed,gold);
        }*/
    }
}
