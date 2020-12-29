using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    public Builder.BuildingAndCost building;

    public Image BuildingImage;
    public TMPro.TMP_Text GoldText;
    public TMPro.TMP_Text BuildingNameText;

    public Button AttemptConstructionButton;

    [SerializeField]private Builder BuilderRef;

    private GameObject CallerLocation; 

    public void UpdateGoldCost()
    {
        GoldText.text = building.goldCost.ToString() + "g";
        if (Player.Instance.hasGold(building.goldCost))
        {
            GoldText.color = Color.white;
        }else
        {
            GoldText.color = Color.red;
        }
    }
    public void UpdateImage()
    {
        if(building != null)
        BuildingImage.sprite = building.building.GetComponent<SpriteRenderer>().sprite;
    }
    public void UpdateName()
    {
        //Not sure if this will work as expected
        BuildingNameText.text = building.building.name;
    }
    // Start is called before the first frame update
    public void UpdateAll(Builder builder, GameObject Location)
    {
        BuilderRef = builder;
        CallerLocation = Location;

        UpdateName();
        UpdateImage();
        UpdateGoldCost();
    }


    public void CallToAttemptBuild()
    {
        BuilderRef.BuildBuilding(CallerLocation, building);
    }

    public void PushToBuilder()
    {
        BuilderRef.AddBuilding(building);
    }
    void Start()
    {
        UpdateImage();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
