using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Palettes;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class DefenderButton : MonoBehaviour
    {
        [SerializeField] private Image defenderImage;
        [SerializeField] private Image frame;
        private DefenderOriginalData defenderOriginalData;
        private EventManager eventManager;

        private void Awake()
        {
            eventManager = ReferenceManager.instance.eventManager;
        }

        public void SetData(DefenderOriginalData _defenderOriginalData)
        {
            defenderOriginalData = _defenderOriginalData;
            defenderImage.sprite = defenderOriginalData.Sprite;
            frame.color = Palette.defenderCostColors[_defenderOriginalData.Cost];
        }

        public void SetBlank()
        {
            defenderImage.sprite = null;
        }

        public void OnClick()
        {
            eventManager.SetOnClickDefenderAction(defenderOriginalData);
        }
    }

}

