using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.EntityStates.Trait.Crops;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.EntityStates.Traits
{
    public class TraitState : EntityState
    {
        private int synergyLevel = 0;
        
        public TraitState(EntityStateData _entityStateData, InGame.Entitys.Entity _self) : base(_entityStateData, _self)
        {
            
        }

        public virtual void OnSynergyLevelChange(int level)
        {
            synergyLevel = level;
        }
    }
}


