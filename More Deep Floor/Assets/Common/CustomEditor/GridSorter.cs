using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.CustomEditors
{
    public class GridSorter : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private Vector2 distance;
        [SerializeField] private Vector3 position;
        [SerializeField] private Transform parent;

        public void SortObjects()
        {
            Vector3 pos = position;
            
            for (int i = 0; i < parent.childCount; i++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int x = 0; x < size.x; x++)
                    {
                        parent.GetChild(i).transform.localPosition = position + new Vector3(x * distance.x , -y * distance.y,0);
                        i++;
                        Debug.Log("정렬");
                    }
                }
            }
        }
    }
}


