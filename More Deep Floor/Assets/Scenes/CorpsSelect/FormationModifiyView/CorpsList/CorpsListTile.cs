using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.CorpsSelectScene.CorpsInfoViews;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class CorpsListTile : MonoBehaviour
    {
        private CorpsData corpsData;
        [SerializeField] private Image tileImage;
        private CorpsInfoView corpsInfoView;
        private EventManager eventManager;

        private void Awake()
        {
            eventManager = ReferenceManager.instance.eventManager;
            corpsInfoView = ReferenceManager.instance.corpsInfoView;
        }

        public void SetCorpData(CorpsData _corpsData)
        {
            corpsData = _corpsData;
            tileImage.sprite = corpsData.CommanderTileImage;
        }

        public void OnClick()
        {
            eventManager.SetOnClickCorpsListTileAction(corpsData);
        }
    }
}


