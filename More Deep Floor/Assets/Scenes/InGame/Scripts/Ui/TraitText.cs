using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;
using UnityEngine.UI;

public class TraitText : MonoBehaviour
{
    private TraitManager traitManager;
    private Text traitText;

    private void Awake()
    {
        traitText = GetComponent<Text>();
        traitManager = ReferenceManager.instance.traitManager;
        traitManager.OnTraitChangeAciton += OnTraitRefresh;
    }

    void OnTraitRefresh(Dictionary<TraitId, BattleFieldTraitInfo> traits)
    {
        List<BattleFieldTraitInfo> list = new List<BattleFieldTraitInfo>();
        
        traitText.text = "";
        
        foreach (var trait in traits)
        {
            list.Add(trait.Value);
        }

        list = list.OrderByDescending(element => element.synergyLevel).ToList();
        
        for (var i = 0; i < list.Count; i++)
        {
            var trait = list[i];
            
            if(trait.traitData.SynergyTrigger.Length < 1)
                continue;

            if (trait.nums != 0)
            {
                string str = "";
                
                if (trait.synergyLevel == -1)
                {
                    str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel + 1]} => {trait.synergyLevel}";
                }
                else if ((trait.synergyLevel + 1 < trait.traitData.SynergyTrigger.Length))
                {
                    if (trait.nums == trait.traitData.SynergyTrigger[trait.synergyLevel])
                    {
                        str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel]} => {trait.synergyLevel}";
                    }
                    else
                    {
                        str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel + 1]} => {trait.synergyLevel}";
                    }
                }
                else
                {
                    str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel]}=> {trait.synergyLevel}";
                }
                
                traitText.text += str;
                traitText.text += "\n";
            }
        }
        
        /*foreach (var trait in list)
        {
            if(trait.traitData.SynergyTrigger.Length < 1)
                continue;
            
            string str = "";
            if (trait.nums != 0)
            {
                if (trait.synergyLevel == -1)
                {
                    str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel + 1]} => {trait.synergyLevel}";
                }
                else if ((trait.synergyLevel + 1 < trait.traitData.SynergyTrigger.Length))
                {
                    if (trait.nums == trait.traitData.SynergyTrigger[trait.synergyLevel])
                    {
                        str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel]} => {trait.synergyLevel}";
                    }
                    else
                    {
                        str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel + 1]} => {trait.synergyLevel}";
                    }
                }
                else
                {
                    str += $"{trait.traitData.Id} : {trait.nums} / {trait.traitData.SynergyTrigger[trait.synergyLevel]}=> {trait.synergyLevel}";
                }
            }

            str += " : ";
            
            foreach (var traitDefenderId in trait.defenderIds)
            {
                str += traitDefenderId + " , ";
            }

            foreach (var traitDefender in trait.defenders)
            {
                str += traitDefender.status.defenderData.spawnId + ",";
            }
            
            traitText.text += str;
            traitText.text += "\n";
        }*/
    }
}
