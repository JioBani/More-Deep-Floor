using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;
using UnityEditor;


namespace LNK.MoreDeepFloor.Data.DefenderTraits
{
    [CreateAssetMenu(fileName = "Trait Generator", menuName = "Scriptable Object/Trait Generator", order = int.MaxValue)]

    public class TraitGenerator : ScriptableObject
    {
        public TraitId traitId;
        public TraitType traitType;
        public string traitName;
        
        public Parameter[] parameters;
        public DefenderStateId traitStateId;

        public void CreateMyAsset()
        {
            string traitAssetPath;
            string stateAssetPath;
            
            if (traitType == TraitType.Job)
            {
                traitAssetPath = $"Assets/Data/Traits/Job/{traitName}.asset";
                stateAssetPath = $"Assets/Data/DefenderStates/Trait/Job/{traitName}.asset";
            }
            else
            {
                traitAssetPath = $"Assets/Data/Traits/Character/{traitName}.asset";
                stateAssetPath = $"Assets/Data/DefenderStates/Trait/Character/{traitName}.asset";
            }
            
            var checkTraitAsset = AssetDatabase.LoadAssetAtPath(traitAssetPath, typeof(TraitData)) as TraitData;
            
            if (checkTraitAsset != null)
            {
                Debug.Log($"특성 데이터가 이미 존재합니다 : {traitName}");
                return;
            }
            
            var checkStateAsset = AssetDatabase.LoadAssetAtPath(traitAssetPath, typeof(TraitData)) as TraitData;
                        
            if (checkStateAsset != null)
            {
                Debug.Log($"특성 상태 데이터가 이미 존재합니다 : {traitName}");
                return;
            }
            
            //#. 특성 상태
            DefenderStateData traitStateData = CreateInstance<DefenderStateData>();
            string stateAssetName = AssetDatabase.GenerateUniqueAssetPath(stateAssetPath);
            AssetDatabase.CreateAsset(traitStateData, stateAssetName);
            traitStateData.SetStateName("Trait_" + traitName);
            if (parameters != null)
            {
                traitStateData.SetParameter(parameters);
            }
            traitStateData.SetTraitId(traitStateId);
            
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();

            //#. 특성 
            TraitData traitAsset = CreateInstance<TraitData>();
            string traitAssetName = AssetDatabase.GenerateUniqueAssetPath(traitAssetPath);
            AssetDatabase.CreateAsset(traitAsset, traitAssetName);
            traitAsset.SetTraitId(traitId);
            traitAsset.SetTraitName(traitName);
            
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
        
            Selection.activeObject = traitAsset;
        }
    }
}

