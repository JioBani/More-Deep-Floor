using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    public class TraitInfo
    { 
        public TraitData traitData;
        public int synergyLevel  { get; private set; }

        public int SetLevel(int level)
        {
            if (level == synergyLevel)
            {
                return 0;
            }
            
            if (synergyLevel <= -1 && level >= -1)
            {
                synergyLevel = level;
                return 1;
            }

            if (synergyLevel > -1 && level <= -1)
            {
                synergyLevel = level;
                return -1;
            }
            
            synergyLevel = level;
            return 0;
        }

        public TraitInfo(TraitData _traitData)
        {
            traitData = _traitData;
            synergyLevel = -1;
        }
    }
    
    public class TraitController : MonoBehaviour
    {
        private Defender defender;
        private DefenderStateController defenderStateController;
        public TraitInfo job;
        public TraitInfo character;
        
        //public delegate void OnTraitLevelChangeEventHandler(TraitType traitType , l);

        [SerializeField] private TextMeshPro jobText;
        [SerializeField] private TextMeshPro characterText;

        void Awake()
        {
            defenderStateController = GetComponent<DefenderStateController>();
            defender = GetComponent<Defender>();
        }

        public void SetTrait(Defender defender)
        {
            job = new TraitInfo(defender.status.defenderData.job);
            character = new TraitInfo(defender.status.defenderData.character);
            defender.OnKillAction += OnKill;
        }


        public void OnBattleFieldExit()
        {
            SetSynergyLevel(TraitType.Job , -1);
            SetSynergyLevel(TraitType.Character , -1);
        }

        public void SetSynergyLevel(TraitType type , int level)
        {
            TraitInfo target;
            
            if (type == TraitType.Job)
                target = job;
            else
                target = character;
            
            int result;
            if (type == TraitType.Job)
            {
                result = target.SetLevel(level);
            }
            else
            {
                result = target.SetLevel(level);
            }

            if (result == -1)
            {
                defenderStateController.RemoveState(target.traitData.TraitStateData.Id);
            }
            else if (result == 1)
            {
                //defenderStateController.AddState(target.traitData.TraitStateData.Id);
                defenderStateController.AddState(target.traitData.GetTraitState(defender));
            }
            
            RefreshText();
        }

        void OnKill(Monster target)
        {
            
        }

        void RefreshText()
        {
            jobText.text = job.traitData.Id + " : " + job.synergyLevel;
            characterText.text = character.traitData.Id + " : " + character.synergyLevel;
        }

        public TraitInfo GetTraitInfo(TraitType type)
        {
            if (type == TraitType.Job) return job;
            else return character;
        }
    }
}


