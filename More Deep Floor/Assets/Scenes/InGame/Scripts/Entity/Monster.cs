using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common;
using LNK.MoreDeepFloor.Common.Direction;
using LNK.MoreDeepFloor.Common.EventHandlers;
using LNK.MoreDeepFloor.InGame.Bullets;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DamageTexts;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entity.Monsters;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class Monster : MonoBehaviour
    {
        private TileManager tileManager;
        private MarketManager _marketManager;
        private ObjectPooler textPooler;
        
        //private Mover mover;
        private MonsterMover mover;
        private Animator animator;
        private Poolable poolable;
        private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer innerHpBarRender;
        [SerializeField] private TextMeshPro hpText;

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

        void Awake()
        {
            tileManager = ReferenceManager.instance.tileManager;
            _marketManager = ReferenceManager.instance.marketManager;
            textPooler = ReferenceManager.instance.objectPoolingManager.textPooler;
            
            animator = GetComponent<Animator>();
            mover = GetComponent<MonsterMover>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            poolable = GetComponent<Poolable>();

            mover.OnArriveEvent = () =>
            {
                poolable.SetOff();
            };

           
        }

        public void Init(MonsterData _monsterData , int _line)
        {
            transform.position = tileManager.battleFieldTiles[0][1].transform.position;
            innerHpBarRender.size = new Vector2(1,0.125f);
            
            monsterData = _monsterData;
            animator.runtimeAnimatorController = monsterData.animatorOverrideController;
            animator.SetTrigger(Down);
            hpText.text = monsterData.hp.ToString();
            
            status = new MonsterStatus(monsterData);
            line = _line;

            InitMover();
        }

        void InitMover()
        {
            mover.Init();
            mover.SetSpeed(status.speed);
            mover.SetRoute(
                tileManager.monsterSpawnTiles[line].transform.position , 
                tileManager.battleFieldTiles[line][0].transform.position);
        }
        
        public void SetMove()
        {
            //transform.position = tileManager.battleFieldTiles[0][1].transform.position;
            //transform.position = startTile.transform.position;
            mover.StartMove();
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

        /*public void OnHit(Bullet bullet)
        {
            ChangeHp((int)-bullet.firer.status.damage.currentValue, bullet.firer);
        }*/

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
            mover.SetPause(isStun);
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
            innerHpBarRender.size = new Vector2((float)(status.currentHp / status.maxHp), innerHpBarRender.size.y);
        }
    }
    
}

