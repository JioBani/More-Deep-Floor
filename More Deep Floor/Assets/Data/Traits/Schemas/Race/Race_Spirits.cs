using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.DefenderTraits.Schemas
{
    [CreateAssetMenu(
        fileName = "Spirits",
        menuName = "Scriptable Object/Trait Data/Race/Spirits",
        order = int.MaxValue)]


    //#. 실제 인게임에서 사용할 특성 정보 클래스
    public class Race_SpiritsData : TraitData
    {
        //#. 특성 파라미터 작성

        [Tooltip("쉴드가 생성되는 체력 퍼센트")]
        [SerializeField] private float triggerHealthPercent;

        public float TriggerHealthPercent => triggerHealthPercent;
        
        [Tooltip("쉴드 체력 계수")]
        [SerializeField] private float[] shieldHealthPercent;
        public float[] ShieldHealthPercent => shieldHealthPercent;


        public override RuntimeTraitData GetRuntimeData()
        {
            return new RuntimeRace_Spirits(this);
        }
    }


    //#. 실제 인게임에서 사용할 특성 정보 클래스
    public class RuntimeRace_Spirits : RuntimeTraitData
    {
        //#. 특성 파라미터 작성
        private float triggerHealthPercent;
        public float TriggerHealthPercent => triggerHealthPercent;
        
        private float[] shieldHealthPercent;
        public float[] ShieldHealthPercent => shieldHealthPercent;


        public RuntimeRace_Spirits(Race_SpiritsData _data) : base(_data)
        {
            triggerHealthPercent = _data.TriggerHealthPercent;
            shieldHealthPercent = _data.ShieldHealthPercent.Clone() as float[];
        }

        public override TraitState GetState(Defender defender)
        {
            return new TraitState_Spirits(traitData.TraitStateData, this, defender);
        }
    }
}