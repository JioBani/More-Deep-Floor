using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    
    [CustomEditor(typeof(CorpsListView))]
    public class CorpsListViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh Sell Size"))
            {
                CorpsListView listView = target as CorpsListView;
                listView.SetCellSize();
            }
        }
    }
}


