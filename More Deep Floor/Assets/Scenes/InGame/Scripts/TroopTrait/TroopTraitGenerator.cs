using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.TroopTraits;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;


namespace LNK.MoreDeepFloor.InGame.TroopTraitSystem
{
    public class TroopTraitGenerator : MonoBehaviour
    {
        [SerializeField] private TroopTraitTable troopTraitTable;

        public TroopTrait Get(TroopTraitId id , int level)
        {
            switch (id)
            {
                case TroopTraitId.None : return new TroopTrait(troopTraitTable.Get(id), level);
                case TroopTraitId.StartGold : return new TroopTrait_StartGold(troopTraitTable.Get(id), level);
                case TroopTraitId.RoundInterest : return new TroopTrait_RoundInterest(troopTraitTable.Get(id), level);
                default : return new TroopTrait(troopTraitTable.Get(id), level);
            }
        }
    }
}

