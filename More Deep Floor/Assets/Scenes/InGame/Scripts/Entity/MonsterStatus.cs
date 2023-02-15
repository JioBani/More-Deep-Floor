using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class MonsterStatus
    {
        public delegate void OnSpeedChangeEventHandler(float speed);

        public OnSpeedChangeEventHandler OnSpeedChangeAction;
        
        public float maxHp;
        public float currentHp;

        public float speed;
        public float currentSpeed;
        private Dictionary<int, float> speedBuffs;
        private int speedBuffId;

        public int gold;
        public int currentGold;
        
        public MonsterData monsterData;

        public MonsterStatus(float maxHp, float speed, int gold)
        {
            this.maxHp = maxHp;
            this.currentHp = maxHp;
            
            this.speed = speed;
            this.currentSpeed = speed;
            
            this.gold = gold;
            this.currentGold = gold;

            speedBuffId = 0;
            
            speedBuffs = new Dictionary<int, float>();
        }

        public MonsterStatus(MonsterData monsterData) : this(
            maxHp: monsterData.hp,
            speed: monsterData.moveSpeed,
            gold: monsterData.gold
        ){ }

        public int AddSpeedBuff(float value)
        {
            speedBuffId++;
            currentSpeed += value;
            speedBuffs[speedBuffId] = value;
            OnSpeedBuffChanged();
            return speedBuffId;
        }

        public void RemoveSpeedBuff(int id)
        {
            speedBuffs.Remove(id);
            OnSpeedBuffChanged();
        }

        public void OnSpeedBuffChanged()
        {
            currentSpeed = speed;
            foreach(KeyValuePair<int, float> items in speedBuffs)
            {
                currentSpeed += items.Value;
            }
            
            if(currentSpeed < 0) currentSpeed = 0;
            OnSpeedChangeAction?.Invoke(currentSpeed);
        }
    }
}