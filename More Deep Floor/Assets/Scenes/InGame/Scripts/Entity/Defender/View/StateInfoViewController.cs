using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.EntityStates;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.Defenders.States
{
    public class StateInfoViewController : MonoBehaviour
    {
        private StateInfoView[] views;
        private List<EntityStateData> states; 
        private int imageNumber;
        [SerializeField] private StateController stateController;
        [SerializeField] private Defender defender;
        
        private void Awake()
        {
            defender.OnSpawnAction += OnSpawn;
            
            views = new StateInfoView[transform.childCount];
            
            for (int i = 0; i < transform.childCount; i++)
            {
                views[i] = transform.GetChild(i).GetComponent<StateInfoView>();
            }
            
            stateController.OnStateChangeAction += OnStateChange;
        }

        private void OnStateChange(List<EntityState> entityStates)
        {
            foreach (var stateInfoView in views)
            {
                stateInfoView.ResetInfo();
            }
            
            for (var i = 0; i < entityStates.Count; i++)
            {
                views[i].Set(entityStates[i]);
            }

            /*foreach (var entityState in entityStates)
            {
                //AddStateImage(entityState.entityStateData);
                
            }*/
        }

        protected virtual void OnSpawn(Entity self)
        {
            imageNumber = 0;
            states = new List<EntityStateData>();

            for (int j = 0; j < views.Length; j++)
            {
                views[j].ResetInfo();
            }
        }
        
        public void AddStateImage(EntityStateData stateData)
        {
            
            //if(stateData.IsInvisible) return;
            
            if(imageNumber == views.Length - 1) return;

            for (var i = 0; i < states.Count; i++)
            {
                if (states[i].StateId == stateData.StateId)
                {
                    return;
                }
            }
            
            states.Add(stateData);
            views[imageNumber].SetImage(stateData.Icon);
            imageNumber++;
        }

        /*public void RefreshStack(DefenderState state)
        {
            //if(!state.stateData.IsNeedStack) return;
            
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].StateId == state.entityStateData.StateId)
                {
                    views[i].UpdateStack(state.stack);
                    return;
                }
            }
        }*/
        
        public void RemoveStateImage(EntityState state)
        {
            EntityStateData stateData = state.entityStateData;
            
            for (var i = 0; i < states.Count; i++)
            {
                if (states[i].StateId == stateData.StateId)
                {
                    states.RemoveAt(i);
                    imageNumber--;
                    
                    for (var j = 0; j < states.Count; j++)
                    {
                        views[j].SetImage(states[j].Icon);
                    }

                    for (int j = states.Count; j < views.Length; j++)
                    {
                        views[j].ResetInfo();
                    }
                    break;
                }
            }
        }
        
        
    }
}


