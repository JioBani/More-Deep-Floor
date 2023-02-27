using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi
{
    public class TraitInfoViewer : MonoBehaviour
    {
        private TraitInfoView[] views;
        private List<BattleFieldTraitInfo> list;

        private void Awake()
        {
            views = new TraitInfoView[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                views[i] = transform.GetChild(i).GetComponent<TraitInfoView>();
                views[i].gameObject.SetActive(false);
            }

            ReferenceManager.instance.traitManager.OnTraitChangeAciton += RefreshTrait;
        }

        public void RefreshTrait(Dictionary<TraitId, BattleFieldTraitInfo> traits)
        {
            List<BattleFieldTraitInfo> list = new List<BattleFieldTraitInfo>();
            
            foreach (var trait in traits)
            {
                if(trait.Value.synergyLevel != -1)
                    list.Add(trait.Value);
            }
            
            list = list.OrderByDescending(element => element.synergyLevel).ToList();
            
            for (var i = 0; i < views.Length; i++)
            {
                if (i < list.Count)
                {
                    views[i].gameObject.SetActive(true);
                    views[i].RefreshTrait(list[i]);
                }
                else
                {
                    views[i].gameObject.SetActive(false);
                }
            }
        }
        
    }
}


