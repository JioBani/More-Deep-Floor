using System;
using System.Collections.Generic;
using ExtensionMethods;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.Data.Traits;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys;

namespace LNK.MoreDeepFloor.InGame.TraitSystem
{
    [Serializable]
    public class ActiveTraitInfo
    {
        public TraitData traitData { get; private set; }
        public List<Defender> defenders { get; private set; } = new List<Defender>();
        public List<DefenderData> activeDefenderDatas { get; private set; } = new List<DefenderData>();

        public List<TraitAdapter> traitAdapters { get; private set; } = new List<TraitAdapter>();
        
        public int synergyLevel { get; private set; } = 0;
        public int activeNumber { get; private set; } = 0;

        public delegate void OnTraitChangeEventHandler(ActiveTraitInfo activeTraitInfo);
        public OnTraitChangeEventHandler OnTraitChangeAction;

        public ActiveTraitInfo(TraitData _traitData)
        {
            traitData = _traitData;
        }

        public void AddDefender(Defender defender)
        {
            int lastSynergyLevel = synergyLevel;
            int lastActiveNumber = activeNumber;
            
            //# 1. Defender 추가
            defenders.Add(defender);
            traitAdapters.Add(defender.traitAdapter);

            if (!activeDefenderDatas.Contains(defender.defenderData))
            {
                activeDefenderDatas.Add(defender.defenderData);
                activeNumber = activeDefenderDatas.Count;
            }
            
            //# 2. 어뎁터 업데이트
            int flag = CalcSynergyLevel();

            InvokeOnAddEventToAdapter(flag , defender.traitAdapter,traitData.TraitType);

            
            if(lastSynergyLevel != synergyLevel || activeNumber != lastActiveNumber) OnTraitChangeAction?.Invoke(this);
        }

        
        /// <summary>
        /// Defender 삭제
        /// </summary>
        /// <param name="defender"></param>
        /// <returns>활성화 Defender가 0인경우 ture</returns>
        public bool Remove(Defender defender)
        {
            int lastSynergyLevel = synergyLevel;
            int lastActiveNumber = activeNumber;
            
            //# 1. Defender 삭제
            defenders.Remove(defender);
            traitAdapters.Remove(defender.traitAdapter);
            
            //# 2. ActiveTraitInfo 업데이트
            if (!defenders.HaveConditions((activeDefender => activeDefender.defenderData.id == defender.defenderData.id)))
            {
                activeDefenderDatas.Remove(defender.defenderData);
                activeNumber = activeDefenderDatas.Count;
            }
            
            //# 3. Adapter 이벤트 액션
            int flag = CalcSynergyLevel();
            InvokeOnRemoveEventToAdapter(flag , traitData.TraitType);

            //# 4. 제거되는 Defender 시너지 끄기
            if(defender.traitAdapter.isSynergyActive[(int)traitData.TraitType])
                defender.traitAdapter.DeactiveSynergy(traitData.TraitType);
            
            if(lastSynergyLevel != synergyLevel || activeNumber != lastActiveNumber) OnTraitChangeAction?.Invoke(this);

            if (activeNumber == 0) return true;
            else return false;
        }

        void InvokeOnAddEventToAdapter(int flag , TraitAdapter newAdapter , TraitType traitType)
        {
            int traitIndex = (int)traitType;
           
            
            switch (flag)
            {
                case -1 : OnSynergyOff(); break;
                case 0 : OnSynergyNotChanged(); break;
                case 1 : OnSynergyOn(); break;
                case 2 : OnSynergyLevelChange(synergyLevel); break;
            }

            //#. 시너지 변경시 같은 시너지를 가진 모든 Defender에게 이벤트
            void OnSynergyOn()
            {
                foreach (var traitAdapter in traitAdapters)
                {
                    traitAdapter.ActiveSynergy(this , traitType);
                }
            }

            void OnSynergyOff()
            {
                foreach (var traitAdapter in traitAdapters)
                {
                    if(traitAdapter.isSynergyActive[traitIndex])
                        traitAdapter.DeactiveSynergy(traitData.TraitType);
                }
            }
            
            void OnSynergyLevelChange(int level)
            {
                foreach (var traitAdapter in traitAdapters)
                {
                    if(traitAdapter.isSynergyActive[traitIndex])
                        traitAdapter.OnSynergyLevelChange(level, traitData.TraitType);
                    else
                        traitAdapter.ActiveSynergy(this , traitType);
                }
            }

            void OnSynergyNotChanged()
            {
                if(synergyLevel > 0 && !newAdapter.isSynergyActive[traitIndex])
                    newAdapter.ActiveSynergy(this , traitType);
            }
        }

        void InvokeOnRemoveEventToAdapter(int flag , TraitType traitType)
        {
            int traitIndex = (int)traitType;
            
            switch (flag)
            {
                case -1 : OnSynergyOff(); break;
                case 0 : break;
                case 1 : OnSynergyOn(); break;
                case 2 : OnSynergyLevelChange(synergyLevel); break;
            }

            //#. 시너지 변경시 같은 시너지를 가진 모든 Defender에게 이벤트
            void OnSynergyOn()
            {
                foreach (var traitAdapter in traitAdapters)
                {
                    traitAdapter.ActiveSynergy(this , traitType);
                }
            }

            void OnSynergyOff()
            {
                foreach (var traitAdapter in traitAdapters)
                {
                    if(traitAdapter.isSynergyActive[traitIndex])
                        traitAdapter.DeactiveSynergy(traitData.TraitType);
                }
            }
            
            void OnSynergyLevelChange(int level)
            {
                foreach (var traitAdapter in traitAdapters)
                {
                    if(traitAdapter.isSynergyActive[traitIndex])
                        traitAdapter.OnSynergyLevelChange(level , traitData.TraitType);
                    else
                        traitAdapter.ActiveSynergy(this , traitType);
                }
            }
        }

        
        public void Reset()
        {
            defenders.Clear();
            activeDefenderDatas = new List<DefenderData>();
            activeNumber = 0;
            OnTraitChangeAction = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 시너지 소멸 : -1 <br> </br>
        /// 시너지 유지 : 0 <br> </br>
        /// 시너지 생성 : 1 <br> </br>
        /// 시너지 변화 : 2 <br> </br>
        /// </returns>
        int CalcSynergyLevel()
        {
            int lastLevel = synergyLevel;
            for (var i = traitData.SynergyTrigger.Length - 1; i >= 0; i--)
            {
                if (activeNumber >= traitData.SynergyTrigger[i])
                {
                    synergyLevel = i;
                    return CheckChanges();
                }
            }
            
            synergyLevel = 0;
            return CheckChanges();

            int CheckChanges()
            {
                if (lastLevel == 0)
                {
                    if (lastLevel < synergyLevel) return 1; // #. 0 -> n : 생성
                    else return 0; //#. 0 -> 0 : 유지
                }
                else if (synergyLevel == 0)
                {
                    return -1; //#. n -> 0 : 소멸
                }
                else
                {
                    if (lastLevel == synergyLevel) return 0; //#. n -> n : 유지
                    else return 2; //#. n -> m : 변화
                }
            }
        }
    }
}


