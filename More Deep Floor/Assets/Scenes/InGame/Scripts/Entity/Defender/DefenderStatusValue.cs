using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders
{

    [Serializable]
    public class DefenderStatusValue
    {
        public float originalValue { get; private set; }
        public float currentValue { get; private set; }
        private List<StatusBuff> buffList = new List<StatusBuff>();

        public DefenderStatusValue(float value)
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
}


