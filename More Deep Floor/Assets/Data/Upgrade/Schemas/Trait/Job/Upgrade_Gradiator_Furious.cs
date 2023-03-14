using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders.States;
using LNK.MoreDeepFloor.InGame.MonsterDetectors;
using LNK.MoreDeepFloor.InGame.Upgrade;
using UnityEngine;
using UnityEngine.UIElements;

namespace LNK.MoreDeepFloor.Data.Upgrades
{
    //#. 공격시 대상 주변에 데미지
    public class Upgrade_Gladiator_Furious : UpgradeData
    {
        public override void OnAddAction(UpgradeManager upgradeManager)
        {
            //upgradeManager.
        }
    }


    namespace LNK.MoreDeepFloor.Data.Defenders.States.Schemas //.
    {
        [CreateAssetMenu(
            fileName = "Name",
            menuName = "Scriptable Object/Defender State Data/Trait/Job/Name",
            //menuName = "Scriptable Object/Defender State Data/Trait/Character/Name", 
            order = int.MaxValue)]

        public class StateData_Furious : DefenderStateData
        {
            public override DefenderState GetState(Defender defender)
            {
                return new Furious(this, defender);
            }
        }

        public class Furious : DefenderState
        {
            public Furious(DefenderStateData _stateData, Defender _defender) : base(_stateData, _defender)
            {
                
            }

            public override void OnTargetHitAction(Defender caster, Monster target, int damage)
            {
                List<Monster> targets = MonsterDetector.Circle(target.transform.position,2);
                
                /*for (var i = 0; i < targets.Count; i++)
                {
                    if(!ReferenceEquals(target , targets[i]))
                        //targets[i].Set("Furious" , damage);
                }*/
            }
        }
    }
}


