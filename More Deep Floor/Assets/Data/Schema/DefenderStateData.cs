using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LNK.MoreDeepFloor.Common.ReadOnlyInspector;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(
        fileName = "DefenderState Data", 
        menuName = "Scriptable Object/DefenderState Data", 
        order = int.MaxValue)]
    
    public abstract class DefenderStateData: ScriptableObject
    {
        [SerializeField] private DefenderStateId id;
        public DefenderStateId Id => id;
        
        [SerializeField] private string stateName;
        public string StateName => stateName;
        
        [SerializeField] private DefenderStateType actionType;
        public DefenderStateType ActionType => actionType;
        
        [TextArea][SerializeField] protected string description;
        public string Description => description;

        [SerializeField] private Sprite image;
        public Sprite Image => image;

        [SerializeField] private bool isInvisible;
        public bool IsInvisible => isInvisible;

        [SerializeField] private bool isNeedStack;
        public bool IsNeedStack => isNeedStack;

        public abstract DefenderState GetState(Defender defender);
    }
    
    public abstract class TraitStateData: DefenderStateData
    {
        public TraitData traitData;
    }

}



