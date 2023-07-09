using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public class EntityTarget : MonoBehaviour
    {
        public Entity entity;
        public delegate void OnHitEventHandler(Entity firer,float damage);

        OnHitEventHandler OnHitAction;

        public void OnHit(Entity firer,float damage)
        {
            OnHitAction?.Invoke(firer,damage);
        }

        public void AddOnHitListener(OnHitEventHandler action)
        {
            OnHitAction += action;
        }
    }
}


