using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Ui.DefenderInfoUi;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private DefenderInfo defenderInfo;

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
    }
}


