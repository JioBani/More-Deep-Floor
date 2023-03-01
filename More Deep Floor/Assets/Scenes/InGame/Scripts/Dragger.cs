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
    public Void_EventHandler OnSimpleClick;
    private float triggerTime = 0.2f;
    private float timer = 0;
    
    private void Update() 
    {
        if(isDragging)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        Vector3 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        startPos = mousePos - transform.position;
        timer = 0;
        isDragging = true;
    }

    private void OnMouseUp()
    {
        //Debug.Log("OnMouseUp");
        if (isDragging)
        {
            if (timer > triggerTime)
            {
                isDragging = false;
                timer = 0;
                OnDragEnd?.Invoke();
            }
            else
            {
                isDragging = false;
                timer = 0;
                OnDragEnd?.Invoke();
                OnSimpleClick?.Invoke();
            }
        }
    }
}
