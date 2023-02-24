using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
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
    }
}

