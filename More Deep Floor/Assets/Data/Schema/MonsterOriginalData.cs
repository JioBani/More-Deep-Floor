using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Entity;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]

    public class MonsterOriginalData : EntityOriginalData
    {
        /*[SerializeField]
        private string name;
        public string Name { get { return name; } }*/
        
        /*[SerializeField]
        private int hp;
        public int Hp { get { return hp; } }*/
        
        /*[SerializeField]
        private float moveSpeed;
        public float MoveSpeed { get { return moveSpeed; } }*/
        
        
        [SerializeField] private int gold;
        public int Gold => gold;

        public AnimatorOverrideController animatorOverrideController;
    }
}


