using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.SceneDataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class ResultWindow : MonoBehaviour
    {
        [SerializeField] private GameObject contents;
        [SerializeField] private Text roundText;
        [SerializeField] private Text rewardText;
        private InGameStateManager inGameStateManager;
        private InGameResult gameResult;
        private GameDataSaver gameDataSaver;
        
        private void Awake()
        {
            inGameStateManager = ReferenceManager.instance.inGameStateManager;
            gameDataSaver = new GameDataSaver();
        }

        public void SetResultWindow(InGameResult _gameResult)
        {
            gameResult = _gameResult;
            contents.SetActive(true);
            roundText.text = "라운드 : " + gameResult.round;
            rewardText.text = "보상 : " + gameResult.rewardGold + " G";
        }

        public void OnClickGoMain()
        {
            gameObject.SetActive(false);
            if (gameDataSaver.LoadGoodsData(out var goodsData))
            {
                goodsData.gold += gameResult.rewardGold;
                
                gameDataSaver.SaveGoodsData(goodsData);
            }
            SceneDataManager.instance.InGameToMain(gameResult);
        }
    }
}


