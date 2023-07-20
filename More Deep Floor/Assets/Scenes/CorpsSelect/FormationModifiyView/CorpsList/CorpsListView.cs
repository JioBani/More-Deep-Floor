using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class CorpsListView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup contentLayoutGroup;
        [SerializeField] private GameObject content;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        [SerializeField] private int column;
        [SerializeField] private CorpsDataBase corpsDataBase;
        private CorpsListTile[] tiles;
        
        private void Awake()
        {
            tiles = new CorpsListTile[content.transform.childCount];
            
            content.transform.EachChildIndex((child , index) =>
            {
                tiles[index] = child.GetComponent<CorpsListTile>();
            });
            
            StartCoroutine(SetCellSizeRoutine());
            SetCorpsTiles();
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SetCorpsTiles();
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void SetCorpsTiles()
        {
            content.transform.EachChildIndex((child, index) =>
            {
                if (index < corpsDataBase.CorpsDatas.Count)
                {
                    tiles[index].SetCorpData(corpsDataBase.CorpsDatas[index]);
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            });
        }

        IEnumerator SetCellSizeRoutine()
        {
            yield return waitForEndOfFrame;
            SetCellSize();
        }

        public void SetCellSize()
        {
            float size = (content.transform as RectTransform).rect.width / column - 10;
            contentLayoutGroup.cellSize = new Vector2(size, size);
            Debug.Log(size);
        }

        /*private void OnValidate()
        {
            float size = (content.transform as RectTransform).rect.width / 6f - 10;
            contentLayoutGroup.cellSize = new Vector2(size, size);
            Debug.Log(size);
        }*/
    }
}


