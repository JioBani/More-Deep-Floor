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

            ReferenceManager.instance.traitManager.OnTraitChangeAction += RefreshTrait;
        }

        public void RefreshTrait(Dictionary<TraitId, BattleFieldTraitInfo> traits)
        {
            List<BattleFieldTraitInfo> list = new List<BattleFieldTraitInfo>();
            
            foreach (var trait in traits)
            {
                if(trait.Value.nums > 0)
                    list.Add(trait.Value);
            }
            
            list.Sort((a, b) =>
            {
                if (a.synergyLevel > b.synergyLevel)
                {
                    return -1;
                }
                if(a.synergyLevel < b.synergyLevel)
                {
                    return 1;
                }
                else
                {
                    return b.nums.CompareTo(a.nums);
                }
            });
            
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


