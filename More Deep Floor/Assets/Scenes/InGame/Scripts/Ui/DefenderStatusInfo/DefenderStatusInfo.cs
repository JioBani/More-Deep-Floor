using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.DefenderStatusInfoUi
{
    
    public class DefenderStatusInfo : MonoBehaviour
    {
        private Defender defender;
        private DefenderData defenderData;
        private EntityStatus defenderStatus;

        private Camera mainCamera;

        [SerializeField] private Image defenderImage;
        
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private Image jobImage;
        [SerializeField] private TextMeshProUGUI jobName;
        
        [SerializeField] private Image characterImage;
        [SerializeField] private TextMeshProUGUI characterName;
        
        [SerializeField] private TextMeshProUGUI ppText;
        [SerializeField] private TextMeshProUGUI spText;
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        [SerializeField] private TextMeshProUGUI rangeText;
        [SerializeField] private TextMeshProUGUI criticalRateText;
        [SerializeField] private TextMeshProUGUI maxHpText;
        [SerializeField] private TextMeshProUGUI pdText;
        [SerializeField] private TextMeshProUGUI sdText;
        [SerializeField] private TextMeshProUGUI maxManaText;
        [SerializeField] private TextMeshProUGUI moveSpeedText;

        private bool isOn = false;

        public void Awake()
        {
            mainCamera = Camera.main;
        }

        public void SetOn(Defender _defender)
        {
            defender = _defender;
            defenderData = _defender.defenderData;
            defenderStatus = _defender.status;

            defenderImage.sprite = defenderData.sprite;
            nameText.text = defenderData.name;

            //jobImage.sprite = defenderData.job.Image;
            //jobName.text = defenderData.job.TraitName;
            
            //characterImage.sprite = defenderData.character.Image;
            //characterName.text = defenderData.character.TraitName;

            isOn = true;
            gameObject.SetActive(true);
            transform.position = mainCamera.WorldToScreenPoint(_defender.gameObject.transform.position);
        }

        public void SetOff()
        {
            isOn = false;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isOn)
            {
                ppText.text = defenderStatus.damage.currentValue.ToString();
                spText.text = defenderStatus.magicalPower.currentValue.ToString();
                attackSpeedText.text = defenderStatus.attackSpeed.currentValue.ToString();
                rangeText.text = defenderStatus.range.currentValue.ToString();
                criticalRateText.text = defenderStatus.criticalRate.currentValue.ToString();
                
                maxHpText.text = defenderStatus.maxHp.currentValue.ToString();
                pdText.text = defenderStatus.physicalDefense.currentValue.ToString();
                sdText.text = defenderStatus.magicalDefense.currentValue.ToString();
                maxManaText.text = defenderStatus.maxMana.currentValue.ToString();
                moveSpeedText.text = defenderStatus.moveSpeed.currentValue.ToString();
            }
        }

        public void OnClickClose()
        {
            SetOff();
        }
    }
}


