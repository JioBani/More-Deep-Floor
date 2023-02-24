using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}

