using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.ProbabilityChecks;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defender.States.Test
{
    public class DefenderStateData : ScriptableObject
    {
        [SerializeField] private DefenderStateId id;
        public DefenderStateId Id => id;
    }
}
