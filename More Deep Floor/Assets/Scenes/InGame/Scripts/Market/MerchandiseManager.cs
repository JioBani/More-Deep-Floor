using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Schemas;
using LNK.MoreDeepFloor.InGame.DataSchema;
using LNK.MoreDeepFloor.InGame.Entitys.Defenders;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.MarketSystem
{
    public class MerchandiseInfo
    {
        private DefenderDataTable defenderDataTable;
        public CostProbability[] costProbabilities =
        {
            new CostProbability(new double[]{ 75, 25, 0, 0, 0 }) ,
            new CostProbability(new double[]{ 75, 25, 0, 0, 0 }) ,
            new CostProbability(new double[]{ 75, 25, 0, 0, 0 }) ,
            new CostProbability(new double[]{ 75, 25, 0, 0, 0 }) ,
            new CostProbability(new double[]{ 55, 30, 15, 0, 0 }) ,
            new CostProbability(new double[]{ 45, 33, 20, 2, 0 }) ,
            new CostProbability(new double[]{ 25, 40, 30, 5, 0 }) ,
            new CostProbability(new double[]{ 19, 20, 35, 25, 4 }) ,
            new CostProbability(new double[]{ 10, 15, 30, 35, 10 }) ,
        };

        public MerchandiseInfo(DefenderDataTable defenderDataTable)
        {
            this.defenderDataTable = defenderDataTable;
        }

        public int SelectCost(int level)
        {
            int value = Random.Range(1, 101);
            double trigger = 0;
            double[] costProbability = GetProbability(level).values;
            
            for (int i = 0; i < costProbability.Length; i++)
            {
                trigger += costProbability[i];
                if (value <= trigger) return i + 1;
            }

            return 0;
        }

        public CostProbability GetProbability(int level)
        {
            if (level < 8)
            {
                return costProbabilities[level];
            }
            else
            {
                return costProbabilities[8];
            } 
        }

        public DefenderData GetDefender(int level)
        {
            int cost = SelectCost(level);
            List<DefenderData> list = defenderDataTable.defenderSortByCost[cost];
            return list[Random.Range(0, list.Count)];
        }
    }

    public struct CostProbability
    {
        public double[] values;

        public CostProbability(double[] _costProbabilities)
        {
            values = _costProbabilities;
        }
    }
}


