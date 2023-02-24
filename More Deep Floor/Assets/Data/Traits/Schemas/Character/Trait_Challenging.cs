using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}

