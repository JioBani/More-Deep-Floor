using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui.DefenderDataInfoUi;
using LNK.MoreDeepFloor.InGame.Ui.DefenderStatusInfoUi;
using LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi;
using UnityEngine;
using UnityEngine.Serialization;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class UiManager : MonoBehaviour
    {
        [FormerlySerializedAs("defenderInfo")] [SerializeField] private DefenderStatusInfo defenderStatusInfo;
        [SerializeField] private TraitDetailsView traitDetailsView;
        [SerializeField] private DefenderDataInfo defenderDataInfo;

        public void OnClickDefender(Defender defender)
        {
            if (defenderStatusInfo.gameObject.activeSelf)
            {
                defenderStatusInfo.SetOff();
                defenderStatusInfo.SetOn(defender);
            }
            else
            {
                defenderStatusInfo.SetOn(defender);
            }
        }

        public void OnClickTrait(Vector2 pos , ActiveTraitInfo activeTraitInfo)
        {
            if (traitDetailsView.gameObject.activeSelf)
            {
                traitDetailsView.SetOff();
                traitDetailsView.SetOn(pos , activeTraitInfo);
            }
            else
            {
                traitDetailsView.SetOn(pos , activeTraitInfo);
            }
        }

        public void OnClickDefenderImage(DefenderData defenderData)
        {
            if (defenderDataInfo.gameObject.activeSelf)
            {
                defenderDataInfo.SetOff();
                defenderDataInfo.SetOn(defenderData , Input.mousePosition);
            }
            else
            {
                defenderDataInfo.SetOn(defenderData , Input.mousePosition);
            }
        }
    }
}


