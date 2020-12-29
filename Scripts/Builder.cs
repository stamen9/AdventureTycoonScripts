using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Builder : MonoBehaviour
{
    [System.Serializable]
    public class BuildingAndCost
    {
        public GameObject building;
        public int goldCost;
        public bool unlocked;

        public BuildingAndCost(GameObject building, int cost, bool v)
        {
            this.building = building;
            this.goldCost = cost;
            this.unlocked = v;
        }
    }

    
    [SerializeField] private GameObject BuildingSelectPanel;
    [SerializeField] private GameObject BuildingSlotPrefab;
    [SerializeField] private List<BuildingAndCost> Buildings;

    public void BuildBuilding(GameObject Location, BuildingAndCost Building)
    {
        int pos = Buildings.BinarySearch(Building);
        if (pos < 0)
        {
            Debug.LogWarning("Building can't be found in internal collection.");
            return;
        }
            
        if (Player.Instance.TakeGold(Building.goldCost))
        {
            Instantiate(Building.building, Location.transform.position, Location.transform.rotation);
            Buildings.Remove(Building);
            Destroy(Location);
        }
        else
        {
            Debug.Log("Insuficient player gold!");
        }

    }

    /*public void PushNewBuilding(GameObject Building, int cost)
    {
        BuildingAndCost NewBuilding = new BuildingAndCost(Building, cost, false);
        Buildings.Add(NewBuilding);
    }*/

    public void AddBuilding(BuildingAndCost building)
    {
        Buildings.Add(building);
    }

    private void UpdateBuildingPanel(GameObject Location)
    {
        foreach(BuildingAndCost AvailableBuilding in Buildings)
        {
            GameObject instance = Instantiate(BuildingSlotPrefab, BuildingSelectPanel.transform);
            BuildingSlot buildingSlotInstance = instance.GetComponent<BuildingSlot>();
            buildingSlotInstance.building = AvailableBuilding;
            buildingSlotInstance.UpdateAll(this,Location);
        }
    }

    private bool CheckForCost(BuildingAndCost cost)
    {
        if(Player.Instance.Gold > cost.goldCost)
        {
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
