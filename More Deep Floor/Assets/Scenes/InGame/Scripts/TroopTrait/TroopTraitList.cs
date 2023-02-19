using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.TroopTraitSelect;
using UnityEngine;


namespace LNK.MoreDeepFloor.InGame.TroopTraitSystem
{
    public class TroopTrait_None : TroopTrait
    {
        public TroopTrait_None(TroopTraitData _traitData, int _level) : base(_traitData, _level)
        {
            type = TroopTraitType.None;
        }
    }
    
    public class TroopTrait_StartGold : TroopTrait
    {
        public TroopTrait_StartGold(TroopTraitData _traitData, int _level) : base(_traitData, _level)
        {
            type = TroopTraitType.OnDataLoad;
        }

        public override void OnDataLoadAction()
        {
            ReferenceManager.instance.marketManager.GoldChange(10 , "TroopTrait_StartGold" );
        }
    }
    
    public class TroopTrait_RoundInterest : TroopTrait
    {
        public TroopTrait_RoundInterest(TroopTraitData _traitData, int _level) : base(_traitData, _level)
        {
            type = TroopTraitType.OnDataLoad;
        }

        public override void OnDataLoadAction()
        {
            ReferenceManager.instance.marketManager.SetInterestLimit(5);
        }
    }
    
    public class TroopTrait_AttackSpeedUp : TroopTrait
    {
        public TroopTrait_AttackSpeedUp(TroopTraitData _traitData, int _level) : base(_traitData, _level)
        {
            type = TroopTraitType.OnDataLoad;
        }

        public override void OnDataLoadAction()
        {
            ReferenceManager.instance.defenderManager.AddAttackSpeed(1);
        }
    }
}

