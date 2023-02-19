using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.ProbabilityChecks
{
    public class ProbabilityCheck
    {
        public static bool Check(int targetNumber, int enterNumber)
        {
            if (Random.Range(1, enterNumber + 1) <= targetNumber) return true;
            else return false;
        }
    }
}


