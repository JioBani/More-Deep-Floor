using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class EntityAnimator : MonoBehaviour
    {
        private Entity entity;
        private Animator animator;
        // Start is called before the first frame update

        private void Awake()
        {
            entity = GetComponent<Entity>();
            
        }

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
