using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(fileName = "Gladiator", menuName = "Scriptable Object/Trait Data/Job/Gladiator", order = int.MaxValue)]
    public class Trait_Gladiator : TraitData
    {
        [SerializeField]private StateData_Effect_Gladiator effect;
        public StateData_Effect_Gladiator Effect => effect;
        
        [SerializeField]private float[] attackSpeedUp;
        public float[] AttackSpeedUp => attackSpeedUp;
        
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Gladiator(this);
        }
    }
    
    public class RuntimeTrait_Gladiator : RuntimeTraitData
    {
        public float[] currentAttackSpeedUp;

        public RuntimeTrait_Gladiator(Trait_Gladiator _data) : base(_data)
        {
            currentAttackSpeedUp = _data.AttackSpeedUp.Clone() as float[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Gladiator(traitData.TraitStateData, this, defender);
        }
    }
}


