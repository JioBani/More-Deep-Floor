using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.Style;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LNK.MoreDeepFloor.InGame.Ui.DefenderDataInfoUi
{
    public class DefenderDataInfo : MonoBehaviour
    {
        
        private DefenderData defenderData;
        private DefenderStatus defenderStatus;
        [SerializeField] private CommonPalette commonPalette;

        private Camera camera;

        [SerializeField] private Image defenderImage;
        [SerializeField] private Image defenderFrame;
        
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private Image jobImage;
        [SerializeField] private TextMeshProUGUI jobName;
        
        [SerializeField] private Image characterImage;
        [SerializeField] private TextMeshProUGUI characterName;
        
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        [SerializeField] private TextMeshProUGUI rangeText;
        [SerializeField] private TextMeshProUGUI manaText;
        
        public void SetOn(DefenderData _defenderData , Vector2 pos)
        {
            defenderData = _defenderData;

            defenderImage.sprite = defenderData.sprite;
            defenderFrame.color = commonPalette.CostColors[defenderData.cost];
            nameText.text = defenderData.name;

            jobImage.sprite = defenderData.job.Image;
            jobName.text = defenderData.job.TraitName;
            
            characterImage.sprite = defenderData.character.Image;
            characterName.text = defenderData.character.TraitName;

            damageText.text = MakeDamageValue(defenderData.damages.currentValues);
            attackSpeedText.text = MakeAttackSpeedValue(defenderData.attackSpeeds.currentValues);
            
            gameObject.SetActive(true);
            transform.position = pos;

        }

        public void SetOff()
        {
            gameObject.SetActive(false);
        }

        string MakeDamageValue(float[] damages)
        {
            string str = "";
            for (var i = 0; i < damages.Length - 1; i++)
            {
                str += (int)damages[i] + " /";
            }

            str += " "+ damages[damages.Length - 1];
            return str;
        }

        string MakeAttackSpeedValue(float[] attackSpeeds)
        {
            string str = "";
            for (var i = 0; i < attackSpeeds.Length - 1; i++)
            {
                str += $"{attackSpeeds[i]:0.#0}" + " /";
            }

            str += $" {attackSpeeds[attackSpeeds.Length - 1]:0.#0}";
            return str;
        }
    }
}


