using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene.FormationModifyViews
{
    public class StatusText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public void SetStatus(float[] values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("");
            
            for (var i = 1; i < values.Length; i++)
            {
                stringBuilder.Append(values[i]);
                if(i != values.Length - 1)
                    stringBuilder.Append(" / ");
            }

            text.text = stringBuilder.ToString();
        }
    }
}


