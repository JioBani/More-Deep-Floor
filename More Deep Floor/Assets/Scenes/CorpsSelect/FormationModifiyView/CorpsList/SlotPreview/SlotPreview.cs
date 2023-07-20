using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class SlotPreview : MonoBehaviour
    {
        public CorpsData corpsData { get; private set; }
        public bool isSelected { get; private set; } = false;
        [SerializeField] private SlotPreviewView slotPreviewView;
        
        [SerializeField] private Image commanderTile;
        

        public void SetCorpsData(CorpsData _corpsData)
        {
            corpsData = _corpsData;
            commanderTile.sprite = corpsData.CommanderTileImage;
            isSelected = true;
        }

        public void OnClick()
        {
            commanderTile.sprite = null;
            corpsData = null;
            isSelected = false;
        }
    }
}
