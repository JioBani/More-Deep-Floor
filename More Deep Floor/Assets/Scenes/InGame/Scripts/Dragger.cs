using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.EventHandlers;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private bool isDragging;
    private Vector2 startPos;
    public Void_EventHandler OnDragEnd;
    
    private void Update() 
    {
        if(isDragging)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }
    
    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        Vector3 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        startPos = mousePos - transform.position;
        isDragging = true;
    }

    private void OnMouseUp()
    {
        //Debug.Log("OnMouseUp");
        if (isDragging)
        {
            isDragging = false;
            OnDragEnd?.Invoke();
        }
    }
}
