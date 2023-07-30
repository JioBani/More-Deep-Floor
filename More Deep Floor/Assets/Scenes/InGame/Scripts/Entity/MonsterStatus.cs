using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public class MonsterStatusBuff
    {
        public float value;
        public string name;
        public float stack;

        public MonsterStatusBuff(float _value , string _name)
        {
            value = _value;
            stack = 1;
            name = _name;
        }

        public void AddStack()
        {
            stack++;
        }

        /// <summary>
        /// 버프 스택 감소
        /// </summary>
        /// <returns>스택이 0 이하인 경우 false</returns>
        public bool RemoveStack()
        {
            stack--;
            if (stack <= 0)
            {
                return false;
            }

            return true;
        }
    }
    
    public class MonsterStatusValue
    {
        public float originalValue { get; private set; }
        public float currentValue { get; private set; }
        private List<MonsterStatusBuff> buffList = new List<MonsterStatusBuff>();
        
        public MonsterStatusValue(float value)
        {
            originalValue = value;
            currentValue = value;
        }
        
        public void Refresh()
        {
            currentValue = originalValue;
            string str = "";

            foreach (var statusBuff in buffList)
            {
                currentValue += statusBuff.value;
                str += $"{statusBuff.name} : {statusBuff.value} ,";
            }
        }

        public MonsterStatusBuff AddBuff(float value, string name)
        {
            
            MonsterStatusBuff buff = buffList.Find(buff => buff.name == name);
            if (buff == null)
            {
                MonsterStatusBuff newBuff = new MonsterStatusBuff(value,name);
                buffList.Add(newBuff);
                Refresh();
                return newBuff;
            }
            else
            {
                buff.AddStack();
                Refresh();
                return buff;
            }
        }

        public MonsterStatusBuff AddBuffWithModify(float value, string name)
        {
            MonsterStatusBuff buff = buffList.Find(buff => buff.name == name);
            if (buff == null)
            {
                MonsterStatusBuff newBuff = new MonsterStatusBuff(value,name);
                buffList.Add(newBuff);
                Refresh();
                return newBuff;
            }
            else
            {
                buff.value = value;
                buff.AddStack();
                Refresh();
                return buff;
            }
        }

        public void RemoveBuff(MonsterStatusBuff buff)
        {
            if (!buff.RemoveStack())
            {
                buffList.Remove(buff);
            }
            
            Refresh();
        }

        public void SetOriginalValue(float value)
        {
            originalValue = value;
            Refresh();
        }

        public void ModifyBuff(MonsterStatusBuff buff, float value)
        {
            buff.value = value;
            Refresh();
        }
    }
    
    public class MonsterStatus : EntityStatus
    {
        public delegate void OnSpeedChangeEventHandler(float speed);

        public OnSpeedChangeEventHandler OnSpeedChangeAction;
        
        public int gold;
        public int currentGold;
        
        public MonsterData monsterData;

        public override void SetStatus(EntityData data, int _level)
        {
            base.SetStatus(data, _level);
            monsterData = data as MonsterData;
            currentGold = monsterData.gold; 
        }

        /*public MonsterStatus(float maxHp, float speed, int gold)
        {
            this.maxHp = maxHp;
            this.currentHp = maxHp;

            this.speed = new MonsterStatusValue(speed);
            
            //this.speed = speed;
            //this.currentSpeed = speed;
            
            this.gold = gold;
            this.currentGold = gold;
            
            speedBuffs = new Dictionary<int, float>();
        }*/

        /*public MonsterStatus(MonsterData monsterData) : this(
            maxHp: monsterData.hp,
            speed: monsterData.moveSpeed,
            gold: monsterData.gold
        ){ }*/

        /*public int AddSpeedBuff(float value)
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
        }*/
    }
}