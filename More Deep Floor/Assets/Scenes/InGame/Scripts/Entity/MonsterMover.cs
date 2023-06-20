using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entity.Monsters
{
    public class MonsterMover : MonoBehaviour
    {
        private Vector2 startPos;
        private Vector2 endPos;
        private bool isMoving;
        private bool pause;
        
        public MonsterStatusValue speed;
        
        public delegate void ArriveAction();

        public ArriveAction OnArriveEvent;

        public void Init()
        {
            isMoving = false;
            pause = false;
        }

        public void SetSpeed(MonsterStatusValue _speed)
        {
            speed = _speed;
        }
        
        public void SetRoute(Vector2 _startPos , Vector2 _endPos)
        {
            startPos = _startPos;
            endPos = _endPos;
        }

        public void SetPause(bool _pause)
        {
            pause = _pause;
        }

        public void StartMove()
        {
            transform.position = startPos;
            isMoving = true;
        }

        void OnArrived()
        {
            isMoving = false;
            OnArriveEvent?.Invoke();
        }
        
        private void FixedUpdate()
        {
            if (isMoving && !pause)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position , 
                    endPos ,
                    speed.currentValue);
                
                if (Vector2.Distance(transform.position, endPos) < 0.01f)
                {
                    OnArrived();
                }
            }
        }
        
    }
}


