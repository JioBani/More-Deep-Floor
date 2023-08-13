using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    [Serializable]
    public class StatusValue
    {
        public float originalValue { get; private set; }
        public float currentValue { get; private set; }

        private List<StatusBuff> buffList = new List<StatusBuff>();

        public StatusValue Reset(float value)
        {
            originalValue = value;
            currentValue = value;
            buffList = new List<StatusBuff>();
            Refresh();
            return this;
        }
        
        public virtual void Refresh()
        {
            currentValue = originalValue;
            string str = "";

            foreach (var statusBuff in buffList)
            {
                currentValue += statusBuff.value;
                str += $"{statusBuff.name} : {statusBuff.value} ,";
            }
        }
        
        public StatusBuff AddBuff(float value, string name)
        {
            
            StatusBuff buff = buffList.Find(buff => buff.name == name);
            if (buff == null)
            {
                StatusBuff newBuff = new StatusBuff(value,name);
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

        public StatusBuff AddBuffWithModify(float value, string name)
        {
            StatusBuff buff = buffList.Find(buff => buff.name == name);
            if (buff == null)
            {
                StatusBuff newBuff = new StatusBuff(value,name);
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

        public void RemoveBuff(StatusBuff buff)
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

        public void ModifyBuff(StatusBuff buff, float value)
        {
            buff.value = value;
            Refresh();
        }

    }
    
    
    [Serializable]
    public class AttackSpeedValue : StatusValue
    {
        public float timerPerAttack { private set; get; }

        public override void Refresh()
        {
            base.Refresh();
            if (currentValue > 0)
            {
                timerPerAttack = 1 / currentValue;
            }
        }
    }
    
    [Serializable]
    public class RangeValue : StatusValue
    {
        public float square { private set; get; }

        public override void Refresh()
        {
            base.Refresh();
            square = currentValue * currentValue;
        }
    }

}

