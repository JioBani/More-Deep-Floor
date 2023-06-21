using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public class EntityCollide : MonoBehaviour
    {
        public delegate void OnCollideActionHandler(Collider2D col);

        public OnCollideActionHandler OnTriggerEnter2DAciton;
        public OnCollideActionHandler OnTriggerExit2DAciton;

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTriggerEnter2DAciton?.Invoke(col);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            OnTriggerExit2DAciton?.Invoke(col);
        }
    }
}

