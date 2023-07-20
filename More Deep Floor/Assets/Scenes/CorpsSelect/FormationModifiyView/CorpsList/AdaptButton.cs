using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene.CorpsInfoViews
{
    public class AdaptButton : MonoBehaviour
    {
        [SerializeField] private SlotPreviewView slotPreviewView;

        public void OnClick()
        {
            slotPreviewView.OnAdapt();
        }
    }
}


