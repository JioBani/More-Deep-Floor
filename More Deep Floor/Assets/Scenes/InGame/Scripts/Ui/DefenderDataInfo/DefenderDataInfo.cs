using System;
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
    [Serializable]
    public class DefenderDataInfo : MonoBehaviour
    {
        
        private DefenderData defenderData;
        [SerializeField] private CommonPalette commonPalette;

        private Camera mainCamera;

        [SerializeField] private Image defenderImage;
        [SerializeField] private Image defenderFrame;
        
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private Image corpsImage;
        [SerializeField] private TextMeshProUGUI corpsName;
        
        [SerializeField] private Image personalityImage;
        [SerializeField] private TextMeshProUGUI personalityName;
        
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

            corpsImage.sprite = defenderData.corpsTraitData.Image;
            corpsName.text = defenderData.corpsTraitData.CopsName;
            
            personalityImage.sprite = defenderData.personalityData.Image;
            personalityName.text = defenderData.personalityData.PersonalityName;
            
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


