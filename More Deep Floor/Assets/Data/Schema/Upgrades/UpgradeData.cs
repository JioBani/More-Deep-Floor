using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.Upgrade;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Object/Upgrade", order = int.MaxValue)]
    public class UpgradeData : ScriptableObject
    {
        [SerializeField] private string upgradeName;
        public string UpgradeName => upgradeName;

        [SerializeField] private string description;
        public string Description => description;

        [SerializeField] private Sprite image;
        public Sprite Image => image;


        /// <summary>
        /// 업그레이드 선택시 발동
        /// </summary>
        public virtual void OnAddAction(UpgradeManager upgradeManager)
        {
            
        }
    }
}


