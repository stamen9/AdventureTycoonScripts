using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Building
{
    public List<Request> RequestList;

    private string[] BasicOreKeys = new string[] { "ironOre", "copperOre" };

    //public int test; 

    public bool PushRequest(Request RequestToAdd)
    {
        if(RequestList.Count < 16)
        {
            RequestList.Add(RequestToAdd);
            //need to instantiate Request holder her maybe?

            return true;
        }
        return false;
    }

    public Item GetRandomBasicOre()
    {
        int randomIndex = UnityEngine.Random.Range(0, BasicOreKeys.Length);
        return ItemDatabase.Instance.Database._myDictionary[BasicOreKeys[randomIndex]];
    }
    public bool PopRequest(Request RequestToRemove)
    {
        if(RequestList.Remove(RequestToRemove))
        {
            return true;
        }
        Debug.Log("Faield to find request to remove in collection");
        return false;
    }

    void OnMouseDown()
    {
        if (GeneralUIHelper.IsUIOpen())
        {
            return;
        }
        SoundManager.Instance.PlaySound("DoorSound");
        this.GetComponent<BlacksmithUIHelper>().ShowPanel();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
