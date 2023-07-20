using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class CorpsSelectSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI commanderName;
        [SerializeField] private TextMeshProUGUI corpsName;
        [SerializeField] private Image commanderImage;

        private CorpsData corpsData;
        
        
        public void SetCorpsData(CorpsData _corpsData)
        {
            corpsData = _corpsData;
            commanderName.text = corpsData.CommanderName;
            corpsName.text = corpsData.CorpsName;
            commanderImage.sprite = corpsData.CommanderImage;
        }
    }
}


