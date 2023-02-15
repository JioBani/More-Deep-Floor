using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LNK.MoreDeepFloor.InGame.Ui.Market
{
    public class ProbabilityPanel : MonoBehaviour
    {
        private MarketManager marketManager;
        private double[] probabilities;

        [SerializeField] private Text[] probabilityTexts;

        private void Awake()
        {
            marketManager = ReferenceManager.instance.marketManager;
            marketManager.onInitLevelAction += OnLevelUp;
            marketManager.OnLevelUpAction += OnLevelUp;
        }

        // Start is called before the first frame update

        void OnLevelUp(int level)
        {
            probabilities = marketManager.GetProbabilities().values;
            for (int i = 0; i < 5; i++)
            {
                probabilityTexts[i].text = probabilities[i] + "%";
            }
        }
    }
}


