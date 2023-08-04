using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Traits.Corps;
using LNK.MoreDeepFloor.Data.Troops;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Corps
{
    [CreateAssetMenu(
        fileName = "CorpsData",
        menuName = "Scriptable Object/Crops/CorpsData",
        order = int.MaxValue)]
    
    public class CorpsData : TraitData
    {
        [SerializeField] private CorpsId corpsId;
        public CorpsId CorpsId => corpsId;
        
        [SerializeField] private string corpsName;
        public string CorpsName => corpsName;
        
        [SerializeField] private string commanderName;
        public string CommanderName => commanderName;
        
        [SerializeField] private Sprite commanderImage;
        public Sprite CommanderImage => commanderImage;
        
        [SerializeField] private Sprite commanderTileImage;
        public Sprite CommanderTileImage => commanderTileImage;

        [SerializeField] private List<DefenderOriginalData> members;
        public List<DefenderOriginalData> Members => members;

        /*[SerializeField] private CorpsTraitData corpsTraitData;
        public CorpsTraitData CorpsTraitData => corpsTraitData;*/
    }
}


