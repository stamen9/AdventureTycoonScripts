using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters Instance;

    public float PopulationIncrease;
    public float PopulationDecrease;
    public float FoodToPopulation;

    public float FoodGatherRate;
    public float GoldGatherRate;
    public float MedecineGatherRate;

    public float InjuryAndSicknessSeverity;

    public float AdventurerRepairTreshold = 0.75f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
