using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Coercive",
        //menuName = "Scriptable Object/Trait Data/Job/Coercive",
        menuName = "Scriptable Object/Trait Data/Character/Coercive",
        order = int.MaxValue)]
    public class Trait_Coercive : TraitData
    {
        [SerializeField] private float[] percent;
        public float[] Percent => percent;

        [SerializeField] private float[] time;
        public float[] Time => time;
        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeTrait_Coercive(this);
        }
    }

    public class RuntimeTrait_Coercive : RuntimeTraitData
    {
        public float[] currentPercent;
        public float[] currentTime;
        
        public RuntimeTrait_Coercive(Trait_Coercive _data) : base(_data)
        {
            currentPercent = _data.Percent.Clone() as float[];
            currentTime = _data.Time.Clone() as float[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Coercive(traitData.TraitStateData, this, defender);
        }
    }
}

