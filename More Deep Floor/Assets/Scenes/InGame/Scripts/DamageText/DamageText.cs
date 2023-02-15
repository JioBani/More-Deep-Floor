using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.Common.TimerSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LNK.MoreDeepFloor.InGame.DamageTexts
{
    public class DamageText : MonoBehaviour
    {
        private PoolingText poolingText;
        private bool isOn;
        public float speed;
        public float timeOut;

        private void Awake()
        {
            poolingText = GetComponent<PoolingText>();
        }

        public void SetOn(float damage, Vector2 pos)
        {
            transform.position = pos + new Vector2(Random.Range(-1,1) * 0.3f , Random.Range(-1,1) * 0.15f);
            poolingText.SetText(damage.ToString());
            isOn = true;
            TimerManager.instance.LateAction(timeOut , SetOff);
        }

        void SetOff()
        {
            isOn = false;
            poolingText.SetOff();
        }

        private void FixedUpdate()
        {
            if (isOn)
            {
                transform.position += new Vector3(0,speed);
            }
        }
    }
}

