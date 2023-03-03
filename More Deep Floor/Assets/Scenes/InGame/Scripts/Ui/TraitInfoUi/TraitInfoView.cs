using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi
{
    public class TraitInfoView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private Text[] statusTexts;

        [SerializeField] private Color[] colors;
        [SerializeField] private Color noneColor;

        private TraitManager traitManager;
        private UiManager uiManager;
        private BattleFieldTraitInfo battleFieldTraitInfo;

        public void Awake()
        {
            traitManager = ReferenceManager.instance.traitManager;
            uiManager = ReferenceManager.instance.uiManager;
        }

        public void RefreshTrait(BattleFieldTraitInfo traitInfo)
        {
            battleFieldTraitInfo = traitInfo;
            
            image.sprite = traitInfo.traitData.Image;
            nameText.text = traitInfo.traitData.TraitName;


            for (int i = 0; i < statusTexts.Length; i++)
            {
                if (i < traitInfo.traitData.SynergyTrigger.Length)
                {
                    statusTexts[i].gameObject.SetActive(true);
                    if (i == traitInfo.synergyLevel)
                    {
                        statusTexts[i].text = $"{traitInfo.nums}/{traitInfo.traitData.SynergyTrigger[i]}";
                        statusTexts[i].color = colors[traitInfo.synergyLevel];
                    }
                    else
                    {
                        statusTexts[i].text = traitInfo.traitData.SynergyTrigger[i].ToString();
                        statusTexts[i].color = Color.white;
                    }
                }
                else
                {
                    statusTexts[i].gameObject.SetActive(false);
                }
            }

            if (traitInfo.synergyLevel == -1)
            {
                image.color = noneColor;
            }
            else
            {
                image.color = colors[traitInfo.synergyLevel];
            }
        }

        public void OnClick()
        {
            uiManager.OnClickTrait(transform.position , battleFieldTraitInfo);
        }
    }
}

