using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop Table", menuName = "Scriptable Objects/New Drop Table", order = 1)]
public class DropTable : ScriptableObject
{
    [Serializable]
    public struct ItemProbabilityPair 
    {
        public string itemKey;
        public int probability;
    }

    public List<ItemProbabilityPair> Table;

    private int CumulativeProbability;

    private void Awake()
    {
        foreach (ItemProbabilityPair pair in Table)
        {
            CumulativeProbability += pair.probability;
        }
    }

    public string DrawFromTable()
    {
        int hit = UnityEngine.Random.Range(0, CumulativeProbability);

        foreach(ItemProbabilityPair pair in Table)
        {
            //is it possible to get stuck at 1? think not. idk its 2am.
            hit -= pair.probability;
            if(hit <= 0)
            {
                return pair.itemKey;
            }
        }

        return null;
    }
}
