using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Sniper",
        menuName = "Scriptable Object/Trait Data/Job/Sniper",
    order = int.MaxValue)]

    public class Trait_Sniper : TraitData
    {
        [SerializeField] private float[] percent;
        public float[] Percent => percent;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Sniper(this);
        }
    }

    public class RuntimeTrait_Sniper : RuntimeTraitData
    {
        public float[] percent;
        public RuntimeTrait_Sniper(Trait_Sniper _data) : base(_data)
        {
            percent = _data.Percent.Clone() as float[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Sniper(traitData.TraitStateData, this, defender);
        }
    }
}


