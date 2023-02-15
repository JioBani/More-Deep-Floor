using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.EventHandlers;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class Placer : MonoBehaviour
    {
        private DefenderManager defenderManager;
        
        public Void_EventHandler OnEnterWaitingRoomAciton;
        public Void_EventHandler OnEnterBattleFieldAction;
        public Void_EventHandler OnExitWatingRoomAction;
        public Void_EventHandler OnExitBattleFieldAction;

        private Defender defender;
        
        public Tile currentTile { get; private set; } = null;

        private void Awake()
        {
            defenderManager = ReferenceManager.instance.defenderManager;
            defender = GetComponent<Defender>();
        }
        
        TileType OrderCompare(TileType a, TileType b)
        {
            return a | (TileType)((int)b * 2);
        }

        public void Init()
        {
            currentTile = null;
        }

        public bool TryMove(Tile to)
        {
            Tile from = currentTile;
            
            //#. 들어갈수 있는지
            if (to == null)
            {
                MoveToLastTile();
                return false;
            }

            if (to == currentTile)
            {
                MoveToLastTile();
                return false;
            }

            if (to.type == TileType.Road)
            {
                MoveToLastTile();
                Debug.Log("[Placer.TryMove()] 길 위에는 배치 할 수 없습니다.");
                return false;
            }

            //#. 실행
            if (to.placer == null)
            {
                //#. 배치 최대치 확인
                if (currentTile != null && 
                    currentTile.type != TileType.BattleField && 
                    to.type == TileType.BattleField && 
                    !defenderManager.CheckBattleDefenderLimit())
                {
                    MoveToLastTile();
                    Debug.Log("[Placer.TryMove()] 배치 최대치에 도달했습니다.");
                    return false;
                }
                
                //#. 이동
                TryLeftTile(from,to);
                EnterTile(from,to);
                defenderManager.SetDefenderPlaceChange(defender);
                return true;
            }
            else
            {
                Placer target = to.placer;
                //#. 교환
                TryLeftTile(from,to);
                target.TryLeftTile(to,from);
            
                target.EnterTile(to,from);
                EnterTile(from,to);
                
                defenderManager.SetDefenderPlaceChange(defender);
                defenderManager.SetDefenderPlaceChange(target.defender);
                
                return true;
            }
        }

        void CheckEnterEvent(Tile from, Tile to)
        {
            TileType fromType;
            TileType toType = to.type;
            
            if(from == null)
                fromType = TileType.Blank;
            else
                fromType = from.type;

            TileType flag = OrderCompare(fromType , toType);
            
            Debug.Log($"[Placer.EnterTile()] 타입 : {fromType}->{toType} : {flag}");

            if (flag == OrderCompare(TileType.Blank, TileType.WaitingRoom))
            {
                SetEnterWaitingRoom();
            }
            else if(flag == OrderCompare(TileType.Blank, TileType.BattleField))
            {
                SetEnterBattleField();
            }
            else if(flag == OrderCompare(TileType.WaitingRoom, TileType.BattleField))
            {
                SetEnterBattleField();
            }
            else if(flag == OrderCompare(TileType.BattleField, TileType.WaitingRoom))
            {
                SetEnterWaitingRoom();
            }
            else
            {
                Debug.Log($"[Placer.EnterTile()] 알수없는 타일 타입 : {fromType}->{toType}");
            }
            
        }

        public void EnterTile(Tile from , Tile to)
        {
            transform.position = new Vector3(to.transform.position.x, to.transform.position.y,transform.position.z);
            currentTile = to;
            currentTile.SetEntity(this);
            CheckEnterEvent(from ,to);
        }

       
        public void TryLeftTile(Tile from, Tile to)
        {
            if (currentTile != null)
            {
                currentTile.LeftEntity();
            }

            TileType fromType;
            TileType toType;
            
            if(to == null)
                toType = TileType.Blank;
            else
                toType = to.type;

            if(from == null)
                fromType = TileType.Blank;
            else
                fromType = from.type;

            TileType flag = OrderCompare(fromType , toType);
            
            Debug.Log($"[Placer.Exit()] 타입 : {fromType}->{toType} : {flag}");

            if(flag == OrderCompare(TileType.WaitingRoom, TileType.BattleField))
            {
                OnExitWatingRoomAction?.Invoke();
            }
            else if(flag == OrderCompare(TileType.BattleField, TileType.WaitingRoom))
            {
                OnExitBattleFieldAction?.Invoke();
            }
            else if (flag == OrderCompare(TileType.BattleField, TileType.Blank))
            {
                OnExitBattleFieldAction?.Invoke();
            }
            else
            {
                Debug.Log($"[Placer.EnterTile()] 알수없는 타일 타입 : {fromType}->{toType}");
            }
        }

        public void SetOff()
        {
            TryLeftTile(currentTile, null);
        }
        
        void SetEnterWaitingRoom()
        {
            OnEnterWaitingRoomAciton?.Invoke();
        }

        void SetEnterBattleField()
        {
            OnEnterBattleFieldAction?.Invoke();
        }
        
        void MoveToLastTile()
        {
            transform.position = new Vector3(
                currentTile.transform.position.x , 
                currentTile.transform.position.y , 
                transform.position.z
            );
        }
    }
}


