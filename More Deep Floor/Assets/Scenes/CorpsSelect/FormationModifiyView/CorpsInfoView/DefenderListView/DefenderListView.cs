using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class DefenderListView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup layoutGroup;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        [SerializeField] private float padding;
        private DefenderButton[] defenderButtons;

        private void Awake()
        {
            defenderButtons = new DefenderButton[transform.childCount];
            
            ReferenceManager.instance.eventManager.AddOnClickCorpsListTileAction(SetDefenders);

            transform.EachChildIndex((child, index) =>
            {
                defenderButtons[index] = child.GetComponent<DefenderButton>();
            });

            StartCoroutine(SetCellSizeRoutine());
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
        
        IEnumerator SetCellSizeRoutine()
        {
            yield return waitForEndOfFrame;
            SetCellSize();
        }

        public void SetCellSize()
        {
            float width = (transform as RectTransform).rect.width;
            float cellSize = (width - 6 * padding) / 5;
            
            layoutGroup.cellSize = new Vector2(cellSize, cellSize);
        }

        public void SetDefenders(CorpsData corpsData)
        {
            /*for (var i = 0; i < defenderButtons.Length; i++)
            {
                defenderButtons[i].SetData(corpsData.Members[i]);
            }*/

            int i = 0;
            
            for (; i < corpsData.Members.Count; i++)
            {
                defenderButtons[i].SetData(corpsData.Members[i]);
            }
            
            for (; i < defenderButtons.Length; i++)
            {
                defenderButtons[i].SetBlank();
            }
        }
    }
}


