using LNK.MoreDeepFloor.Data.Defenders.States.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Greedy",
        //menuName = "Scriptable Object/Trait Data/Job/Greedy",
        menuName = "Scriptable Object/Trait Data/Character/Greedy",
        order = int.MaxValue)]
    public class Trait_Greedy : TraitData
    {
        [SerializeField] private float[] percent;
        public float[] Percent => percent;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Greedy(this);
        }
    }

    public class RuntimeTrait_Greedy : RuntimeTraitData
    {
        public float[] currentPercent;
        
        public RuntimeTrait_Greedy(Trait_Greedy _data) : base(_data)
        {
            currentPercent = _data.Percent;
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Greedy(traitData.TraitStateData , this , defender);
        }
    }
}

