using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Style
{
    [CreateAssetMenu(fileName = "CommonPalette", menuName = "Scriptable Object/Style/CommonPalette", order = int.MaxValue)]
    
    public class CommonPalette : ScriptableObject
    {
        [SerializeField] private List<Color> costColors;
        public List<Color> CostColors => costColors;
    }
}


