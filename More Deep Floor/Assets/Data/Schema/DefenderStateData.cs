using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LNK.MoreDeepFloor.Common.ReadOnlyInspector;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(
        fileName = "DefenderState Data", 
        menuName = "Scriptable Object/DefenderState Data", 
        order = int.MaxValue)]

    public class DefenderStateData : ParameterSo
    {
        [SerializeField] private DefenderStateId id;
        public DefenderStateId Id => id;

        public void SetStateName(string _name)
        {
            dataName = _name;
        }

        public void SetTraitId(DefenderStateId _id)
        {
            id = _id;
        }
    }

}

