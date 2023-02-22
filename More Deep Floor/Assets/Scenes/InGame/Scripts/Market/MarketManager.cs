using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.MarketSystem
{
    public class MarketManager : MonoBehaviour
    {
        private DefenderManager defenderManager;
        private TileManager tileManager;
        private StageManager stageManager;
        private InGameStateManager inGameStateManager;
        private Camera mainCamera;

        [SerializeField] private GameObject defenderButtonParent;
        [SerializeField] private DefenderTableOriginalData defenderTableOriginalData;

        private MerchandiseInfo merchandiseInfo;

        private DefenderButton[] defenderButtons;

        public int startGold;
        public int gold { get; private set; }
        public int interestLimit { get; private set; }

        //public int level;
        public MarketLevelInfo levelInfo = new MarketLevelInfo();

        public delegate void OnInitEventHandler(int level, int currentExp, int maxExp);
        public delegate void OnInitLevelHandler(int level);
        public delegate void OnLevelUpHandler(int level);
        public delegate void OnGoldChangeEventHandler(int gold, int change);
        public delegate void OnExpUpEventHandler(int currentExp, int maxExp);

        public OnInitEventHandler OnInitEventAction;
        public OnInitLevelHandler onInitLevelAction;
        public OnGoldChangeEventHandler OnGoldChangeAction;
        public OnLevelUpHandler OnLevelUpAction;
        public OnExpUpEventHandler OnExpUpAction;

        void Awake()
        {
            mainCamera = Camera.main;

            inGameStateManager = ReferenceManager.instance.inGameStateManager;
            tileManager = ReferenceManager.instance.tileManager;
            defenderManager = ReferenceManager.instance.defenderManager;
            stageManager = ReferenceManager.instance.stageManager;
            
            defenderButtons = new DefenderButton[defenderButtonParent.transform.childCount];
            merchandiseInfo = new MerchandiseInfo(defenderTableOriginalData);

            for (int i = 0; i < defenderButtonParent.transform.childCount; i++)
            {
                defenderButtons[i] = defenderButtonParent.transform.GetChild(i).GetComponent<DefenderButton>();
            }

            inGameStateManager.OnDataLoadAction += OnDataLoad;
            inGameStateManager.OnRoundStartAction += OnRoundStart;
            inGameStateManager.OnRoundEndAction += OnRoundEnd;

           
        }

        void Start()
        {
            levelInfo.Init();

            foreach (var defenderButton in defenderButtons)
            {
                //int cost = merchandiseInfo.SelectCost(level);
                defenderButton.SetDefender(new DefenderData(merchandiseInfo.GetDefender(levelInfo.level)));
            }
            
            OnInitEventAction?.Invoke(levelInfo.level,levelInfo.currentExp,levelInfo.maxExp);
            onInitLevelAction?.Invoke(levelInfo.level);
        }
        
        
        //#. 이벤트 함수

        void OnDataLoad()
        {
            GoldChange(startGold, "시작골드");
        }

        void OnRoundStart(int round)
        {
            AddInterest(round);
        }

        void OnRoundEnd(int round)
        {
            
        }
        
        void AddInterest(int round)
        {
            int income = 0;
            
            if (round > 1)
            {
                if (gold > interestLimit * 10)
                {
                    income += interestLimit;
                }
                else if(gold > 0)
                {
                    income += gold / 10;
                }
            }

            GoldChange(income , "이자");
        }

        public void SetInterestLimit(int value)
        {
            interestLimit = value;
        }

        //#. 리롤
        public void DoReRoll()
        {
            foreach (var defenderButton in defenderButtons)
            {
                int cost = merchandiseInfo.SelectCost(levelInfo.level);
                defenderButton.SetDefender(new DefenderData(merchandiseInfo.GetDefender(levelInfo.level)));
            }
            
        }

        public void TryReRoll()
        {
            if (TryBuy(2))
            {
                DoReRoll();   
            }
        }
        
        
        //#. 골드
        public void GoldChange(int change , string name)
        {
            gold += change;
            Debug.Log($"[MarketManager.GoldChange()] 골드 변화 {name} 으로 부터 {change}");
            OnGoldChangeAction?.Invoke(gold , change);
        }

        //#. 구매
        public bool TryBuy(int price)
        {
            if (price > gold)
            {
                Debug.Log($"[MarketManager.TryBuy()] 구매 실패 : 잔액 부족({gold})");
                return false;
            }
            else
            {
                GoldChange(-price , "수호자 구매");
                Debug.Log($"[MarketManager.TryBuy()] 구매 성공");
                return true;
            }
        }

        public bool TryBuyDefender(DefenderData defenderData)
        {
            Tile tile = tileManager.GetEmptyTile();
            
            if (tile == null)
            {
                Debug.Log("[MarketButtonManager.OnClickBuyDefender()] 대기석이 모두 차있습니다. ");
                return false;
            }
            
            if (TryBuy(defenderData.cost))
            {
                defenderManager.SpawnDefenderById(defenderData.id,tile);
                return true;
            }

            return false;
        }
        
        //#. 레벨

        void ExpUp(int value)
        {
            if (levelInfo.ExpUp(value))
            {
                OnLevelUp();
            }
            OnExpUpAction?.Invoke(levelInfo.currentExp,levelInfo.maxExp);
        }

        void OnLevelUp()
        {
            OnLevelUpAction?.Invoke(levelInfo.level);
        }

        public void TryBuyExp()
        {
            if (TryBuy(4))
            {
                ExpUp(4);
            }
        }

        public void SellDefender(Defender defender)
        {
            GoldChange(defender.status.defenderData.cost * defender.status.level , "수호자 판매");
            Debug.Log($"[MarketManager.SellDefender()] 수호자 판매 : {defender.status.defenderData.cost * defender.status.level}");
        }
        
        
        //#. 기타
        
        // #. 수호자 최대치 확인
        /*public bool CheckPlaceLimit()
        {
            if (defenderManager.battleDefenders.Count < levelInfo.level) return true;
            else return false;
        }*/

        // #. 수호자 확률 가져오기 
        public CostProbability GetProbabilities()
        {
            return merchandiseInfo.GetProbability(levelInfo.level);
        }
    }
}


