using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Palettes;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.DebugSystem;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.SkillSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui;
using LNK.MoreDeepFloor.InGame.Ui.DefenderDataInfoUi;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class DefenderEventManager
    {
        public delegate void OnSpawnEventHandler();
        public delegate void BeforeCommonAttackEventHandler(Monster target , AttackInfo attackInfo);
        public delegate void AfterCommonAttackEventHandler(Monster target , AttackInfo attackInfo);
        public delegate void OnKillEventHandler(Monster target);
        public delegate void AfterUseSkillEventHandler(Monster target, AttackInfo attackInfo);
        public delegate void OnTargetCommonHitEventHandler(Monster target, AttackInfo attackInfo);


        public OnSpawnEventHandler OnSpawnAction;
        public BeforeCommonAttackEventHandler BeforeCommonAttackAction;
        public AfterCommonAttackEventHandler AfterCommonAttackAction;
        public OnKillEventHandler OnKillAction;
        public AfterUseSkillEventHandler AfterUseSkillAction;
        public OnTargetCommonHitEventHandler OnTargetCommonHitAction;
    }


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
        [SerializeField] private SpriteRenderer frameSpriteRenderer;
        [SerializeField] private EntityCollide entityCollide;
        [SerializeField] private SpriteRenderer manaInnerBarRenderer;
        [SerializeField] private SkillController skillController;
        [SerializeField] private DefenderVisual defenderVisual;

        public DefenderEventManager eventManager;

        /*public delegate void OnSpawnEventHandler();
        public delegate void OnBeforeOriginalAttackEventHandler(Monster target,DefenderStateId from);
        public delegate void OnBeforeAttackEventHandler(Monster target,DefenderStateId from);
        public delegate void OnUseSkillEventHandler(Monster target, bool isFinal = false);
        public delegate void OnAttackEventHandler(Monster target);
        public delegate void OnKillEventHandler(Monster target);
        public delegate void OnTargetHitEventHandler(Monster target , int damage);
        
        public delegate void OnTargetCommonHitEventHandler(Monster target, HitInfo hitInfo);*/

        /*public OnSpawnEventHandler OnSpawnAction;
        public OnBeforeOriginalAttackEventHandler OnBeforeOriginalAttackAction;
        public OnBeforeAttackEventHandler OnBeforeAttackAction;
        public OnUseSkillEventHandler OnUseSkillAction;
        public OnKillEventHandler OnKillAction;
        public OnTargetHitEventHandler OnTargetHitAciton*/

        private Monster commonAttackTarget;

        public DefenderStatus status;
        private SkillData skillData;

        public double attackTimer = 0;
        private bool isOn = true;
        private bool isInSellZone = false;

        private void Awake()
        {
            eventManager = new DefenderEventManager();
            
            bulletManager = ReferenceManager.instance.bulletManager;
            defenderManager = ReferenceManager.instance.defenderManager;
            marketManager = ReferenceManager.instance.marketManager;
            uiManager = ReferenceManager.instance.uiManager;
            
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
                if (attackTimer > status.attackSpeedTimer && !ReferenceEquals(commonAttackTarget,null))
                {
                    CommonAttack();
                    //OriginalAttack();
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
            OnManaChanged(status.currentMaxMana , status.maxMana);
            status.OnManaChangedAction += OnManaChanged;
            skillData = _defenderData.skillData;
            skillController.SetSkillData(this , skillData , stateController);
            traitController.SetTrait(this);
            stateController.Init();
            defenderVisual.SetStar(_defenderData.cost , status.level);
        }

        public void OnSpawn()
        {
            eventManager.OnSpawnAction?.Invoke();
            
            //OnSpawnAction?.Invoke();
            
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

        public void OnKillTarget(Monster _target)
        {
            eventManager.OnKillAction?.Invoke(_target);
            
            //OnKillAction?.Invoke(target);
        }

        public void OnTargetHit(Monster _target , AttackInfo attackInfo)
        {
            eventManager.OnTargetCommonHitAction?.Invoke(_target , attackInfo);
            //OnTargetHitAciton?.Invoke(target,damage);
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
            if (ReferenceEquals(commonAttackTarget , null))
            {
                commonAttackTarget = _target;
            }
        }

        public void TrySetUnTarget(Monster _target)
        {
            if (ReferenceEquals(commonAttackTarget , _target))
            {
                commonAttackTarget = null;
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

            if (targets.Contains(commonAttackTarget)) targets.Remove(commonAttackTarget);

            return targets;
        }

        void LevelUp()
        {
            if (status.level < 3)
            {
                status.LevelUp();
                defenderVisual.SetStar(status.defenderData.cost , status.level);
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

        void CommonAttack(bool isBeforeEventOn = true, bool isAfterEventOn = true)
        {
            AttackInfo attackInfo = AttackInfo.CommonAttack(this);
            if(isBeforeEventOn)
                eventManager.BeforeCommonAttackAction?.Invoke(commonAttackTarget , attackInfo);
            
            bulletManager.Fire(this, commonAttackTarget.gameObject,attackInfo);
            attackTimer = 0;
            
            if(status.ManaUp(20))
            {
                UseSkill(AttackInfo.SkillAttack(this));
            }

            if(isAfterEventOn)
                eventManager.AfterCommonAttackAction?.Invoke(commonAttackTarget , attackInfo);
        }

        
        /*
        void OriginalAttack()
        {
            eventManager.BeforeCommonAttackAction?.Invoke();
            
            OnBeforeOriginalAttackAction?.Invoke(target,DefenderStateId.None);
            OnBeforeAttackAction?.Invoke(target,DefenderStateId.None);
            
            bulletManager.Fire(gameObject, target.gameObject);
            attackTimer = 0;
            if(status.ManaUp(20))
            {
                UseSkill();
            }
        }
        */

        
        /// <summary>
        /// 추가 공격(서커스단 등에서 활용) 
        /// </summary>
        public void SetExtraAttack(
            Monster _target , 
            DefenderStateId from, 
            bool isActiveBeforeEvent = true,
            bool isActiveAfterEvent = true)
        {
            AttackInfo attackInfo = AttackInfo.CommonAttack(this);
            if (isActiveBeforeEvent)
                eventManager.BeforeCommonAttackAction?.Invoke(_target , attackInfo);
            
            bulletManager.Fire(this, _target.gameObject , attackInfo);
            
            if(isActiveAfterEvent) 
                eventManager.AfterCommonAttackAction?.Invoke(_target , attackInfo);
        }

        public void UseSkill(AttackInfo attackInfo , bool isActiveAfterAction = true)
        {
            skillController.UseSkill(new List<Monster> { commonAttackTarget });
            
            if(isActiveAfterAction)
                eventManager.AfterUseSkillAction?.Invoke(commonAttackTarget , attackInfo);
            //OnUseSkillAction?.Invoke(target);
        }

        /*public void UseSkillFinal()
        {
            skillController.UseSkill(new List<Monster> { commonAttackTarget });
            
            eventManager.AfterUseSkillAction?.Invoke();
            
            //OnUseSkillAction?.Invoke(target , true);
        }*/

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


