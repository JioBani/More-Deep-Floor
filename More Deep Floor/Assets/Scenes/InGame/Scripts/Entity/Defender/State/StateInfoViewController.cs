using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Defenders.States
{
    public class StateInfoViewController : MonoBehaviour
    {
        private StateInfoView[] views;
        private List<DefenderStateData> states; 
        private int imageNumber;
        [SerializeField] private Defender defender;
        
        private void Awake()
        {
            defender.OnSpawnAction += OnSpawn;
            
            views = new StateInfoView[transform.childCount];
            
            for (int i = 0; i < transform.childCount; i++)
            {
                views[i] = transform.GetChild(i).GetComponent<StateInfoView>();
            }
        }
        
        void OnSpawn()
        {
            imageNumber = 0;
            states = new List<DefenderStateData>();

            for (int j = 0; j < views.Length; j++)
            {
                views[j].ResetInfo();
            }
        }
        
        public void AddStateImage(DefenderStateData stateData)
        {
            
            if(stateData.IsInvisible) return;
            
            if(imageNumber == views.Length - 1) return;

            for (var i = 0; i < states.Count; i++)
            {
                if (states[i].Id == stateData.Id)
                {
                    return;
                }
            }
            
            states.Add(stateData);
            views[imageNumber].SetImage(stateData.Image);
            imageNumber++;
        }

        public void RefreshStack(DefenderState state)
        {
            if(!state.stateData.IsNeedStack) return;
            
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].Id == state.id)
                {
                    views[i].UpdateStack(state.stack);
                    return;
                }
            }
        }
        
        public void RemoveStateImage(DefenderState state)
        {
            DefenderStateData stateData = state.stateData;
            
            for (var i = 0; i < states.Count; i++)
            {
                if (states[i].Id == stateData.Id)
                {
                    states.RemoveAt(i);
                    imageNumber--;
                    
                    for (var j = 0; j < states.Count; j++)
                    {
                        views[j].SetImage(states[j].Image);
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


