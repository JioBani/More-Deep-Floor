using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys.States;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    //# 클래스를 사용하면 GC가 생기는지 확인
    class ShieldState
    {
        public string id;
        public float amount;
        public float leftAmount;
        public float maintainTime;
        //public DefenderState state;

        public ShieldState(string id , float amount, float maintainTime, EntityState state)
        {
            this.id = id;
            this.amount = amount;
            this.leftAmount = amount;
            this.maintainTime = maintainTime;
            //this.state = state as DefenderState;
        }
    }
    
    public class ShieldController
    {
        private Defender defender;
        public float amount { get; private set; }
        private Dictionary<string, ShieldState> shieldDic;
        private List<ShieldState> shieldList;

        public delegate void OnShieldChangeEventHandler(float amount);

        private OnShieldChangeEventHandler OnShieldChangeAction;

        public void Init()
        {
            amount = 0;
            shieldDic = new Dictionary<string, ShieldState>();
            shieldList = new List<ShieldState>();
            OnShieldChangeAction = null;
        }

        public void AddShield(
            string id, 
            float maxAmount, 
            float lateTime,
            EntityState state,
            bool isHasTimeoutAction = false
            )
        {
            ShieldState shieldState = new ShieldState(id , maxAmount , lateTime , state);

            shieldList.Add(shieldState);
            shieldDic.Add(shieldState.id , shieldState);
            
            if (lateTime > 0)
            {
                TimerManager.instance.LateAction(lateTime , () =>
                {
                    var shield = FindShieldState(id);
                    if (shield != null)
                    {
                        RemoveShield(shield);
                        if (!isHasTimeoutAction)
                        {
                            //TODO state.OnShieldTimeOutAction(maxAmount , shield.leftAmount);
                        }
                    }
                });
            }
            
            Refresh();
        }

        ShieldState FindShieldState(string id)
        {
            if (shieldDic.TryGetValue(id, out var result))
            {
                return result;
            }
            return null;
        }
        
        void RemoveShield(ShieldState shieldState)
        {
            shieldState.leftAmount = 0;
            Refresh();
        }

        void RemoveShield(string id)
        {
            try
            {
                RemoveShield(shieldDic[id]);
            }
            catch (Exception e)
            {
                Common.Loggers.CustomLogger.LogException(e);
                throw;
            }
        }

        public float SetDamage(float damage)
        {
            
            if (amount == 0) return damage;
            
            foreach (var shieldState in shieldList)
            {

                if (shieldState.leftAmount > damage)
                {
                    shieldState.leftAmount -= damage;
                    Refresh();
                    return 0;
                }
                else
                {
                    float temp = shieldState.leftAmount;
                    shieldState.leftAmount = 0;
                    damage -= temp;
                }
            }
            
            Refresh();
            return damage;
        }

        void Refresh()
        {
            amount = 0;
            
            for (var i = shieldList.Count - 1; i >= 0; i--)
            {
                amount += shieldList[i].leftAmount;
                
                
                if (shieldList[i].leftAmount == 0)
                {
                    var shield = shieldList[i];
                    shieldDic.Remove(shieldList[i].id);
                    shieldList.RemoveAt(i);
                    //TODO shield.state.OnShieldBreakAction(shield.amount);
                }
            }
            
            OnShieldChangeAction?.Invoke(amount);
        }

        public void AddListener(OnShieldChangeEventHandler action)
        {
            OnShieldChangeAction += action;
        }
    }
}

