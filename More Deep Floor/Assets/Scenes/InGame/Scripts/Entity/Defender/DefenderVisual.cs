using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders
{
    public class Star
    {
        private GameObject parent;
        private List<SpriteRenderer> starSpriteRenderers;

        public Star(GameObject _parent)
        {
            starSpriteRenderers = new List<SpriteRenderer>();
            for (int i = 0; i < _parent.transform.childCount; i++)
            {
                starSpriteRenderers.Add(_parent.transform.GetChild(i).GetComponent<SpriteRenderer>());
            }
        }

        public void Set(Color color , int level)
        {
            
        }
    }
    
    public class DefenderVisual : MonoBehaviour
    {
        
    }
}

