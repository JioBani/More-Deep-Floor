using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui.DefenderInfoUi;
using LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private DefenderInfo defenderInfo;
        [SerializeField] private TraitDetails traitDetails;

        public void OnClickDefender(Defender defender)
        {
            if (defenderInfo.gameObject.activeSelf)
            {
                defenderInfo.SetOff();
                defenderInfo.SetOn(defender);
            }
            else
            {
                defenderInfo.SetOn(defender);
            }
        }

        public void OnClickTrait(Vector2 pos , BattleFieldTraitInfo battleFieldTraitInfo)
        {
            if (traitDetails.gameObject.activeSelf)
            {
                traitDetails.SetOff();
                traitDetails.SetOn(pos , battleFieldTraitInfo);
            }
            else
            {
                traitDetails.SetOn(pos , battleFieldTraitInfo);
            }
        }
    }
}


