using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class DefenderInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI defenderName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private List<StatusText> statusTexts;

        private DefenderOriginalData defenderOriginalData;

        private void Awake()
        {
            ReferenceManager.instance.eventManager.AddOnClickDefenderAction(SetDefenderData);
        }

        void SetDefenderData(DefenderOriginalData _defenderOriginalData)
        {
            defenderOriginalData = _defenderOriginalData;

            defenderName.text = defenderOriginalData.name;
            
            statusTexts[0].SetStatus(_defenderOriginalData.Status.Damages);
            statusTexts[1].SetStatus(_defenderOriginalData.Status.AttackSpeeds);
            statusTexts[2].SetStatus(_defenderOriginalData.Status.Ranges);
            statusTexts[3].SetStatus(_defenderOriginalData.Status.CriticalRates);
            statusTexts[4].SetStatus(_defenderOriginalData.Status.MaxManas);
            
            // description.text = defenderOriginalData.
        }
    }
}


