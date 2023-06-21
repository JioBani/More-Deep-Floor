using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas //.
{
    [CreateAssetMenu(
        fileName = "None",
        menuName = "Scriptable Object/Defender State Data/None",
        order = int.MaxValue)]

    public class StateData_None : DefenderStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new None(this, defender);
        }
    }

    public class None : DefenderState
    {
        public None(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {

        }
    }
}