using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders

{
    public class StatusBuff
    {
        public float value;
        public string name;
        public float stack;

        public StatusBuff(float _value , string _name)
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
}



