using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using TMPro;
using UnityEngine;


namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders.States
{
    public class StateInfoView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TextMeshPro text;
        private EntityState state;

        public void Set(EntityState _state)
        {
            state = _state;
            spriteRenderer.sprite = _state.entityStateData.Icon;
        }

        public void ResetInfo()
        {
            state = null;
            spriteRenderer.sprite = null;
            text.text = "";
        }

        public void SetImage(Sprite image)
        {
            spriteRenderer.sprite = image;
        }

        public void UpdateStack(int stack)
        {
            if(stack != 0)
                text.text = stack.ToString();
        }

        
    }
}

