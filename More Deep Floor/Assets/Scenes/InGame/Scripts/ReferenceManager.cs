using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.InGame.DebugSystem;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.SkillSystem;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui;
using LNK.MoreDeepFloor.InGame.Upgrade;
using LNK.MoreDeepFloor.RouteAiScene;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class ReferenceManager : MonoBehaviour
    {
        public static ReferenceManager instance;

        public Canvas uiCanvas;
            
            
        public InGameStateManager inGameStateManager;
        public BulletManager bulletManager;
        public TileManager tileManager;
        public StageManager stageManager;
        public ObjectPoolingManager objectPoolingManager;
        public MonsterManager monsterManager;
        public MarketManager marketManager;
        public DefenderManager defenderManager;
        public UiAssetManager uiAssetManager;
        public SkillManager skillManager;
        public TraitManager traitManager;
        public UiManager uiManager;
        public UpgradeManager upgradeManager;
        public EntityStateGenerator entityStateGenerator;
        public EntityManager entityManager;
        public HexPathFinder hexPathFinder;
        
        public DebugController debugController;

        //public DefenderStateList defenderStateList;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}

