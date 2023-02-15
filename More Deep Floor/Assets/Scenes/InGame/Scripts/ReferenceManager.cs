using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.SkillSystem;
using LNK.MoreDeepFloor.InGame.StateActions;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame
{
    public class ReferenceManager : MonoBehaviour
    {
        public static ReferenceManager instance;

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

        public DefenderStateActionList defenderStateActionList;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}

