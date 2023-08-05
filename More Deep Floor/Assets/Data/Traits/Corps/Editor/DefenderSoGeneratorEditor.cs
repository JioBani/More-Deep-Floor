using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Traits.Corps
{
    [CustomEditor(typeof(DefenderSoGenerator))]
    public class DefenderSoGeneratorEditor : Editor
    {
        private DefenderSoGenerator generator;
        
        private void OnEnable()
        {
            generator = target as DefenderSoGenerator;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Set Dic"))
            {
                generator.GenerateDefenders();
            }
        }
    }
}

