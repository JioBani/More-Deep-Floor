using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.EventHandlers;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.InputControls
{
    public class InputListener : MonoBehaviour
    {
        public Void_EventHandler OnTouchEvent;
        public Void_EventHandler OnMouseDownEvent;
        public Void_EventHandler OnTouchOrMouseDownEvent;
        public Void_EventHandler OnMouseUpEvent;
        
        
    }
}

