using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEditor;
using UnityEngine;
using LNK.MoreDeepFloor.Data.DefenderTraits;

namespace LNK.MoreDeepFloor.Data.Editors
{
    [CustomEditor(typeof(TraitGenerator), true)]
    public class TraitGeneratorEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Generate"))
            {
                ((TraitGenerator) target).CreateMyAsset();
            }
      
            EditorGUI.EndDisabledGroup();
        }
    }
}


