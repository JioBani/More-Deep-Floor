using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common
{
    public class PoolingText : MonoBehaviour
    {
        private TextMeshPro textMeshPro;
        private Poolable poolable;

        private void Awake()
        {
            textMeshPro = GetComponent<TextMeshPro>();
            poolable = GetComponent<Poolable>();
        }

        public void SetText(string str)
        {
            textMeshPro.text = str;
        }

        public void SetTimeOut(float time)
        {
            StartCoroutine(TimeOutRoutine(time));
        }

        IEnumerator TimeOutRoutine(float time)
        {
            yield return new WaitForSeconds(time);
            poolable.SetOff();
        }

        public void SetOff()
        {
            poolable.SetOff();
        }
    }
}


