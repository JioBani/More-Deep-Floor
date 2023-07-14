using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.Loggers;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.DefenderTraits;
using LNK.MoreDeepFloor.Data.EntityStates;
using LNK.MoreDeepFloor.Data.EntityStates.Trait.Crops;
using LNK.MoreDeepFloor.Data.EntityStates.Traits;
using LNK.MoreDeepFloor.Data.EntityStates.Traits.Personalities;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Entitys.States
{
    public class EntityStateGenerator : MonoBehaviour
    {
        private Dictionary<EntityStateId, EntityStateData> stateDataDic;
        private Dictionary<TraitId, TraitData> traitStateDic;

        [SerializeField] private EntityStateDataBase entityStateDataBase;
        [SerializeField] private TraitDataBase traitDataBase;
        

        private void Awake()
        {
            stateDataDic = entityStateDataBase.GetDic();
            traitStateDic = traitDataBase.GetDic();
        }

        /*public T Generate<T>(EntityStateId id) where T : EntityState
        {
            if(!stateDataDic.TryGetValue(id , out var state))
            {
                CustomLogger.LogWarning($"[EntityStateGenerator.Generate()] Entity State가 존재하지 않음 : {id}");
                return null;
            }
            else
            {
                return state;
            }
        }*/

        public T GenerateToSpecificType<T>(EntityStateId stateId , Defender self) where T : EntityState
        {
            if(!stateDataDic.TryGetValue(stateId , out var stateData))
            {
                CustomLogger.LogWarning($"[EntityStateGenerator.GenerateToSpecificType()] Entity State가 존재하지 않음 : id = {stateId}");
                return null;
            }

            return GenerateToSpecificType<T>(stateData, self);
        }
        
        /// <summary>
        /// EntityState 추가
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GenerateToSpecificType<T>(EntityStateData stateData  , Defender self) where T : EntityState
        {
            
            switch (stateData.StateId)
            {
                case EntityStateId.Corps_JadeKnights : return new CorpsState_JadeKnights(stateData, self) as T;
                case EntityStateId.Personality_Test : return new PersonalityState_Test(stateData, self) as T;
            }

            CustomLogger.LogWarning($"[EntityStateGenerator.GenerateToSpecificType()] Entity State가 존재하지 않음 : id = {stateData.StateId}");
            return null;
        }
        
        
        public TraitState GenerateAsTraitState(TraitId traitId , Defender self)
        {
            if(!traitStateDic.TryGetValue(traitId , out var traitData))
            {
                return null;
            }
            
            return GenerateToSpecificType<TraitState>(traitData.TraitStateData.StateId , self);
        }

        public TraitState GenerateAsTraitState(TraitData traitData, Defender self)
        {
            TraitState state = GenerateToSpecificType<TraitState>(traitData.TraitStateData, self);
            return state;
        }
    }
}


