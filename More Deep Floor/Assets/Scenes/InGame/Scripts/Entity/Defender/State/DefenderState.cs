using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    public class DefenderState
    {
        public DefenderStateId id;
        public int stack;
        public DefenderStateData stateData;
        public DefenderStateActionInfoBase actionInfo;
        private DefenderStateController stateController;

        public DefenderState(DefenderStateId _id,DefenderStateData _stateData, DefenderStateActionInfoBase _actionInfo)
        {
            stateData = _stateData;
            id = _id;
            stack = 1;
            actionInfo = _actionInfo;
        }
        
        public bool RemoveStack()
        {
            stack--;
            
            if (stack <= 0)
            {
                return false;
            }
            return true;
        }

        public void AddStack()
        {
            stack++;
        }

        public void Set(Defender defender, DefenderStateController _stateController)
        {
            stateController = _stateController;
            actionInfo.Set(stateData,defender,_stateController,defender.GetComponent<TraitController>());
        }

        public void RemoveState()
        {
            stateController.RemoveState(id);
        }
    }
}

