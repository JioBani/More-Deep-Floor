using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}


