using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defenders.States.Schemas.Traits;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using UnityEngine;


namespace LNK.MoreDeepFloor.Data.Schemas
{
    public abstract class RuntimeTraitData
    {
        public TraitData traitData;

        public RuntimeTraitData(TraitData _traitData)
        {
            traitData = _traitData;
        }

        public abstract TraitState GetState(Defender defender);
    }
}

