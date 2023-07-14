using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.EntityStates.Traits;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.EntityStates.Trait.Crops
{
    public class CorpsState_JadeKnights : TraitState
    {
        private List<float> basicSp; //#. 기본_마법력
        private List<float> extraSp; //#. 추가_마법력

        public CorpsState_JadeKnights(EntityStateData _entityStateData, InGame.Entitys.Entity _self) 
            : base(_entityStateData, _self)
        {
            SetData(_entityStateData.properties);
        }
        
        public void SetData(Dictionary<string , List<float>> data)
        {
            basicSp = data["기본_마법력"].ToList();
            extraSp = data["추가_마법력"].ToList();
        }

        public override void OnOnAction(InGame.Entitys.Entity _self)
        {
            //TODO buff = _self.status.SpellPower.AddBuff(basicSp[level])
        }

        public override void OnUseSkillAction(List<InGame.Entitys.Entity> targets)
        {
            //TODO 
        }

        public override void OnSynergyLevelChange(int level)
        {
            base.OnSynergyLevelChange(level);
            //TODO _self.status.SpellPower.ModifyBuff(buff , basicSp[level])
        }
    }

    public class TraitEffect_JadeKnights_Buff : EntityState
    {
        public TraitEffect_JadeKnights_Buff(EntityStateData _entityStateData, InGame.Entitys.Entity _self) : base(_entityStateData, _self)
        {
            
        }
    }
}


