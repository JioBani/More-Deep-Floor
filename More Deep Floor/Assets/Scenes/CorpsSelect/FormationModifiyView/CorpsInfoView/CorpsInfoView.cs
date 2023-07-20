using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene.CorpsInfoViews
{
    public class CorpsInfoView : MonoBehaviour
    {
        private CorpsData corpsData;
        
        [SerializeField] private TextMeshProUGUI corpsName;
        [SerializeField] private TextMeshProUGUI commanderName;
        [SerializeField] private Image commanderImage;

        private void Awake()
        {
            ReferenceManager.instance.eventManager.AddOnClickCorpsListTileAction(SetCorpsDate);
        }

        public void SetCorpsDate(CorpsData _corpsData)
        {
            corpsData = _corpsData;
            corpsName.text = corpsData.CorpsName;
            commanderName.text = corpsData.CommanderName;
            commanderImage.sprite = corpsData.CommanderImage;
        }
    }
}


