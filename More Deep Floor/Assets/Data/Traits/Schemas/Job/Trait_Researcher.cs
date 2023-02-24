using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}

