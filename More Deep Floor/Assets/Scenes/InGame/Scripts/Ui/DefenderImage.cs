using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.Style;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class DefenderImage : MonoBehaviour
    {
        private DefenderData defenderData;
        private UiManager uiManager = null;
        [SerializeField] private Image image;
        [SerializeField] private Image frame;
        [SerializeField] private CommonPalette palette;

        public void Awake()
        {
            gameObject.SetActive(false);
        }

        public void OnEnable()
        {
            if (uiManager == null) uiManager = ReferenceManager.instance.uiManager;
        }

        public void SetOn(DefenderData _defenderData)
        {
            defenderData = _defenderData;
            image.sprite = defenderData.sprite;
            frame.color = palette.CostColors[defenderData.cost];
            gameObject.SetActive(true);
        }

        public void SetOff()
        {
            gameObject.SetActive(false);
        }

        public void OnClick()
        {
            uiManager.OnClickDefenderImage(defenderData);
        }
        
    }
}


