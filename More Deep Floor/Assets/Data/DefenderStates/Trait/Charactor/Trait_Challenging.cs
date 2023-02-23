using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;



namespace Test
{
    [CreateAssetMenu(
        fileName = "Trait_Challenging", 
        menuName = "Scriptable Object/Defender State Data/Trait/Challenging", 
        order = int.MaxValue)]
    
    public class Trait_Challenging : ParameterSo
    {
        public List<float> hpPer;
    }
}

