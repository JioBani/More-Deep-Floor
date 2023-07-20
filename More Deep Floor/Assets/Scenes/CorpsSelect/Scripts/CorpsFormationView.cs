using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class CorpsFormationView : MonoBehaviour
    {
        private CorpsData[] selectedCorps = new CorpsData[4];
        [SerializeField] private CorpsSelectSlot[] corpsSelectSlots;

        public void SetCorps(int slotIndex , CorpsData corpsData)
        {
            corpsSelectSlots[slotIndex].SetCorpsData(corpsData);
        }
    }
}


