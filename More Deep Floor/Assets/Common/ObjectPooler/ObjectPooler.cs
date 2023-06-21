using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

public class ObjectPooler : MonoBehaviour
{
   public GameObject objectPool;
   private List<Poolable> poolObjects = new List<Poolable>();
   public GameObject objectOrigin;

   private void Awake()
   {
      for (int i = 0; i < objectPool.transform.childCount; i++)
      {
         Poolable poolable = objectPool.transform.GetChild(i).GetComponent<Poolable>();
         poolable.objectPool = objectPool;
         poolObjects.Add(poolable);
      }
   }

   /// <summary>
   /// 오브젝트 풀링
   /// </summary>
   /// <param name="parent">풀링 오브젝트를 자식으로 할 부모 오브젝트</param>
   public GameObject Pool(GameObject parent)
   {
      Poolable poolable = Pooling();
      poolable.transform.SetParent(parent.transform);
      poolable.OnPool?.Invoke();
      return poolable.gameObject;
   }
   

   /// <summary>
   /// <para>오브젝트 풀링</para> 
   /// 풀링 오브젝트는 ObjectPool을 부모로 가짐
   /// </summary>
   public GameObject Pool()
   {
      Poolable poolable = Pooling();
      poolable.OnPool?.Invoke();
      return poolable.gameObject;
   }


   Poolable Pooling()
   {
      for (int i = 0; i < poolObjects.Count; i++)
      {
         if (!poolObjects[i].gameObject.activeSelf)
         {
            Poolable poolObject = poolObjects[i];
            poolObject.gameObject.SetActive(true);
            return poolObject;
         }
      }
      
      Logger.Log($"[ObjectPooler] 풀링할 오브젝트가 없습니다. : {gameObject.name}");
      
      Poolable newPoolable = Instantiate(objectOrigin , objectPool.transform).GetComponent<Poolable>();
      newPoolable.objectPool = objectPool;
      poolObjects.Add(newPoolable);
      return newPoolable;
   }
}
