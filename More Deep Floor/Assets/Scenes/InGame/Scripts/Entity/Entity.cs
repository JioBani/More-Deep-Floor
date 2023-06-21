using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States;
using LNK.MoreDeepFloor.InGame.PathFinder;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys
{
    

    public abstract class Entity : MonoBehaviour
    {
        public delegate void OnSpawnEventHandler();
        
        public delegate void OnBeforeOriginalAttackEventHandler(Monster target,DefenderStateId from);

        
    }
}
