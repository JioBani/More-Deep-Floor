using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.EntityStates
{
    [CreateAssetMenu(
        fileName = "Entity State Database",
        menuName = "Scriptable Object/Entity State/Entity State Database",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class EntityStateDataBase : ScriptableObject
    {
        [SerializeField] private List<EntityStateData> entityStates;

        public Dictionary<EntityStateId, EntityStateData> GetDic()
        {
            return entityStates.ToDictionary(keySelector: state => state.StateId, elementSelector: state => state);
        }
    }
}


