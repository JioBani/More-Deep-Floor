using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Entity;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using UnityEditor;
using UnityEngine;


namespace LNK.MoreDeepFloor.Data.Traits.Corps
{
    [Serializable]
    public struct GenerateInfo
    {
        public string name;
        public int cost;
    }
    
    [CreateAssetMenu(
        fileName = "Defender So Generator",
        menuName = "Scriptable Object/Entity/Defender So Generator",
        order = int.MaxValue)]
    public class DefenderSoGenerator : ScriptableObject
    {
        public List<GenerateInfo> defenders;
        public string corpsName;
        public EntityStatus status;

        public void GenerateDefenders()
        {
            foreach (var generateInfo in defenders)
            {
                Generate(generateInfo);
            }
        }

        void Generate(GenerateInfo info)
        {
            DefenderOriginalData asset = ScriptableObject.CreateInstance<DefenderOriginalData>();
            AssetDatabase.CreateAsset(asset, $"Assets/Data/Traits/Corps/{corpsName}/{corpsName}_{info.cost}_{info.name}.asset");
            asset.SetInfo(corpsName , info);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

    }
}
