using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.Entitys
{

    public class EntityStatus
    {
        private TimerManager timerManager = null;
        public EntityData data { private set; get; }

        public float attackSpeedTimer;
        public int level { get; private set; }

        //#. 공격력, 마법력, 공격속도, 사거리, 체력, 물리방어력, 마법방어력, 치명타율, 이동속도, 마나

        public StatusValue damage;
        public StatusValue magicalPower;
        public StatusValue attackSpeed;
        public StatusValue range;
        public StatusValue criticalRate;
        
        public StatusValue maxHp;
        public StatusValue physicalDefense;
        public StatusValue magicalDefense;
        public StatusValue moveSpeed;
        public StatusValue maxMana;

        public float currentMana;
        public float currentHp;

        public ShieldController shieldController;

        private Dictionary<EntityStatusArray, StatusValue> statusDic;
        
        public delegate void OnManaChangedEventHandler(float maxMana, float currentMana);
        public delegate void OnHpChangedEventHandler(float maxHp, float currentHp , Entity caster);

        public OnManaChangedEventHandler OnManaChangedAction;
        public OnHpChangedEventHandler OnHpChangedAction;

        private int isCanGainMana = 0;

        public EntityStatus()
        {
            timerManager = TimerManager.instance;

            level = 0;
            damage = new StatusValue();
            magicalPower = new StatusValue();
            attackSpeed = new StatusValue();
            range = new StatusValue();
            criticalRate = new StatusValue();
            maxHp = new StatusValue();
            physicalDefense = new StatusValue();
            magicalDefense = new StatusValue();
            moveSpeed = new StatusValue();
            maxMana = new StatusValue();

            shieldController = new ShieldController();
        }

        public virtual void SetStatus(EntityData data, int _level)
        {
            this.data = data;
            
            level = _level;

            statusDic = new Dictionary<EntityStatusArray, StatusValue>();

            statusDic.Add(data.damages , damage.Reset(data.damages.currentValues.SaveGet(level,10)));
            statusDic.Add(data.magicalPowers , magicalPower.Reset(data.magicalPowers.currentValues.SaveGet(level,10)));
            statusDic.Add(data.attackSpeeds , attackSpeed.Reset(data.attackSpeeds.currentValues.SaveGet(level,1)));
            statusDic.Add(data.ranges , range.Reset(data.ranges.currentValues.SaveGet(level,10)));
            statusDic.Add(data.criticalRates , criticalRate.Reset(data.criticalRates.currentValues.SaveGet(level,10)));
            
            statusDic.Add(data.heathPoints , maxHp.Reset(data.heathPoints.currentValues.SaveGet(level,100)));
            statusDic.Add(data.physicalDefenses , physicalDefense.Reset(data.physicalDefenses.currentValues.SaveGet(level,0)));
            statusDic.Add(data.magicalDefenses , magicalDefense.Reset(data.magicalDefenses.currentValues.SaveGet(level,0)));
            
            statusDic.Add(data.moveSpeeds , moveSpeed.Reset(data.moveSpeeds.currentValues.SaveGet(level,1)));
            statusDic.Add(data.maxManas , maxMana.Reset(data.maxManas.currentValues.SaveGet(level,100)));

            currentMana = 0;
            currentHp = maxHp.currentValue;

            attackSpeedTimer = 1 / attackSpeed.currentValue;
            isCanGainMana = 0;
            
            shieldController.Init();
        }
        
        public void RefreshStatus()
        {
            /*foreach (var keyValuePair in statusDic)
            {
                
            }

            attackSpeedTimer = 1 / attackSpeed.currentValue;*/
        }
        
        public void LevelUp()
        {
            level++;
            
            foreach (var pair in statusDic)
            {
                pair.Value.SetOriginalValue(pair.Key.currentValues[level]);
            }
            
            attackSpeedTimer = 1 / attackSpeed.currentValue;
            
            
            RefreshStatus();
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
        
        public void ChangeHp(float value , Entity caster)
        {
            currentHp += value;
            if (currentHp > maxHp.currentValue) currentHp = maxHp.currentValue;
            else if (currentHp < 0) currentHp = 0;
            OnHpChangedAction?.Invoke(maxHp.currentValue ,currentHp , caster);
        }
        
        public bool ManaUp(int value)
        {
            if (isCanGainMana != 0)
            {
                return false;
            }
            
            currentMana += value;
            if (currentMana >= maxMana.currentValue)
            {
                currentMana = maxMana.currentValue - currentMana;
                OnManaChangedAction?.Invoke(maxMana.currentValue , currentMana);

                return true;
            }
            else
            {
                OnManaChangedAction?.Invoke(maxMana.currentValue , currentMana);
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
    }
}


