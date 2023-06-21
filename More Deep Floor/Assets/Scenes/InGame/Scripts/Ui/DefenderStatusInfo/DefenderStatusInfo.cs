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
        private DefenderStatus defenderStatus;

        private Camera mainCamera;

        [SerializeField] private Image defenderImage;
        
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private Image jobImage;
        [SerializeField] private TextMeshProUGUI jobName;
        
        [SerializeField] private Image characterImage;
        [SerializeField] private TextMeshProUGUI characterName;
        
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        [SerializeField] private TextMeshProUGUI rangeText;
        [SerializeField] private TextMeshProUGUI manaText;

        private bool isOn = false;

        public void Awake()
        {
            mainCamera = Camera.main;
        }

        public void SetOn(Defender _defender)
        {
            defender = _defender;
            defenderData = _defender.status.defenderData;
            defenderStatus = _defender.status;

            defenderImage.sprite = defenderData.sprite;
            nameText.text = defenderData.name;

            jobImage.sprite = defenderData.job.Image;
            jobName.text = defenderData.job.TraitName;
            
            characterImage.sprite = defenderData.character.Image;
            characterName.text = defenderData.character.TraitName;

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
                damageText.text = defenderStatus.damage.currentValue.ToString();
                attackSpeedText.text = defenderStatus.attackSpeed.currentValue.ToString();
            }
        }

        public void OnClickClose()
        {
            SetOff();
        }
    }
}


