using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas.TroopTraitScene;
using LNK.MoreDeepFloor.Data.TroopTraits;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "TroopTraitTable", menuName = "Scriptable Object/TroopTraitTable", order = int.MaxValue)]

    public class TroopTraitTable : ScriptableObject
    {
        [SerializeField] private TroopTraitData[] troopList;
        private Dictionary<TroopTraitId, TroopTraitData> dic = null;

        public TroopTraitData Get(TroopTraitId _id)
        {
            if (dic == null)
            {
                dic = new Dictionary<TroopTraitId, TroopTraitData>();
                for (var i = 0; i < troopList.Length; i++)
                {
                    dic.Add(troopList[i].TraitId ,troopList[i] );
                }
            }
            
            if (dic.TryGetValue(_id, out var result))
            {
                return result;
            }
            else
            {
                Debug.LogError($"[TroopTraitTable.Get()] 데이터를 찾을수 없습니다. : {_id}");
                return null;
            }
        }
    }
}


