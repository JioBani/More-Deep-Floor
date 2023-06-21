using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Defender Table Original Data", menuName = "Scriptable Object/Defender Table Original Data", order = int.MaxValue)]
    
    public class DefenderTableOriginalData : ScriptableObject
    {
        [SerializeField] public List<DefenderOriginalData> defenders;
        public  List<DefenderOriginalData> Defenders { get { return defenders; } }
    }
}

