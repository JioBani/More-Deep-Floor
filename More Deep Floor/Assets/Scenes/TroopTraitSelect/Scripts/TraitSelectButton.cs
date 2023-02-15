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
    
        public TroopTraitData traitData;
        public TroopTrait trait;
        public Button button;
        public bool isClicked = false;

        private void Awake()
        {
            trait = new TroopTrait(traitData);
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            iconImage.sprite = traitData.Sprite;
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

