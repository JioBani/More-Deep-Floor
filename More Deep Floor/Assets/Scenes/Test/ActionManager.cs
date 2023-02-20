using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entity;
using UnityEngine;


namespace LNK.MoreDeepFloor.Test
{
    public class ActionManager : MonoBehaviour
    {
        public delegate void Action();

        void Action1()
        {
            Debug.Log(1);
        }
        
        void Action2()
        {
            Debug.Log(2);
        }
        
        void Action3()
        {
            Debug.Log(3);
        }

        private Action action;

        private void Start()
        {
            action += Action1;
            action += Action2;
            action += Action3;
            
            
            Action newAction = action;

            newAction -= Action1;
            
            //action.Invoke();
            //newAction.Invoke();
            Debug.Log(action.GetInvocationList().Length);
            Debug.Log(newAction.GetInvocationList().Length);
            
            action -= Action1;
            
            Debug.Log(action.GetInvocationList().Length);
            Debug.Log(newAction.GetInvocationList().Length);
        }
    }
}

