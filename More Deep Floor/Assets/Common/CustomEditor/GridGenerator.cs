using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.CustomEditors
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject prefeb;
        [SerializeField] private Vector2Int size;
        [SerializeField] private Vector2 distance;
        [SerializeField] private Vector3 position;
        [SerializeField] private Transform parent;

        public void GenerateObjects()
        {
            Vector3 pos = position;
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var newObject = Instantiate(prefeb , parent);
                    newObject.transform.position = pos;
                    pos += new Vector3(distance.x , 0, 0); 
                }

                pos -= new Vector3(0, distance.y, 0);
                pos.x = position.x;
            }
        }
    }
}


