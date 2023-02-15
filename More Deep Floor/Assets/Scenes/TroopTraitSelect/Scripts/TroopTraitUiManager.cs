using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.TroopTraitSelect
{
    public class TroopTraitUiManager : MonoBehaviour
    {
        [SerializeField]private GameObject traitButtonObject;
        private List<TraitSelectButton> buttons;
        [SerializeField] private TraitUpgradeViewer upgradeViewer;
        

        private void Awake()
        {
            buttons = new List<TraitSelectButton>();
            
            for (int i = 0; i < traitButtonObject.transform.childCount; i++)
            {
                buttons.Add(traitButtonObject.transform.GetChild(i).GetComponent<TraitSelectButton>());
            }
        }

        public void OnClickTrait(TroopTrait trait)
        {
            upgradeViewer.SetData(trait);
            Refresh();
        }

        void Refresh()
        {
            foreach (var traitSelectButton in buttons)
            {
                traitSelectButton.ResetClicked();
            }
        }
    }
}


