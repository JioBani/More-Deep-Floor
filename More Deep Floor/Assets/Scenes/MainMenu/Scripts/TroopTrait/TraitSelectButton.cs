/*
using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;
using UnityEngine.UI ;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class TraitSelectButton : MonoBehaviour
    {
        [SerializeField] private TroopTraitUiManager uiManager;
        [SerializeField] private Image iconImage;
    
        private TroopTrait trait;
        public TroopTraitData traitData;
        public Button button;
        public bool isClicked = false;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        public void SetData(TroopTrait _trait)
        {
            trait = _trait;
            iconImage.sprite = trait.traitData.Sprite;
        }

        public void OnClick()
        {
            uiManager.OnClickTrait(trait);
            isClicked = true;
            button.interactable = false;
            iconImage.color = GetColor(false);
        }

        public void ResetClicked()
        {
            isClicked = false;
            button.interactable = true;
            iconImage.color = GetColor(true);
        }

        Color GetColor(bool isEnable)
        {
            if (isEnable)
            {
                ColorUtility.TryParseHtmlString("#FFFFFF", out Color color);
                return color;
            }
            else
            {
                ColorUtility.TryParseHtmlString("#C8C8C8", out Color color);
                return color;
            }
        }
    }

}
*/

