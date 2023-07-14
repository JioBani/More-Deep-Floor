using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.Style;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi
{
    public class TraitDetailsView : MonoBehaviour
    {
        [SerializeField] private GameObject defenderImageParents;
        [SerializeField] private ContentSizeFitter contentSizeFitter;
        private DefenderImage[] defenderImages;
        private TraitManager traitManager;
        [SerializeField] private CommonPalette palette;
        [SerializeField] private Text traitName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI[] effectTexts;
        [SerializeField] private Color traitOnColor;
        [SerializeField] private Color traitOffColor;

        [SerializeField] private int horizontalImageLimit;
        
        public void Awake()
        {
            traitManager = ReferenceManager.instance.traitManager;
            
            defenderImages = new DefenderImage[defenderImageParents.transform.childCount];
            
            for (int i = 0; i < defenderImages.Length; i++)
            {
                defenderImages[i] = defenderImageParents.transform.GetChild(i).GetComponent<DefenderImage>();
            };
            
            gameObject.SetActive(false);
        }

        public void SetOn(Vector2 pos ,ActiveTraitInfo activeTraitInfo)
        {
            //List<DefenderOriginalData> defenders = traitManager.defenderSortByTrait[traitInfo.traitData.Id];
            List<DefenderOriginalData> defenders = activeTraitInfo.traitData.Members;

            for (var i = 0; i < defenderImages.Length; i++)
            {
                if (i < defenders.Count)
                {
                    defenderImages[i].SetOn(new DefenderData(defenders[i]));
                }
                else
                {
                    defenderImages[i].SetOff();
                }
            }

            traitName.text = activeTraitInfo.traitData.name;
            description.text = activeTraitInfo.traitData.Description;
            
            /*for (var i = 0; i < effectTexts.Length; i++)
            {
                if (i < activeTraitInfo.traitData.SynergyTrigger.Length)
                {
                    if (i == activeTraitInfo.synergyLevel)
                    {
                        effectTexts[i].color = traitOnColor;
                    }
                    else
                    {
                        effectTexts[i].color = traitOffColor;
                    }
                    //TODO effectTexts[i].text = battleFieldTrait.traitData.Description; => effectTexts[i].text = battleFieldTrait.traitData.GetEffcet(i);
                    effectTexts[i].text = activeTraitInfo.traitData.Description;
                    effectTexts[i].gameObject.SetActive(true);
                }
                else
                {
                    effectTexts[i].text = "";
                    effectTexts[i].gameObject.SetActive(false);
                }
            }*/

            transform.position = pos;
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }

        public void SetOff()
        {
            gameObject.SetActive(false);
        }
    }
}


