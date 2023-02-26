using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Direction;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity
{
    public class Mover : MonoBehaviour
    {
        private List<Tile> destinations;
        private int currentDestiIndex = -1;
        private Tile currentDesti;
        private Tile lastTile;
        public bool isMoving = false;
        public MonsterStatusValue speed;
        public delegate void Action(Tile nextDestination);

        public delegate void EndAction();

        public Action OnArrivedAction;
        public Action OnDepartAction;
        public EndAction OnMoveEnd;

        public bool pause;

        public void SetRoute(List<Tile> destinations)
        {
            lastTile = ReferenceManager.instance.tileManager.battleFieldTiles[0][1];
            this.destinations = destinations;
            currentDestiIndex = -1;
        }

        public void Move()
        {
            Depart();
        }

        /*public void Depart()
        {
            if (currentDestiIndex == destinations.Count - 1)
            {
                isMoving = false;
                OnMoveEnd?.Invoke();
            }
            else
            {
                if (currentDestiIndex == -1)
                {
                    currentDestiIndex = 0;
                    isMoving = true;
                }
                else
                {
                    lastTile = destinations[currentDestiIndex];
                    currentDestiIndex++;
                }

                currentDesti = destinations[currentDestiIndex];
                OnDepartAction?.Invoke(currentDesti);
            }
        }*/

        public void Depart()
        {
            if (currentDestiIndex == destinations.Count - 1)
            {
               //isMoving = false;
                lastTile = destinations[currentDestiIndex];
                currentDestiIndex = 0;
                //OnMoveEnd?.Invoke();
            }
            else
            {
                if (currentDestiIndex == -1)
                {
                    currentDestiIndex = 0;
                    isMoving = true;
                }
                else
                {
                    lastTile = destinations[currentDestiIndex];
                    currentDestiIndex++;
                }
            }
            currentDesti = destinations[currentDestiIndex];
            OnDepartAction?.Invoke(currentDesti);
        }

        public void OnArrived()
        {
            OnArrivedAction?.Invoke(currentDesti);
            Depart();
        }

        private void FixedUpdate()
        {
            if (isMoving && !pause)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position , 
                    currentDesti.transform.position ,
                    speed.currentValue);
                
                if (Vector2.Distance(transform.position, currentDesti.transform.position) < 0.01f)
                {
                    OnArrived();
                }
            }
        }

        public Direction GetMoveDirection()
        {
            Vector2Int way = currentDesti.index - lastTile.index;

            if (way.x == 0)
            {
                if (way.y < 0)
                    return Direction.Up;
                else
                    return Direction.Down;
            }
            else
            {
                if (way.x < 0)
                    return Direction.Right;
                else
                    return Direction.Left;
            }
        }

        public void Init(MonsterStatus status)
        {
            speed = status.speed;
            SetPause(false);
        }
        
        public void SetPause(bool _isPause)
        {
            pause = _isPause;
        }
    }
}

