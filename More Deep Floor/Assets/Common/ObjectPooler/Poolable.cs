using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public GameObject objectPool;

    public delegate void OnPoolAction();

    public OnPoolAction OnPool = null;

    public void SetOff()
    {
        transform.SetParent(objectPool.transform);
        gameObject.SetActive(false);
    }
}
