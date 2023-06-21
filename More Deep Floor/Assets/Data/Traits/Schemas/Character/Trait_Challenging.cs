using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Challenging",
        //menuName = "Scriptable Object/Trait Data/Job/Challenging",
        menuName = "Scriptable Object/Trait Data/Character/Challenging",
        order = int.MaxValue)]
    public class Trait_Challenging : TraitData
    {
        [SerializeField] private float[] maxHpPer;
        public float[] MaxHpPer => maxHpPer;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Challenging(this);
        }
    }

    public class RuntimeTrait_Challenging : RuntimeTraitData
    {
        public float[] currentMaxHpPer;

        public RuntimeTrait_Challenging(Trait_Challenging _data) : base(_data)
        {
            currentMaxHpPer = _data.MaxHpPer.Clone() as float[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Challenging(traitData.TraitStateData, this, defender);
        }
    }
}

