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
    public class DefenderImage
    {
        public GameObject gameObject;
        Image image;
        Image frame;

        public DefenderImage(GameObject _gameObject)
        {
            gameObject = _gameObject;
            image = _gameObject.transform.GetChild(0).GetComponent<Image>();
            frame = _gameObject.transform.GetChild(1).GetComponent<Image>();
        }

        public void SetOn(Sprite sprite, Color color)
        {
            image.sprite = sprite;
            frame.color = color;
            gameObject.SetActive(true);
        }

        public void SetOff()
        {
            gameObject.SetActive(false);
        }
    }
    
    public class TraitInfoView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private Text[] statusTexts;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI effectsText;

        [SerializeField] private Color[] colors;
        [SerializeField] private GameObject panelCanvas;
        [SerializeField] private GameObject defenderImageParents;
        private DefenderImage[] defenderImages;

        private TraitManager traitManager;
        private TraitId traitId = TraitId.None;
        private bool isPanelOpen = false;

        public void Awake()
        {
            defenderImages = new DefenderImage[defenderImageParents.transform.childCount];
            for (int i = 0; i < defenderImages.Length; i++)
            {
                defenderImages[i] = new DefenderImage(defenderImageParents.transform.GetChild(i).gameObject);
            }

            traitManager = ReferenceManager.instance.traitManager;
            panelCanvas.SetActive(false);
        }

        public void RefreshTrait(BattleFieldTraitInfo traitInfo)
        {
            image.sprite = traitInfo.traitData.Image;
            nameText.text = traitInfo.traitData.TraitName;
            descriptionText.text = traitInfo.traitData.Description;
            effectsText.text = traitInfo.traitData.TriggerDescription;

            for (var i = 0; i < traitInfo.traitData.SynergyTrigger.Length; i++)
            {
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

            if (traitId == traitInfo.traitData.Id) return;
            else traitId = traitInfo.traitData.Id;

            List<DefenderOriginalData> defenders = traitManager.defenderSortByTrait[traitInfo.traitData.Id];

            for (var i = 0; i < defenderImages.Length; i++)
            {
                if (i < defenders.Count)
                {
                    defenderImages[i].SetOn(defenders[i].Sprite , colors[defenders[i].Cost - 1]);
                }
                else
                {
                    defenderImages[i].SetOff();
                }

            }
        }

        public void OnClick()
        {
            if (!isPanelOpen)
            {
                panelCanvas.SetActive(true);
                isPanelOpen = true;
            }
            else
            {
                panelCanvas.SetActive(false);
                isPanelOpen = false;
            }

        }
    }
}

