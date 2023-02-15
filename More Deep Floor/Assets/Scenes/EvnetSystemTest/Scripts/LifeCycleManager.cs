using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.EventHandlers;
using UnityEngine;

public class LifeCycleManager : MonoBehaviour
{
    public Void_EventHandler Init;
    public Void_EventHandler LateInit;
    
    private void Awake()
    {
        Init?.Invoke();
        LateInit.Invoke();
    }
}
