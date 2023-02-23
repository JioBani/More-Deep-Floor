using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    [Serializable]
    public class BattleFieldTraitInfo
    {
         public TraitData traitData;
         public int nums;
         public List<Defender> defenders;
         public Dictionary<DefenderId,int> defenderIds;
         public int synergyLevel;
 
         public BattleFieldTraitInfo(TraitData _traitData)
         {
             traitData = _traitData;
             nums = 0;
             defenders = new List<Defender>();
             defenderIds = new Dictionary<DefenderId,int>();
             synergyLevel = -1;
         }
 
         public void AddDefender(Defender defender, TraitType type)
         {
             DefenderData data = defender.status.defenderData;
             
             if (type == TraitType.Job && data.job.Id != traitData.Id)
             {
                 return;
             }
 
             if (type == TraitType.Character && data.character.Id != traitData.Id)
             {
                 return;
             }
             
             defenders.Add(defender);
             if (defenderIds.ContainsKey(data.id))
             {
                 defenderIds[data.id]++;
             }
             else
             {
                 defenderIds[data.id] = 1;

             }
 
             nums = defenderIds.Count;
             synergyLevel = -1;
             
             for (var i = traitData.SynergyTrigger.Length - 1; i >= 0; i--)
             {
                 if (traitData.SynergyTrigger[i] <= nums)
                 {
                     synergyLevel = i;
                     break;
                 }
             }
         }
 
 
         public void RemoveDefender(Defender defender, TraitType type)
         {
             DefenderData data = defender.status.defenderData;
             
             if (type == TraitType.Job && data.job.Id != traitData.Id)
             {
                 return;
             }
 
             if (type == TraitType.Character && data.character.Id != traitData.Id)
             {
                 return;
             }
             
             defenders.Remove(defender);
 
             defenderIds[data.id]--;
             
             if (defenderIds[data.id] == 0)
             {
                 defenderIds.Remove(data.id);
             }
 
             nums = defenderIds.Count;
             
             synergyLevel = -1;
             for (var i = traitData.SynergyTrigger.Length - 1; i >= 0; i--)
             {
                 if (traitData.SynergyTrigger[i] <= nums)
                 {
                     synergyLevel = i;
                     break;
                 } 
             }
         }
    }
}