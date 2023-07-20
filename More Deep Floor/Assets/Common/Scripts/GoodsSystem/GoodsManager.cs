using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.DataSave;
using LNK.MoreDeepFloor.Common.DataSave.DataSchema;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.Common.GoodsSystyem
{
    public class GoodsManager : MonoBehaviour
    {
        [SerializeField] private int gold;
        public GoodsData goodsData;
        public int Gold => gold;
        private GameDataSaver gameDataSaver;

        public delegate void OnGoldChangeEventHandler(int change, int gold);

        public delegate void OnGoodsInitEventHandler(int gold);

        public OnGoldChangeEventHandler OnGoldChangeAction;
        public OnGoodsInitEventHandler OnGoodsInitAction;


        private void Awake()
        {
            goodsData = new GoodsData();
            gameDataSaver = new GameDataSaver();
            gameDataSaver.LoadGoodsData(out goodsData);
        }

        public void Start()
        {
            OnGoodsInitAction?.Invoke(goodsData.gold);
        }

        void GoldChange(int change)
        {
            goodsData.gold += change;
            gameDataSaver.SaveGoodsData(goodsData);
            OnGoldChangeAction?.Invoke(change , goodsData.gold);
        }

        public bool TryBuy(int _gold)
        {
            if (_gold > goodsData.gold)
            {
                Debug.Log($"[GoodsManager.TryBuy()] 잔액 부족 : {gold}");
                return false;
            }
            else
            {
                GoldChange(-_gold);
                Debug.Log($"[GoodsManager.TryBuy()] 구매 성공");
                return true;
            }
        }

        public void OnClickSave()
        {
            gameDataSaver.SaveGoodsData(goodsData);
            Debug.Log("저장완료");
        }

        public void OnClickGoldUp()
        {
            GoldChange(10);
        }

        public void OnClickLoad()
        {
            if (gameDataSaver.LoadGoodsData(out goodsData))
            {
                Debug.Log($"불러오기 완료 : {goodsData.gold}");
            }
            else
            {
                Debug.Log($"불러오기 실패");
            }
        }
    }
}


