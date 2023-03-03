using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.Style;
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
    
    public class TraitDetails : MonoBehaviour
    {
        [SerializeField] private GameObject defenderImageParents;
        private DefenderImage[] defenderImages;
        private TraitManager traitManager;
        [SerializeField] private CommonPalette palette;
        [SerializeField] private Text name;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI[] effectTexts;
        [SerializeField] private Color traitOnColor;
        [SerializeField] private Color traitOffColor;
        
        public void Awake()
        {
            traitManager = ReferenceManager.instance.traitManager;
            
            defenderImages = new DefenderImage[defenderImageParents.transform.childCount];
            
            for (int i = 0; i < defenderImages.Length; i++)
            {
                defenderImages[i] = new DefenderImage(defenderImageParents.transform.GetChild(i).gameObject);
            };
            
            gameObject.SetActive(false);
        }

        public void SetOn(Vector2 pos ,BattleFieldTraitInfo traitInfo)
        {
            List<DefenderOriginalData> defenders = traitManager.defenderSortByTrait[traitInfo.traitData.Id];
            
            for (var i = 0; i < defenderImages.Length; i++)
            {
                if (i < defenders.Count)
                {
                    defenderImages[i].SetOn(defenders[i].Sprite , palette.CostColors[defenders[i].Cost - 1]);
                }
                else
                {
                    defenderImages[i].SetOff();
                }
            }

            name.text = traitInfo.traitData.TraitName;
            description.text = traitInfo.traitData.Description;
            
            for (var i = 0; i < effectTexts.Length; i++)
            {
                if (i < traitInfo.traitData.Effects.Length)
                {
                    if (i == traitInfo.synergyLevel)
                    {
                        effectTexts[i].color = traitOnColor;
                    }
                    else
                    {
                        effectTexts[i].color = traitOffColor;
                    }
                    effectTexts[i].text = traitInfo.traitData.GetEffect(i);
                    effectTexts[i].gameObject.SetActive(true);
                }
                else
                {
                    effectTexts[i].text = "";
                    effectTexts[i].gameObject.SetActive(false);
                }
            }

            transform.position = pos;
            gameObject.SetActive(true);
        }

        public void SetOff()
        {
            gameObject.SetActive(false);
        }
    }
}


