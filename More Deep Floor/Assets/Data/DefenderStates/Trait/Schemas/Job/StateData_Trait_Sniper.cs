using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits
{
    [CreateAssetMenu(
        fileName = "Sniper",
        menuName = "Scriptable Object/Defender State Data/Trait/Job/Sniper",
        //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
        order = int.MaxValue)]

    public class StateData_Trait_Sniper : DefenderStateData
    {
        private float[] percents;
        
        public override DefenderState GetState(Defender defender)
        {
            return new Trait_Sniper(this, defender);
        }
    }

    public class Trait_Sniper : DefenderState
    {
        private float[] percents;
        
        public Trait_Sniper(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
        {

        }
        
        public override void OnTargetHitAction(Defender caster, Monster target, int damage)
        {
            if (!target.gameObject.activeSelf) return;
            int level = traitController.job.synergyLevel;
            float maxDamagePercent = percents[level];
            float distance = Vector2.Distance(caster.transform.position, target.transform.position);
            float value = distance / 5f;
            float addDamage = maxDamagePercent * value * 0.01f * damage;
            target.SetHitFinal(addDamage * damage, caster);
            Debug.Log($"[Trait_Sniper] " +
                      $"distance = {distance} , " +
                      $"value = {value} , " +
                      $"addDamage = {addDamage}");
        }
    }
}


