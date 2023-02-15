using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.InputControls
{
    public class InputMessenger : MonoBehaviour
    {
        private void Update()
        {
            if (Input.touchCount > 0) {
                // 터치 입력 시,
                Touch touch = Input.GetTouch (0);       // only touch 0 is used

                if (touch.phase == TouchPhase.Ended) {
                    Ray ray = Camera.main.ScreenPointToRay (touch.position);
                    RaycastHit[] hits = new RaycastHit[10];
                    Physics.RaycastNonAlloc(ray , hits);
                    InputListener inputListener = null;
                    for (int i = 0; i < hits.Length; i++)
                    {
                        inputListener = hits[i].transform.GetComponent<InputListener>();
                        if (inputListener != null)
                        {
                            inputListener.OnTouchEvent?.Invoke();
                            inputListener.OnTouchOrMouseDownEvent?.Invoke();
                            break;
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp (0)) {
                // 마우스 입력 시,
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit[] hits = new RaycastHit[10];
                Physics.RaycastNonAlloc(ray , hits);
                InputListener inputListener = null;
                for (int i = 0; i < hits.Length; i++)
                {
                    inputListener = hits[i].transform.GetComponent<InputListener>();
                    if (inputListener != null)
                    {
                        inputListener.OnMouseDownEvent?.Invoke();
                        inputListener.OnTouchOrMouseDownEvent?.Invoke();
                        break;
                    }
                }
            }
        }
    }
}


