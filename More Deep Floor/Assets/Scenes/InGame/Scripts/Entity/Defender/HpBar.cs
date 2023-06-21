using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private TextMeshPro hpText;
        [SerializeField] private SpriteRenderer hpBarSprite;
        [SerializeField] private SpriteRenderer hpBarBackGround;

        private Vector2 maxSize;
        
        private void Awake()
        {
            maxSize = hpBarBackGround.size;
        }

        public void RefreshBar(int maxHp , int currentHp)
        {
            hpBarSprite.size = new Vector2(maxSize.x * (currentHp / (float)maxHp), maxSize.y);
            hpText.text = currentHp.ToString();
        }
    }
}


