using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Bullets
{
    public class RangeSkillBullet : MonoBehaviour
    {
        [SerializeField] private Poolable poolable;
        private AttackInfo attackInfo;

        public void Show(Vector2 pos)
        {
            transform.position = pos;
            Invoke(nameof(Off) , 0.5f);
        }
        
        void Off()
        {
            poolable.SetOff();
        }
    }
}


