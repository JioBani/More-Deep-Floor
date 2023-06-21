using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Encouraging",
        //menuName = "Scriptable Object/Trait Data/Job/Encouraging",
        menuName = "Scriptable Object/Trait Data/Character/Encouraging",
        order = int.MaxValue)]
    public class Trait_Encouraging : TraitData
    {
        [SerializeField] private int[] percent;
        public int[] Percent => percent;

        [SerializeField] private StateData_Effect_Encouraging effect;
        public StateData_Effect_Encouraging Effect => effect;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Encouraging(this);
        }
    }

    public class RuntimeTrait_Encouraging : RuntimeTraitData
    {
        public int[] currentPercent;
        public RuntimeTrait_Encouraging(Trait_Encouraging _data) : base(_data)
        {
            currentPercent = _data.Percent.Clone() as int[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Encouraging(traitData.TraitStateData ,this,defender);
        }
    }
}

