using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.Common.Direction;
using LNK.MoreDeepFloor.Common.EventHandlers;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DamageTexts;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using LNK.MoreDeepFloor.InGame.Entitys.Monsters;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using TMPro;
using UnityEngine;
using Logger = LNK.MoreDeepFloor.Common.Loggers.Logger;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    public class Monster : Entity
    {
        private TileManager tileManager;
        private MarketManager _marketManager;
        private ObjectPooler textPooler;
        private BulletManager bulletManager;
        
        //private Mover mover;
        private MonsterMover mover;
        private Animator animator;
        private Poolable poolable;
        private SpriteRenderer spriteRenderer;
        [SerializeField] private HpBar hpBar;
        /*[SerializeField] private SpriteRenderer innerHpBarRender;
        [SerializeField] private TextMeshPro hpText;*/
        [SerializeField] private DefenderSearcher searcher;

        private Direction direction;
        private List<Tile> route;
        public MonsterData monsterData;

        public Void_EventHandler OnDieAction;
        public Void_EventHandler OnPassAction;
        public Void_EventHandler OnOffAction;

        private static readonly int Down = Animator.StringToHash("Down");
        private static readonly int Up = Animator.StringToHash("Up");
        private static readonly int Side = Animator.StringToHash("Side");

        public MonsterStatus status;
        
        public bool isStun = false;
        private int line;
        private Defender target;

        private float attackTimer = 0;

        void Awake()
        {
            tileManager = ReferenceManager.instance.tileManager;
            _marketManager = ReferenceManager.instance.marketManager;
            textPooler = ReferenceManager.instance.objectPoolingManager.textPooler;
            bulletManager = ReferenceManager.instance.bulletManager;
            
            animator = GetComponent<Animator>();
            mover = GetComponent<MonsterMover>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            poolable = GetComponent<Poolable>();

            mover.OnArriveEvent = () =>
            {
                poolable.SetOff();
            };

            searcher.OnTargetSearchEvent += OnTargetSearch;
            searcher.OnTargetLostEvent += OnTargetLost;

            status = new MonsterStatus();
        }

        private void Update()
        {
            if (searcher.isTargetExist)
            {
                if (attackTimer < 1)
                {
                    attackTimer += Time.deltaTime;
                }
                else
                {
                    Attack();
                }
            }
        }
        
        /*public void FixedUpdate()
        {
            if (!ReferenceEquals(searcher.target, null))
            {
                Logger.Log($"[Monster.Attack()] 타겟 있음");
                mover.U(true);
            }
            else 
            {
                Logger.Log($"[Monster.Attack()] 타겟 없음");
                mover.SetPause(false);
            }
        }*/



        public void Init(MonsterData _monsterData , int _line)
        {
            transform.position = tileManager.battleFieldTiles[0][1].transform.position;
            //innerHpBarRender.size = new Vector2(1,0.125f);
            
            monsterData = _monsterData;
            animator.runtimeAnimatorController = monsterData.animatorOverrideController;
            animator.SetTrigger(Down);
            //hpText.text = monsterData.hp.ToString();
            
            status.SetStatus(monsterData , 0);
            
            //status = new MonsterStatus(monsterData);
            line = _line;
            
            hpBar.RefreshBar((int)status.maxHp.currentValue , (int)status.currentHp);
            
            InitMover();
        }

        void InitMover()
        {
            mover.Init();
            mover.SetSpeed(status.moveSpeed);
            mover.SetRoute(
                tileManager.monsterSpawnTiles[line].transform.position , 
                tileManager.battleFieldTiles[line][0].transform.position);
        }
        
        public void SetMove()
        {
            //transform.position = tileManager.battleFieldTiles[0][1].transform.position;
            //transform.position = startTile.transform.position;
            mover.StartMove();
            //Invoke(nameof(Attack),1f );
        }

        /*public void SetRoute(Tile _startTile , List<Tile> route)
        {
            mover.SetRoute(route , _startTile);
        }*/

        /*public void SetLine(int _line)
        {
            line = _line;
        }*/

        public void SetActions(
            Void_EventHandler onDieAction,
            Void_EventHandler onPassAction,
            Void_EventHandler onOffAction)
        {
            OnOffAction = onDieAction;
            OnDieAction = onPassAction;
            OnPassAction = onOffAction;
        }
        
        void SetDie()
        {
            OnDie();
        }

        void SetOff()
        {
            OnOff();
            poolable.SetOff();
        }
        

        public void SetHit(int damage, Defender caster)
        {
            ChangeHp(-damage , caster);
            caster.OnTargetHit(this, damage);
        }

        public void SetHit(float damage, Defender caster) => SetHit((int)damage, caster);

        public void SetHitFinal(int damage, Defender caster)
        {
            ChangeHp(-damage , caster);
        }

        public void SetHitFinal(float damage, Defender caster) => SetHitFinal((int)damage, caster);
        
        public void SetHitWithBuff(AttackInfo attackInfo)
        {
            SetHit(attackInfo.damage, attackInfo.caster);
            attackInfo.OnMonsterHitAction?.Invoke(this);
        }

        public void SetStun(bool _isStun)
        {
            isStun = _isStun;
            //mover.SetPause(isStun);
            mover.UpPauseStack();
        }

        void OnDie()
        {
            OnDieAction?.Invoke();
            SetOff();
        }

        void OnPass()
        {
            OnPassAction?.Invoke();
            SetOff();
        }

        void OnOff()
        {
            OnOffAction?.Invoke();
        }

        void ChangeHp(int value , Defender caster)
        {
            status.currentHp += value;
            if (value < 0)
            {
                textPooler.Pool().GetComponent<DamageText>().SetOn(value , transform.position);
            }
            OnHpChanged(value);
            if (status.currentHp <= 0)
            {   
                caster.OnKillTarget(this);
                SetDie();
            }
        }

        void OnHpChanged(int value)
        {
            hpBar.RefreshBar((int)status.maxHp.currentValue ,(int)status.currentHp);
        }

       
        void Attack()
        {
            if (!ReferenceEquals(searcher.target, null))
            {
                bulletManager.Fire(this , searcher.target, 10,AttackType.MonsterToDefender);
                attackTimer = 0;
            }
        }

        void OnTargetSearch()
        {
            mover.UpPauseStack();
        }

        void OnTargetLost()
        {
            mover.DownPauseStack();
        }
    }
    
}

