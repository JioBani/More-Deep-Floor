using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Researcher",
        menuName = "Scriptable Object/Trait Data/Job/Researcher",
        order = int.MaxValue)]

    public class Trait_Researcher : TraitData
    {
        [SerializeField] private int[] percent;
        public int[] Percent => percent;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Researcher(this);
        }
    }

    public class RuntimeTrait_Researcher : RuntimeTraitData
    {
        public int[] currentPercent;
        
        public RuntimeTrait_Researcher(Trait_Researcher _data) : base(_data)
        {
            currentPercent = _data.Percent;
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Researcher(traitData.TraitStateData, this, defender);
        }
    }
}

