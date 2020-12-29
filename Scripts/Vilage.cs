using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilage : MonoBehaviour
{
    [SerializeField] private int Population;

    [SerializeField] private int Food;
    [SerializeField] private int Medecine;
    [SerializeField] private int Gold;


    [SerializeField] private float FoodFocus;
    [SerializeField] private float MedecineFocus;
    [SerializeField] private float GoldFocus;

    [SerializeField] private bool IsInNeedOfMMedecine = false;

    [SerializeField] private bool HasToUseMedecine = false;

    // Start is called before the first frame update
    void Start()
    {
        DayNightCycle.Instance.OnDayPassed += Instance_OnDayPassed;
    }

    //All of this logic may be a bit unnecessary 
    private void Instance_OnDayPassed(object sender, System.EventArgs e)
    {
        ResourcesChange();
        if(HasToUseMedecine)
        {
            if(Medecine > (int)(Population * GameParameters.Instance.InjuryAndSicknessSeverity))
            {
                Medecine -= (int)(Population * GameParameters.Instance.InjuryAndSicknessSeverity);
                HasToUseMedecine = false;
            }
            else
            {
                IsInNeedOfMMedecine = true;
            }
        }
        PopulationChange();
        RebalanceFocus();
    }

    private void RebalanceFocus()
    {
        if ((int)(Population * GameParameters.Instance.FoodToPopulation) > Food)
        {
            FoodFocus += MedecineFocus / 2 + GoldFocus / 2;
            MedecineFocus /= 2;
            GoldFocus /= 2;
        }
        else
        {
            //Begging for a floating point exaption to happen; 
            float SpareFromFood = FoodFocus * 0.2f;
            FoodFocus *= 0.8f;
            GoldFocus += SpareFromFood * 0.8f;
            MedecineFocus += SpareFromFood * 0.2f;
        }
        if(IsInNeedOfMMedecine)
        {
            MedecineFocus += FoodFocus + GoldFocus;
            FoodFocus = 0f;
            GoldFocus = 0f;
        }
    }
    private void ResourcesChange()
    {
        Food += (int)(Population * FoodFocus * GameParameters.Instance.FoodGatherRate);
        Medecine += (int)(Population * MedecineFocus * GameParameters.Instance.MedecineGatherRate);
        Gold += (int)(Population * GoldFocus * GameParameters.Instance.GoldGatherRate);
    }

    private void PopulationChange()
    {
        if (Food <= 0)
        {
            Population -= (int)(Population * GameParameters.Instance.PopulationDecrease);
        }
        else if ((int)(Population * GameParameters.Instance.FoodToPopulation) < Food)
        {
            Food -= (int)(Population * GameParameters.Instance.FoodToPopulation);
            Population += (int)(Population * GameParameters.Instance.PopulationIncrease);
        }
        else
        {
            Food = 0;
            Population--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
