using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.EventHandlers;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool isDragging;
    private Vector2 startPos;
    public Void_EventHandler OnDragEndAction;
    public Void_EventHandler OnSimpleClickAction;
    private float clickStartTime;
    private float triggerTime = 0.1f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 mousePos;
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        startPos = mousePos - transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEndAction?.Invoke();
    }

    private void OnMouseDown()
    {
        clickStartTime = Time.time;
    }

    private void OnMouseUp()
    {
        if (Time.time - clickStartTime< triggerTime)
        {
            OnSimpleClickAction?.Invoke();
        }
    }
}
