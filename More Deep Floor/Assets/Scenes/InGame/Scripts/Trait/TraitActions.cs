using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    public class TraitActions : MonoBehaviour
    { 
        private TimerManager timerManager = null;
        
        public delegate void TraitActionHandler(Defender caster , Monster target = null);
        public Dictionary<TraitId, TraitActionHandler> actions;
        
        public TraitActions()
        {
            /*actions = new Dictionary<TraitId, TraitActionHandler>()
            {
                { TraitId.A, GladiatorFunction },
            };*/
        }

        /*void GladiatorFunction(Defender caster,Monster target = null)
        {
            int id = caster.status.AddAttackSpeedBuff(caster.status.attackSpeed);
            timerManager.LateAction(3f , () =>
            {
                caster.status.RemoveAttackSpeedBuff(id);
            });
        }*/
    }
}

