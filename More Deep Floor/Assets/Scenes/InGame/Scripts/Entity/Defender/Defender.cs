using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Palettes;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.SkillSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class Defender : MonoBehaviour
    {
        private BulletManager bulletManager;
        private DefenderManager defenderManager;
        private MarketManager marketManager;
        private UiManager uiManager;
        
        private Dragger dragger;
        private Poolable poolable;
        private TraitController traitController;
        public DefenderStateController stateController;
            
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Placer placer;
        [SerializeField] private TextMeshPro levelUpText;
        [SerializeField] private SpriteRenderer frameSpriteRenderer;
        [SerializeField] private EntityCollide entityCollide;
        [SerializeField] private SpriteRenderer manaInnerBarRenderer;
        [SerializeField] private SkillController skillController;

        public delegate void OnSpawnEventHandler();
        public delegate void OnBeforeOriginalAttackEventHandler(Monster target,DefenderStateId from);
        public delegate void OnBeforeAttackEventHandler(Monster target,DefenderStateId from);
        public delegate void OnUseSkillEventHandler(Monster target, bool isFinal = false);
        public delegate void OnAttackEventHandler(Monster target);
        public delegate void OnKillEventHandler(Monster target);
        public delegate void OnTargetHitEventHandler(Monster target , int damage);

        public OnSpawnEventHandler OnSpawnAction;
        public OnBeforeOriginalAttackEventHandler OnBeforeOriginalAttackAction;
        public OnBeforeAttackEventHandler OnBeforeAttackAction;
        public OnUseSkillEventHandler OnUseSkillAction;
        //private OnAttackEventHandler OnAttackAction;
        public OnKillEventHandler OnKillAction;
        public OnTargetHitEventHandler OnTargetHitAciton;

        private Monster target;

        public DefenderStatus status;
        private SkillData skillData;

        public double attackTimer = 0;
        private bool isOn = true;
        private bool isInSellZone = false;

        private void Awake()
        {
            bulletManager = ReferenceManager.instance.bulletManager;
            defenderManager = ReferenceManager.instance.defenderManager;
            marketManager = ReferenceManager.instance.marketManager;
            uiManager = ReferenceManager.instance.UiManager;
            
            dragger = GetComponent<Dragger>();
            poolable = GetComponent<Poolable>();
            placer = GetComponent<Placer>();
            traitController = GetComponent<TraitController>();
            stateController = GetComponent<DefenderStateController>();
            
            dragger.OnDragEndAction += OnDragEnd;
            dragger.OnSimpleClickAction += OnSimpleClick;

            placer.OnEnterBattleFieldAction = EnterBattleField;
            placer.OnEnterWaitingRoomAciton = EnterWaitingRoom;
            placer.OnExitBattleFieldAction = ExitBattleField;
            placer.OnExitWatingRoomAction = ExitWaitingRoom;

            entityCollide.OnTriggerEnter2DAciton += OnEntityTriggerEnter;
            entityCollide.OnTriggerExit2DAciton += OnEntityTriggerExit;
        }
        

        void Update()
        {
            if (isOn)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > status.attackSpeedTimer && !ReferenceEquals(target,null))
                {
                    OriginalAttack();
                }
            }
        }

        private void OnEntityTriggerEnter(Collider2D col)
        {
            if (col.CompareTag("SellZone"))
            {
                isInSellZone = true;
            }
        }
        
        private void OnEntityTriggerExit(Collider2D col)
        {
            if (col.CompareTag("SellZone"))
            {
                isInSellZone = false;
            }
        }

        public void Init(DefenderData _defenderData)
        {
            status = new DefenderStatus(_defenderData);
            spriteRenderer.sprite = _defenderData.sprite;
            frameSpriteRenderer.color = Palette.defenderCostColors[_defenderData.cost];
            levelUpText.text = "Lv 1";
            OnManaChanged(status.currentMaxMana , status.maxMana);
            status.OnManaChangedAction += OnManaChanged;
            skillData = _defenderData.skillData;
            skillController.SetSkillData(this , skillData , stateController);
            traitController.SetTrait(this);
            stateController.Init();
        }

        public void OnSpawn()
        {
            OnSpawnAction?.Invoke();
            
            defenderManager.CheckMerge(this);
        }

        void OnDragEnd()
        {
            if (isInSellZone)
            {
                marketManager.SellDefender(this);
                SetOff();
                Debug.Log("판매");
            }
            else
            {
                placer.TryMove(SearchTile());
            }
            
        }

        public void OnKillTarget(Monster target)
        {
            OnKillAction?.Invoke(target);
        }

        public void OnTargetHit(Monster target, int damage)
        {
            OnTargetHitAciton?.Invoke(target,damage);
        }

        Tile SearchTile()
        {
            int layerMask = 1 << LayerMask.NameToLayer("Tile");
            Collider2D collider2D = Physics2D.OverlapPoint(transform.position,layerMask);

            if (!ReferenceEquals(collider2D, null))
            {
                return collider2D.gameObject.GetComponent<Tile>();
            }
            else
            {
                return null;
            }
        }

        public void SpawnAtWaitingRoom(Tile tile)
        {
            placer.TryMove(tile);
        }

        public void EnterWaitingRoom()
        {
            defenderManager.OnDefenderEnterWaitingRoom(this);
            isOn = false;
        }

        public void ExitWaitingRoom()
        {
            status.SetManaGain(false);
            defenderManager.OnDefenderExitWaitingRoom(this);
        }

        public void EnterBattleField()
        {
            status.SetManaGain(true);
            defenderManager.OnDefenderEnterBattleField(this);
            isOn = true;
        }
        
        public void ExitBattleField()
        {
            defenderManager.OnDefenderExitBattleField(this);
        }       

        public void TrySetTarget(Monster _target)
        {
            if (ReferenceEquals(target , null))
            {
                target = _target;
            }
        }

        public void TrySetUnTarget(Monster _target)
        {
            if (ReferenceEquals(target , _target))
            {
                target = null;
            }
        }

        public List<Monster> TrySearchTargets(float range)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(
                transform.position, 
                range, 
                LayerMask.GetMask("MonsterCollideZone"));

            List<Monster> targets = new List<Monster>();
            
            for (var i = 0; i < cols.Length; i++)
            {
                Monster monster = cols[i].transform.parent.GetComponent<Monster>();
                if (!ReferenceEquals(monster, null))
                {
                    targets.Add(monster);
                }
            }

            return targets;
        }
        
        public List<Monster> TrySearchTargetsExpectTarget(float range)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(
                transform.position, 
                range, 
                LayerMask.GetMask("MonsterCollideZone"));

            List<Monster> targets = new List<Monster>();
            
            for (var i = 0; i < cols.Length; i++)
            {
                Monster monster = cols[i].transform.parent.GetComponent<Monster>();
                if (!ReferenceEquals(monster, null))
                {
                    targets.Add(monster);
                }
            }

            if (targets.Contains(target)) targets.Remove(target);

            return targets;
        }

        void LevelUp()
        {
            if (status.level < 3)
            {
                status.LevelUp();
                levelUpText.text = "Lv " + status.level;
            }
        }

        public void Merge()
        {
            LevelUp();
        }

        public void BeMerged(Defender _defender)
        {
            SetOff();
        }

        public void SetOff()
        {
            defenderManager.OnDefenderOff(this);
            placer.SetOff();
            poolable.SetOff();
        }

        void OriginalAttack()
        {
            OnBeforeOriginalAttackAction?.Invoke(target,DefenderStateId.None);
            OnBeforeAttackAction?.Invoke(target,DefenderStateId.None);
            bulletManager.Fire(gameObject, target.gameObject);
            attackTimer = 0;
            if(status.ManaUp(20))
            {
                UseSkill();
            }
        }

        public void SetExtraAttack(Monster _target , DefenderStateId from)
        {
            OnBeforeAttackAction?.Invoke(target, from);
            bulletManager.Fire(gameObject, _target.gameObject);
        }

        public void UseSkill()
        {
            skillController.UseSkill(new List<Monster> { target });
            OnUseSkillAction?.Invoke(target);
        }

        public void UseSkillFinal()
        {
            skillController.UseSkill(new List<Monster> { target });
            OnUseSkillAction?.Invoke(target , true);
        }

        void OnManaChanged(int maxMana, int currentMana)
        {
            manaInnerBarRenderer.size = new Vector2(status.currentMana / (float)status.currentMaxMana ,0.25f);
        }

        void OnSimpleClick()
        {
            uiManager.OnClickDefender(this);
        }
    }
}


