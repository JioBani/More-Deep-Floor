using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui.TraitInfoUi
{
    public class ActiveTraitInfoViewController : MonoBehaviour
    {
        private TraitManager traitManager;
        private List<TraitInfoView> traitInfoViews;
        [SerializeField]private List<ActiveTraitInfo> list = new List<ActiveTraitInfo>();

        private void Awake()
        {
            traitInfoViews = new List<TraitInfoView>();
            
            transform.EachChild((child) =>
            {
                traitInfoViews.Add(child.GetComponent<TraitInfoView>());
            });
            
            traitManager = ReferenceManager.instance.traitManager;
            traitManager.OnTraitChangeAction += Refresh;
        }

        void Refresh(Dictionary<TraitId, ActiveTraitInfo> dictionary)
        {
            list.Clear();
            list = dictionary.Values.ToList();
            list.Sort((a, b) =>
            {
                if (a.synergyLevel > b.synergyLevel) return -1;
                if (a.synergyLevel < b.synergyLevel) return 1;
                else
                {
                    if (a.activeNumber > b.activeNumber) return -1;
                    if (a.activeNumber < b.activeNumber) return 1;
                    else return 0;
                }
            });
            
            for (var i = 0; i < traitInfoViews.Count; i++)
            {
                if (i < list.Count)
                {
                    traitInfoViews[i].gameObject.SetActive(true);
                    traitInfoViews[i].RefreshTrait(list[i]);
                }
                else
                {
                    traitInfoViews[i].gameObject.SetActive(false);
                }

            }
        }
    }
}


