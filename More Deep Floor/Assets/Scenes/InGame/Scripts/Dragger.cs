using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LNK.MoreDeepFloor.Common.EventHandlers;
using LNK.MoreDeepFloor.Common.Loggers;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour
{
    [SerializeField]private GameObject owner;
    private bool isDragging;
    private bool isClicking;
    private Vector2 startPos;
    public Void_EventHandler OnDragEndAction;
    public Void_EventHandler OnSimpleClickAction;
    private float clickStartTime;
    private Camera mainCamera;

    private Vector2 startPosition;
    private float clickThreshold = 0.1f;
    private float clickTime;

    public LayerMask dragLayer;

    private Stopwatch stopwatch = new Stopwatch();
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject()) // UI 위에서 클릭된 경우 무시
            {
                
                RaycastHit2D hit = Physics2D.Raycast(
                    mainCamera.ScreenToWorldPoint(Input.mousePosition), 
                    Vector2.zero, 
                    Mathf.Infinity, 
                    dragLayer);

                if (!ReferenceEquals(hit.collider , null) && hit.collider.gameObject == gameObject)
                {
                    isClicking = true;
                    startPosition = hit.point;
                    clickTime = Time.time;
                }
            }
        }
        else if (isClicking && Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                OnDragEndAction?.Invoke();
            }
            else
            {
                OnSimpleClickAction?.Invoke();
            }
            stopwatch.Reset();
            isClicking = false;
        }
        
        if (Input.GetMouseButton(0) && isClicking)
        {
            if (Time.time - clickTime > clickThreshold)
            {
                isDragging = true;
                Vector2 currentPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                owner.transform.position = new Vector3(currentPosition.x, currentPosition.y, owner.transform.position.z);
            }
            else
            {
                isDragging = false;
            }
        }
    }
}
