using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using UnityEngine;

using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Moody",
        menuName = "Scriptable Object/Trait Data/Personality/Moody",
        order = int.MaxValue)]


    //#. 실제 인게임에서 사용할 특성 정보 클래스
    public class Personality_Moody : TraitData
    {
        //#. 특성 파라미터 작성

        public float[] attackSpeed { get; private set; }

        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimePersonality_Moody(this);
        }
    }


    //#. 실제 인게임에서 사용할 특성 정보 클래스
    public class RuntimePersonality_Moody : RuntimeTraitData
    {
        //#. 특성 파라미터 작성
        
        public float[] attackSpeed { get; private set; }

        public RuntimePersonality_Moody(Personality_Moody _data) : base(_data)
        {
            attackSpeed = _data.attackSpeed.Clone() as float[];
        }

        public override TraitState GetState(Defender defender)
        {
           return new PersonalityState_Moody(traitData.TraitStateData, this, defender);
        }
    }
}