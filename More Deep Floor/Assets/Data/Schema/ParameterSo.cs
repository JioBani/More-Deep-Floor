using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LNK.MoreDeepFloor.Common.ReadOnlyInspector;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEditor;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Schemas
{
    public class ParameterSo : ScriptableObject
    {
        [SerializeField] protected string dataName;
        public string DataName => dataName;
        
        [TextArea][SerializeField] protected string description;
        public string Description => description;
            
        [TextArea , SerializeField]protected string descriptionShowing;
        public string DescriptionShowing => descriptionShowing;
        
        [SerializeField] private Parameter[] parameters;

        private Dictionary<string, float> Parameters;
        
        public void SetParameter(Parameter[] _parameters)
        {
            parameters = _parameters;
        }

        #region #. 에디터 함수


        public void SetParameters()
        {
            //EditorUtility.SetDirty(this);
            Parameters = new Dictionary<string, float>();
            
            for (int i = 0; i < parameters.Length; i++)
            {
                Parameters.Add(parameters[i].name , parameters[i].value);
            }
        }
        
        public bool GetParameter(string name , out float parameter)
        {
            if (Parameters == null)
                SetParameters();

            if(Parameters.TryGetValue(name , out float value))
            {
                parameter = value;
                return true;
            }
            else
            {
                parameter = 1;
                string str = "";
                str += $"[SkillData.GetParameter()] {dataName} : \"{name}\"매개변수를 가져올수 없습니다.\n";
                foreach (var keyValuePair in Parameters)
                {
                    str += keyValuePair.Key + ",";
                }
                Debug.LogError(str);
                return false;
            }
        }

        #endregion
    }
}

