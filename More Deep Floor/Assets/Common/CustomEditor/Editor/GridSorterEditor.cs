using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LNK.MoreDeepFloor.Common.CustomEditors
{
    [CustomEditor(typeof(GridSorter))]
    public class GridSorterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GridSorter sorter = (GridSorter)target;
            if (GUILayout.Button("Sort Objects"))
            {
                sorter.SortObjects();
            }
        }
    }

}

