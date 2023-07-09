using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public abstract class EntitySearcher : MonoBehaviour
    {
        public Entity target { get; private set; }
        public EntityTarget entityTarget; 
        public bool isTargetExist { get; private set; }

        public delegate void OnEntitySearchEventHandler(Entity entity);

        public delegate void OnEntityLostEventHandler(Entity entity);

        OnEntitySearchEventHandler OnEntitySearchAction;
        OnEntityLostEventHandler OnEntityLostAction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (ReferenceEquals(target, null))
            {
                entityTarget = other.GetComponent<EntityTarget>();
                target = entityTarget.entity;
                isTargetExist = true;
                OnEntitySearchAction?.Invoke(target);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (ReferenceEquals(entityTarget.gameObject, other.gameObject))
            {
                Entity temp = target;
                target = null;
                isTargetExist = false;
                OnEntityLostAction?.Invoke(temp);
            }
        }

        public void Init()
        {
            OnEntitySearchAction = null;
            OnEntityLostAction = null;
        }

        public void AddSearchListener(OnEntitySearchEventHandler action)
        {
            OnEntitySearchAction += action;
        }

        public void AddLostListener(OnEntityLostEventHandler action)
        {
            OnEntityLostAction += action;
        }
    }
}

 
