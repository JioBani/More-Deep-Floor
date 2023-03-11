    using LNK.MoreDeepFloor.Data.Defenders.States.Schemas;
    using LNK.MoreDeepFloor.Data.Schemas;
    using LNK.MoreDeepFloor.InGame.Entity;
    using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
    using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Tenacious",
        //menuName = "Scriptable Object/Trait Data/Job/Tenacious",
        menuName = "Scriptable Object/Trait Data/Character/Tenacious",
        order = int.MaxValue)]
    public class Trait_Tenacious : TraitData
    {
        [SerializeField] private int[] percent;
        public int[] Percent => percent;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Tenacious(this);
        }
    }

    public class RuntimeTrait_Tenacious : RuntimeTraitData
    {
       // public Trait_Tenacious traitData;

        public int[] currentPercent;
        
        public RuntimeTrait_Tenacious(Trait_Tenacious _data) : base(_data)
        {
            //traitData = _data;
            currentPercent = _data.Percent.Clone() as int[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Tenacious(traitData.TraitStateData, this, defender);
        }
    }
}