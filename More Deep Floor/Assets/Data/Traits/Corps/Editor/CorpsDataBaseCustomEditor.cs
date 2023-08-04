using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Traits.Corps
{
    [CustomEditor(typeof(CorpsDataBase))]
    public class CorpsDataBaseCustomEditor : Editor
    {
        private CorpsDataBase corpsDataBase;
        
        private void OnEnable()
        {
            corpsDataBase = target as CorpsDataBase;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Set Dic"))
            {
                corpsDataBase.SetDic();
            }
        }
    }
}


