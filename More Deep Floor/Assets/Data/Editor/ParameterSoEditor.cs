using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Editors
{
    public class ParameterSoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // built-in 인스펙터 뷰를 그린다. 각 프로퍼티의 (커스텀 포함) PropertyDrawer.OnGUI()호출.
            DrawDefaultInspector();
 
            // Preview 버튼 추가.
            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Set Parameters"))
            {
                ((ParameterSo) target).SetParameters();
            }
            
            if (GUILayout.Button("Set Description"))
            {
                //((ParameterSo) target).SetDescriptionShowing();
            }
            EditorGUI.EndDisabledGroup();
        }
    }

}
