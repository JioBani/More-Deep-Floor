using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.Entitys.Monsters
{
    public class DefenderSearcher : MonoBehaviour
    {
        public delegate void OnTargetSearchEventHandler();

        public delegate void OnTargetLostEventHandler();

        public OnTargetSearchEventHandler OnTargetSearchEvent;
        public OnTargetLostEventHandler OnTargetLostEvent;
        
        public Defender target { get; private set; }
        public bool isTargetExist { get; private set; } = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (ReferenceEquals(target, null))
            {
                target = other.transform.parent.GetComponent<Defender>();
                isTargetExist = true;
                OnTargetSearchEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            
            if (ReferenceEquals(target.gameObject, other.transform.parent.gameObject))
            {
                target = null;
                isTargetExist = false;
                OnTargetLostEvent?.Invoke();
            }
        }
    }
}

