using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public class MonsterSearcher : MonoBehaviour
    {
        private Defender defender;
        
        private void Awake()
        {
            defender = transform.parent.GetComponent<Defender>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            defender.TrySetTarget(other.transform.parent.GetComponent<Monster>());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            defender.TrySetUnTarget(other.transform.parent.GetComponent<Monster>());
        }
    }

}
