using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
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
    
    public class TroopTrait_GoldAttack : TroopTrait
    {
        public TroopTrait_GoldAttack(TroopTraitData _traitData, int _level) : base(_traitData, _level)
        {
            type = TroopTraitType.OnDataLoad;
        }

        public override void OnDataLoadAction()
        {
            ReferenceManager.instance.defenderManager.OnDefenderSpawnAction -= AddState;
            ReferenceManager.instance.defenderManager.OnDefenderSpawnAction += AddState;
        }

        void AddState(Defender defender)
        {
            defender.GetComponent<DefenderStateController>().AddState(DefenderStateId.TroopTrait_GoldAttack);
            Debug.Log("TroopTrait_GoldAttack.AddState");
        }
    }
}

