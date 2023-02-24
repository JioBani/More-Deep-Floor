using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}