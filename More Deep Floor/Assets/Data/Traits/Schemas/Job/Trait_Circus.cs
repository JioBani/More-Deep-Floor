using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Circus",
        menuName = "Scriptable Object/Trait Data/Job/Circus",
        order = int.MaxValue)]

    public class Trait_Circus : TraitData
    {
        [SerializeField] private int[] targetNumber;
        public int[] TargetNumber => targetNumber;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Circus(this);
        }
    }

    public class RuntimeTrait_Circus : RuntimeTraitData
    {
        public int[] currentTargetNumber;
        
        public RuntimeTrait_Circus(Trait_Circus _data) : base(_data)
        {
            currentTargetNumber = _data.TargetNumber.Clone() as int[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Circus(traitData.TraitStateData, this, defender);
        }
    }
}

