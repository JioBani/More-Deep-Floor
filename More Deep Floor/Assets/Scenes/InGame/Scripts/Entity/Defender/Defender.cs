using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Palettes;
using LNK.MoreDeepFloor.Common.WaitForSecondsCache;
using LNK.MoreDeepFloor.Data.Defenders;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders.States;
using LNK.MoreDeepFloor.InGame.Entitys.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.SkillSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using LNK.MoreDeepFloor.InGame.Ui;
using TMPro;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.Entitys
{


    #region Old Defender

    /*public class Defender : Entity
    {
        private BulletManager bulletManager;
        private DefenderManager defenderManager;
        private MarketManager marketManager;
        protected UiManager uiManager;
        
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
        [SerializeField] private HpBar hpBar;
        [SerializeField] private Collider2D defenderTargetCol;
        [SerializeField] private TextMeshPro shieldText;

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
        private DefenderLifeState state = DefenderLifeState.None;

        #region #. 유니티 이벤트함수

        private void Awake()
        {
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

            status = new DefenderStatus();
        }
        

        void Update()
        {
            if (state == DefenderLifeState.None || state == DefenderLifeState.Die)
            {
                attackTimer = 0;
            }
            else if (isOn)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > status.attackSpeedTimer && !ReferenceEquals(target,null))
                {
                    OriginalAttack();
                }
            }
        }

        #endregion

        #region #. 드래그 및 클릭 이벤트 함수 

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
        
        public void EnterWaitingRoom()
        {
            defenderManager.OnDefenderEnterWaitingRoom(this);
            isOn = false;
            state = DefenderLifeState.None;
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
            state = DefenderLifeState.Default;
        }
        
        public void ExitBattleField()
        {
            defenderManager.OnDefenderExitBattleField(this);
        }       
        
        void OnSimpleClick()
        {
            uiManager.OnClickDefender(this);
        }
        
       

        #endregion

        #region #. 앤티티 이벤트 함수

        public void Init(DefenderData _defenderData , int level)
        {
            //status.SetStatus(_defenderData , _defenderData.l);
            //status = new DefenderStatus(_defenderData);
            //status = new DefenderStatus();
            
            status.SetStatus(_defenderData , level);
            
            spriteRenderer.sprite = _defenderData.sprite;
            frameSpriteRenderer.color = Palette.defenderCostColors[_defenderData.cost];
            OnManaChanged(status.maxMana.currentValue , status.currentMana);
            
            status.OnManaChangedAction += OnManaChanged;
            status.OnHpChangedAction += OnHpChanged;
            
            skillData = _defenderData.skillData;
            skillController.SetSkillData(this , skillData , stateController);
            traitController.SetTrait(this);
            stateController.Init();
            defenderVisual.SetStar(_defenderData.cost , status.level);
            
            status.shieldController.AddListener(OnShieldChange);
            hpBar.RefreshBar(status.maxHp.currentValue, status.currentHp,status.shieldController.amount);
        }
        
        protected override void OnSpawn()
        {
            OnSpawnAction?.Invoke();
            
            defenderManager.CheckMerge(this);
            
            AddShield( id : "테스트쉴드" , 
                maxAmount:100 ,
                lateTime: 0,
                state: stateController.AddState(DefenderStateId.Test_TestShield));
        }

        public void OnDie()
        {
            state = DefenderLifeState.Die;
            //Logger.Log($"{name} 죽음");
            defenderTargetCol.gameObject.SetActive(false);
            StartCoroutine(Revive());
            //status.ChangeHp(status.maxHp.currentValue);
        }

        public void OnKillTarget(Monster target)
        {
            OnKillAction?.Invoke(target);
        }

        public void OnTargetHit(Monster target, int damage)
        {
            OnTargetHitAciton?.Invoke(target,damage);
        }

        public void SpawnAtWaitingRoom(Tile tile)
        {
            placer.TryMove(tile);
        }
        
        void OnManaChanged(float maxMana, float currentMana)
        {
            manaInnerBarRenderer.size = new Vector2(status.currentMana / status.maxMana.currentValue ,0.25f);
        }

        void OnShieldOfHpChanged()
        {
            hpBar.RefreshBar(status.maxHp.currentValue, status.currentHp,status.shieldController.amount);
        }

        void OnHpChanged(float maxHp , float currentHp , Entity caster)
        {
            hpBar.RefreshBar((int)maxHp , (int)currentHp,status.shieldController.amount);
            if (currentHp <= 0)
            {
                OnDie();
            }
        }

        void OnShieldChange(float amount)
        {
            shieldText.text = amount.ToString();
        }

        #endregion
        

        #region #. 행동 함수
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

        public override void SetOff()
        {
            defenderManager.OnDefenderOff(this);
            placer.SetOff();
            poolable.SetOff();
        }

        void OriginalAttack()
        {
            OnBeforeOriginalAttackAction?.Invoke(target,DefenderStateId.None);
            OnBeforeAttackAction?.Invoke(target,DefenderStateId.None);
            bulletManager.Fire(this, target.GetComponent<Monster>() , (int)status.damage.currentValue , AttackType.DefenderToMonster);
            attackTimer = 0;
            if(status.ManaUp(20))
            {
                UseSkill();
            }
        }

        public void SetExtraAttack(Monster _target , DefenderStateId from)
        {
            OnBeforeAttackAction?.Invoke(target, from);
            bulletManager.Fire(this, _target.GetComponent<Monster>() , (int)status.damage.currentValue , AttackType.DefenderToMonster);
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

        public void SetHit(float value , Monster firer)
        {
            float leftValue = status.shieldController.SetDamage(value);
            status.ChangeHp(-leftValue , firer);
            OnShieldOfHpChanged();
        }

        IEnumerator Revive()
        {
            yield return YieldInstructionCache.WaitForSeconds(4f);
            //defenderTargetCol.gameObject.layer = LayerMask.NameToLayer("DefenderSearcher");
            defenderTargetCol.gameObject.SetActive(true);
            status.ChangeHp(status.maxHp.currentValue , null);
            state = DefenderLifeState.Default;
        }

        void AddShield(string id , float maxAmount , DefenderState state ,  float lateTime, bool isHasTimeoutAction = false)
        {
            status.shieldController.AddShield(
                id ,
                maxAmount ,
                lateTime,
                state,
                isHasTimeoutAction
                );
            
            OnShieldOfHpChanged();
        }

        #endregion

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
    }*/

    #endregion
    
    /*enum DefenderLifeState
    {
        None = 0,
        Wait = 1,
        Battle = 2,
        Die = 3,
    }*/
    
    public class Defender : Entity
    {
        private MarketManager marketManager;
        private DefenderManager defenderManager;

        private Dragger dragger;
        private Poolable poolable;
        private Placer placer;
        [SerializeField] private SpriteRenderer frameSpriteRenderer;
        private TraitController traitController;
        [SerializeField] private DefenderVisual defenderVisual;
        [SerializeField] private Collider2D defenderTargetCol;
        [SerializeField] private SpriteRenderer defenderImage;
        private UiManager uiManager;
        //public DefenderStatus defenderStatus;


        public DefenderData defenderData;
        private bool isInSellZone;

        #region 이벤트 함수

        public delegate void PlaceEventHandler();
        public PlaceEventHandler OnEnterWaitingRoomAction;
        public PlaceEventHandler OnEnterBattleFieldAction;
        public PlaceEventHandler OnExitWaitingRoomAction;
        public PlaceEventHandler OnExitBattleFieldAction;

        #endregion

        protected override void EntityAwake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            defenderManager = ReferenceManager.instance.defenderManager;
            uiManager = ReferenceManager.instance.uiManager;
            
            traitController = GetComponent<TraitController>();
            dragger = GetComponent<Dragger>();
            poolable = GetComponent<Poolable>();
            placer = GetComponent<Placer>();
            
            
            dragger.OnDragEndAction += OnDragEnd;
            dragger.OnSimpleClickAction += OnSimpleClick;

            placer.OnEnterBattleFieldAction = EnterBattleField;
            placer.OnEnterWaitingRoomAciton = EnterWaitingRoom;
            placer.OnExitBattleFieldAction = ExitBattleField;
            placer.OnExitWatingRoomAction = ExitWaitingRoom;
            
            //entityCollide.OnTriggerEnter2DAciton += OnEntityTriggerEnter;
            //entityCollide.OnTriggerExit2DAciton += OnEntityTriggerExit;
        }


        public override float GetAttackDamage()
        {
            throw new System.NotImplementedException();
        }
        

        #region #. 라이프 이벤트 함수

        protected override void SetDie(Entity killer)
        {
            
        }

        protected override void OnDie(Entity killer)
        {
            base.OnDie(killer);
            defenderTargetCol.gameObject.SetActive(false);
            attacker.AddAttackDisableStack("Die" , 1);
            StartCoroutine(Revive());
            Logger.Log("OnDie");
        }
        
        protected override void OnInit(EntityData entityData, int level)
        {
            defenderData = entityData as DefenderData;
            
            Logger.Log($"[Defender.Init()] {defenderData}");

            //defenderStatus = status as DefenderStatus;

            frameSpriteRenderer.color = Palette.defenderCostColors[defenderData.cost];
            traitController.SetTrait(this);
            defenderVisual.SetStar(defenderData.cost , status.level);
            
            defenderImage.sprite = defenderData.sprite;
            frameSpriteRenderer.color = Palette.defenderCostColors[defenderData.cost];
            
            
            //status.OnManaChangedAction += OnManaChanged;
        }

        protected override void OnSpawn()
        {
            defenderManager.CheckMerge(this);
            
            /*AddShield( id : "테스트쉴드" , 
                maxAmount:100 ,
                lateTime: 0,
                state: stateController.AddState(DefenderStateId.Test_TestShield));*/
        }

        void OnRevive()
        {
            defenderTargetCol.gameObject.SetActive(true);
            status.ChangeHp(status.maxHp.currentValue , null);
            attacker.RemoveAttackDisableStack("Die" , 0 , removeAll:true);
            SetLifeState(EntityLifeState.Battle);
            Logger.Log("OnRevive");
        }

        #endregion

        #region #. 행동함수
        
        IEnumerator Revive()
        {
            yield return YieldInstructionCache.WaitForSeconds(4f);
            OnRevive();
        }

        public override void SetOff(Entity killer , string msg = null)
        {
            defenderManager.OnDefenderOff(this);
            placer.SetOff();
            poolable.SetOff();
        }
        
        void LevelUp()
        {
            if (status.level < 3)
            {
                status.LevelUp();
                defenderVisual.SetStar(defenderData.cost , status.level);
            }
        }

        public void Merge()
        {
            LevelUp();
        }

        public void BeMerged(Defender _defender)
        {
            SetOff(this);
        }
        
        public void SpawnAtWaitingRoom(Tile tile)
        {
            placer.TryMove(tile);
        }


        #endregion


        #region #. 드래그 및 클릭 이벤트 함수 

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
        
        void OnDragEnd()
        {
            if (isInSellZone)
            {
                marketManager.SellDefender(this);
                SetOff(null , "판매");
                Logger.Log($"[Defender.OnDragEnd()] 판매 : {defenderData.spawnId}");
            }
            else
            {
                placer.TryMove(SearchTile());
            }
            
        }
        
        public void EnterWaitingRoom()
        {
            //Set
            SetLifeState(EntityLifeState.Wait);
            attacker.AddAttackDisableStack("wait" , 1);
            status.SetManaGain(false);
            //isOn = false;
            //state = DefenderLifeState.None;
            defenderManager.OnDefenderEnterWaitingRoom(this);  
            //stateController.AddState();
        }

        public void ExitWaitingRoom()
        {
            //status.SetManaGain(false);
            defenderManager.OnDefenderExitWaitingRoom(this);
        }

        public void EnterBattleField()
        {
            //isOn = true;
            //state = DefenderLifeState.Default;
            SetLifeState(EntityLifeState.Battle);
            attacker.RemoveAttackDisableStack("wait" , 1, true);
            status.SetManaGain(true);
            
            defenderManager.OnDefenderEnterBattleField(this);
        }
        
        public void ExitBattleField()
        {
            defenderManager.OnDefenderExitBattleField(this);
        }       
        
        void OnSimpleClick()
        {
            uiManager.OnClickDefender(this);
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
       

        #endregion

        
    }
    
}


