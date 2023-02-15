using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    [System.Serializable]
    public class DefenderStatus
    {
        private TimerManager timerManager = null;
        
        public int level;
        public float attackSpeedTimer;
        public int maxMana;
        public int currentMaxMana;
        public int currentMana;

        public int isCanGainMana = 0;
        
        public DefenderData defenderData;
        public Dictionary<string, int> buffList;

        public delegate void OnManaChangedEventHandler(int maxMana, int currentMana);
        public OnManaChangedEventHandler OnManaChangedAction;

        public DefenderStatusValue damage;
        public DefenderStatusValue attackSpeed;

        public DefenderStatus(DefenderData _defenderData)
        {
            if(timerManager == null)
                timerManager = TimerManager.instance;

            defenderData = _defenderData;
            level = 1;

            damage = new DefenderStatusValue(_defenderData.damages[0]);

            attackSpeed = new DefenderStatusValue(_defenderData.attackSpeeds[0]);
            attackSpeedTimer = 1 / attackSpeed.currentValue;

            maxMana = defenderData.maxMana;
            currentMaxMana = defenderData.maxMana;
            currentMana = 0;

            buffList = new Dictionary<string, int>();
        }

        public void LevelUp()
        {
            level++;
            attackSpeed.SetOriginalValue(defenderData.attackSpeeds[level - 1]);
            damage.SetOriginalValue(defenderData.damages[level - 1]);
        }

        public bool ManaUp(int value)
        {
            if (isCanGainMana != 0)
            {
                return false;
            }
            
            currentMana += value;
            if (currentMana >= currentMaxMana)
            {
                currentMana = currentMaxMana - currentMana;
                OnManaChangedAction?.Invoke(currentMaxMana , currentMana);

                return true;
            }
            else
            {
                OnManaChangedAction?.Invoke(currentMaxMana , currentMana);
                return false;
            }
        }

        public StatusBuff AddAttackSpeedBuff(float value, string name)
        {
            StatusBuff buff = attackSpeed.AddBuff(value , name);
            attackSpeedTimer = 1 / attackSpeed.currentValue;
            return buff;
        }

        public void RemoveAttackSpeedBuff(StatusBuff buff)
        {
            attackSpeed.RemoveBuff(buff);
            attackSpeedTimer = 1 / attackSpeed.currentValue;
        }
        
        public StatusBuff ModifyAttackSpeedBuff(float value, string name)
        {
            StatusBuff buff = attackSpeed.AddBuffWithModify(value , name);
            attackSpeedTimer = 1 / attackSpeed.currentValue;
            return buff;
        }

        public void SetManaGain(bool isGain)
        {
            if (isGain)
            {
                isCanGainMana++;
                if (isCanGainMana > 0) isCanGainMana = 0;
            }
            else
            {
                isCanGainMana--;
            }
               
        }
        
    }
}


