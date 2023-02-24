using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(fileName = "Gladiator", menuName = "Scriptable Object/Trait Data/Job/Gladiator", order = int.MaxValue)]
    public class Trait_Gladiator : TraitData
    {
        [SerializeField]private StateData_Effect_Gladiator effect;
        public StateData_Effect_Gladiator Effect => effect;
        
        [SerializeField]private float[] attackSpeedUp;
        public float[] AttackSpeedUp => attackSpeedUp;
    }
}


