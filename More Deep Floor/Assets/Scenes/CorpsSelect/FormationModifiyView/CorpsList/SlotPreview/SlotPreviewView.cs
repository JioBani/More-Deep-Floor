using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class SlotPreviewView : MonoBehaviour
    {
        [SerializeField] private SlotPreview[] slotPreviews;
        private EventManager eventManager;

        private void Awake()
        {
            eventManager = ReferenceManager.instance.eventManager;
            
            eventManager.AddOnClickCorpsListTileAction(OnClickListItem);
        }

        public void OnClickListItem(CorpsData corpsData)
        {
            if (slotPreviews.HaveConditions((preview) => (preview.corpsData == corpsData))) return;
            
            for (var i = 0; i < slotPreviews.Length; i++)
            {
                if (!slotPreviews[i].isSelected)
                {
                    slotPreviews[i].SetCorpsData(corpsData);
                    break;
                }
                else if(i == 3)
                {
                    slotPreviews[3].SetCorpsData(corpsData);
                }
            }
        }

        public void OnAdapt()
        {
            List<CorpsData> corpsDatas = slotPreviews.MakeToList((preview) => preview.corpsData);
            
            if (corpsDatas.Contains(null))
            {
                CustomLogger.Log("[SlotPreviewView.OnAdapt()] 4개가 모두 선택되지 않음");
                return;
            }
            
            eventManager.SetOnAdaptAction(corpsDatas);
        }
    }
}


