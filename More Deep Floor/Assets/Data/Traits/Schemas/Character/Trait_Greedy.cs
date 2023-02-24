using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}

