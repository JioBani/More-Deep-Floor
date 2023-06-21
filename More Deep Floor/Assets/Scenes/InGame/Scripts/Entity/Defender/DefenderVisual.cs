using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Style;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders
{
    public class Star
    {
        private GameObject parent;
        private List<SpriteRenderer> starSpriteRenderers;
        
        public Star(GameObject _parent)
        {
            parent = _parent;
            starSpriteRenderers = new List<SpriteRenderer>();
            for (int i = 0; i < _parent.transform.childCount; i++)
            {
                starSpriteRenderers.Add(_parent.transform.GetChild(i).GetComponent<SpriteRenderer>());
            }
        }

        public void Set(Color color)
        {
            for (var i = 0; i < starSpriteRenderers.Count; i++)
            {
                starSpriteRenderers[i].color = color;
            }
            parent.SetActive(true);
        }

        public void Off()
        {
            parent.SetActive(false);
        }
    }
    
    public class DefenderVisual : MonoBehaviour
    {
        [SerializeField] private CommonPalette palette;
        [SerializeField] private GameObject[] starGameObjects;
        private Star[] stars;

        private void Awake()
        {
            stars = new Star[3];
            
            for (var i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(starGameObjects[i]);
            }
        }

        public void SetStar(int cost , int level)
        {
            for (var i = 0; i < stars.Length; i++)
            {
                stars[i].Off();
            }

            stars[level - 1].Set(palette.CostColors[cost]);
        }
    }
}

