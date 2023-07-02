using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas //.
{
    [CreateAssetMenu(
        fileName = "Shield",
        menuName = "Scriptable Object/Defender State Data/Test/Shield",
        order = int.MaxValue)]

    public class StateData_TestShield : DefenderStateData
    {
        public override DefenderState GetState(Defender defender)
        {
            return new TestShield(this , defender);
        }
    }

    public class TestShield : DefenderState
    {
        public TestShield(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {

        }

        public override void OnShieldBreakAction(float maxAmount)
        {
            RemoveState();
        }

        public override void OnShieldTimeOutAction(float maxAmount, float leftAmount)
        {
            RemoveState();
        }
    }
}