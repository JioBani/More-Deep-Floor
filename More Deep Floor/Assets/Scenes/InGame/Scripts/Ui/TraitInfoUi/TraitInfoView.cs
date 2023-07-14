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

        [SerializeField] private TextMeshProUGUI activeMemberNumsText;
        [SerializeField] private Text[] statusTexts;

        [SerializeField] private Color[] colors;
        [SerializeField] private Color noneColor;
        //[SerializeField] private Color highlightColor;

        private TraitManager traitManager;
        private UiManager uiManager;
        private ActiveTraitInfo activeTraitInfo;

        public void Awake()
        {
            traitManager = ReferenceManager.instance.traitManager;
            uiManager = ReferenceManager.instance.uiManager;
        }

        public void RefreshTrait(ActiveTraitInfo traitInfoInfo)
        {
            activeTraitInfo = traitInfoInfo;
            image.sprite = traitInfoInfo.traitData.Image;
            nameText.text = traitInfoInfo.traitData.name;
            activeMemberNumsText.text = traitInfoInfo.activeNumber.ToString();


            for (int i = 0; i < statusTexts.Length; i++)
            {
                if (i < traitInfoInfo.traitData.SynergyTrigger.Length - 1)
                {
                    statusTexts[i].gameObject.SetActive(true);
                    statusTexts[i].text = traitInfoInfo.traitData.SynergyTrigger[i + 1].ToString();
                    
                    if (i == traitInfoInfo.synergyLevel - 1)
                    {
                        //statusTexts[i].text = $"{traitInfoInfo.activeNumber}/{traitInfoInfo.traitData.SynergyTrigger[i]}";
                        statusTexts[i].color = GetSynergyColor(traitInfoInfo.synergyLevel);
                    }
                    else
                    {
                        //statusTexts[i].text = traitInfoInfo.traitData.SynergyTrigger[i].ToString();
                        statusTexts[i].color = Color.white;
                    }
                }
                else
                {
                    statusTexts[i].gameObject.SetActive(false);
                }
            }

            if (traitInfoInfo.synergyLevel == -1)
            {
                image.color = noneColor;
            }
            else
            {
                image.color = GetSynergyColor(traitInfoInfo.synergyLevel);
            }
        }

        Color GetSynergyColor(int synergyLevel)
        {
            if (synergyLevel >= colors.Length) return colors[colors.Length - 1];
            else return colors[synergyLevel];
        }

        public void OnClick()
        {
            uiManager.OnClickTrait(transform.position , activeTraitInfo);
        }
    }
}

