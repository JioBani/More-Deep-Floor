using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;



namespace LNK.MoreDeepFloor.InGame.Entity
{
    [System.Serializable]
    public class DefenderStatus
    {
        private TimerManager timerManager = null;
        
        public int level;
        public float attackSpeedTimer;
        //public float maxMana;
        public int currentMaxMana;
        public int currentMana;

        public int isCanGainMana = 0;
        
        public DefenderData defenderData;
        public Dictionary<string, int> buffList;

        public delegate void OnManaChangedEventHandler(int maxMana, int currentMana);
        public delegate void OnHpChangedEventHandler(float maxHp, float currentHp);

        public OnManaChangedEventHandler OnManaChangedAction;
        public OnHpChangedEventHandler OnHpChangedAction;

        public DefenderStatusValue damage;
        public DefenderStatusValue attackSpeed;
        public DefenderStatusValue maxHp;

        public float currentHp
        {
            get;
            private set;
        }

        public DefenderStatus(DefenderData _defenderData)
        {
            if(timerManager == null)
                timerManager = TimerManager.instance;

            defenderData = _defenderData;
            level = 1;

            damage = new DefenderStatusValue(_defenderData.currentDamages[level]);

            attackSpeed = new DefenderStatusValue(_defenderData.currentAttackSpeeds[level]);
            attackSpeedTimer = 1 / attackSpeed.currentValue;

            maxHp = new DefenderStatusValue(_defenderData.heathPoints[level]);
            currentHp = maxHp.currentValue;
            
            
            currentMaxMana = defenderData.currentMaxManas.SaveGet(0, 100);
            currentMana = defenderData.currentMaxManas.SaveGet(0, 100);
            
            currentMana = 0;

            buffList = new Dictionary<string, int>();
        }

        public void LevelUp()
        {
            level++;
            RefreshStatus();
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

        public void ChangeHp(float value)
        {
            currentHp += value;
            if (currentHp > maxHp.currentValue) currentHp = maxHp.currentValue;
            else if (currentHp < 0) currentHp = 0;
        }

        public void RefreshStatus()
        {
            damage.SetOriginalValue(defenderData.currentDamages[level]);
            attackSpeed.SetOriginalValue(defenderData.currentAttackSpeeds[level]);
            attackSpeedTimer = 1 / attackSpeed.currentValue;
        }
    }
}


