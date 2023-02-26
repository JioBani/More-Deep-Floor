using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Tenacious",
        //menuName = "Scriptable Object/Trait Data/Job/Tenacious",
        menuName = "Scriptable Object/Trait Data/Character/Tenacious",
        order = int.MaxValue)]
    public class Trait_Tenacious : TraitData
    {
        int[] percent;
    }
}