using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi
{
    public class TraitInfoView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private Text[] statusTextsts;
        [SerializeField] private Color[] colors;

        public void RefreshTrait(BattleFieldTraitInfo traitInfo)
        {
            image.sprite = traitInfo.traitData.Image;

            for (var i = 0; i < traitInfo.traitData.SynergyTrigger.Length; i++)
            {
                if (i == traitInfo.synergyLevel)
                {
                    statusTextsts[i].text = $"{traitInfo.nums}/{traitInfo.traitData.SynergyTrigger[i]}";
                    statusTextsts[i].color = colors[traitInfo.synergyLevel];
                }
                else
                {
                    statusTextsts[i].text = traitInfo.traitData.SynergyTrigger[i].ToString();
                    statusTextsts[i].color = Color.white;
                }
            }
            
            string str;
            
            /*if (traitInfo.synergyLevel + 1 < traitInfo.traitData.SynergyTrigger.Length)
            {
                if (traitInfo.nums == traitInfo.traitData.SynergyTrigger[traitInfo.synergyLevel])
                {
                    str = $"{traitInfo.nums}/{traitInfo.traitData.SynergyTrigger[traitInfo.synergyLevel]}";
                }
                else
                {
                    str = $"{traitInfo.nums}/{traitInfo.traitData.SynergyTrigger[traitInfo.synergyLevel + 1]}";
                }
            }
            else
            {
                str = $"{traitInfo.nums}/{traitInfo.traitData.SynergyTrigger[traitInfo.synergyLevel]}";
            }

            statusText.text = str;*/

        }
    }
}

