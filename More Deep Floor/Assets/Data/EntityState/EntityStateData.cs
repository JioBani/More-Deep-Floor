using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.EntityStates
{
    [CreateAssetMenu(
        fileName = "Entity State Data",
        menuName = "Scriptable Object/Entity State/Entity State Data",
        order = int.MaxValue)]
    
    public class EntityStateData : ScriptableObject
    {
        public Dictionary<string, List<float>> properties { get; private set; }
        
        [SerializeField] private EntityStateId stateId;
        public EntityStateId StateId => stateId;
        
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField] private List<ActionType> actionTypes;
        public List<ActionType> ActionTypes => actionTypes;

        public void SetProperties(Dictionary<string, List<float>> _properties)
        {
            properties = _properties;
        }
    }
}


