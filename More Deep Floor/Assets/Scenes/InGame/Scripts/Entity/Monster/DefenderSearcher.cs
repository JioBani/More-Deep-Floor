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
            Logger.Log($"[DefenderSearcher.OnTriggerEnter()] {other.gameObject.name}");
            if (ReferenceEquals(target, null))
            {
                target = other.transform.parent.GetComponent<Defender>();
                Logger.Log($"[DefenderSearcher.OnTriggerEnter()] {other.transform.parent}");
                Logger.Log($"[DefenderSearcher.OnTriggerEnter()] 타겟설정 : {target.name}");
                isTargetExist = true;
                OnTargetSearchEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Logger.Log($"[DefenderSearcher.OnTriggerExit2D()] {other.gameObject.name}");
            
            if (ReferenceEquals(target.gameObject, other.transform.parent.gameObject))
            {
                target = null;
                Logger.Log($"[DefenderSearcher.OnTriggerEnter()] 타겟 로스트");
                isTargetExist = false;
                OnTargetLostEvent?.Invoke();
            }
        }
    }
}

