using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using TMPro;
using UnityEngine;


namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders.States
{
    public class StateInfoView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TextMeshPro text;

        public void SetImage(Sprite image)
        {
            spriteRenderer.sprite = image;
        }

        public void UpdateStack(int stack)
        {
            if(stack != 0)
                text.text = stack.ToString();
        }

        public void ResetInfo()
        {
            spriteRenderer.sprite = null;
            text.text = "";
        }
    }
}

