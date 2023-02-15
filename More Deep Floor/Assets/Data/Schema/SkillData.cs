using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using LNK.MoreDeepFloor.Common.ReadOnlyInspector;
using LNK.MoreDeepFloor.Data.Defender;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
    public class SkillData : ParameterSo
    {
        [SerializeField] private DefenderId id;
        public DefenderId Id => id;

        [SerializeField] private string name;
        public string Name => name;
    }
}


