using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private TextMeshPro hpText;
        [SerializeField] private SpriteRenderer hpBarSprite;
        [SerializeField] private SpriteRenderer hpBarBackGround;
        [SerializeField] private SpriteRenderer shieldBar;

        private Vector2 maxSize;
        
        private void Awake()
        {
            maxSize = hpBarBackGround.size;
        }

        public void RefreshBar(float maxHp , float currentHp , float shield)
        {
            if (currentHp + shield <= maxHp)
            {
                hpBarSprite.size = new Vector2(maxSize.x * (currentHp / maxHp), maxSize.y);
                shieldBar.size = new Vector2(maxSize.x * (shield / maxHp), maxSize.y);
                shieldBar.transform.position = hpBarSprite.transform.position + new Vector3(hpBarSprite.size.x + shieldBar.size.x / 2,0);
            }
            else
            {
                hpBarSprite.size = new Vector2(maxSize.x * (currentHp / (currentHp + shield)), maxSize.y);
                shieldBar.size = new Vector2(maxSize.x * (shield / (currentHp + shield)), maxSize.y);
                shieldBar.transform.position = hpBarSprite.transform.position + new Vector3(hpBarSprite.size.x + shieldBar.size.x / 2,0);
            }
            hpText.text = ((int)(currentHp)).ToString();
        }
    }
}


