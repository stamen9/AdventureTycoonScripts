using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Request 
{
    public enum RequestType
    {
        Repair,
        Weapon,
        Armor,
        Special //What does special mean is beyonf me
    }
    public RequestType TypeInfo;
    public RequestTypeData Data;

    public bool AcceptedStatus = false;

    //Can have the logic for random request directly in the constructor?
    public Request(bool random = false)
    {
        if(random)
        {

        }else
        {
            TypeInfo = RequestType.Repair;
            //Data = new RequestTypeData(,10);
        }
    }
    public Request(RequestType type, RequestTypeData data)
    {
        TypeInfo = type;
        Data = data;
        AcceptedStatus = false;
    }
}

[System.Serializable]
public class RequestTypeData
{
    public List<Player.InventorySlot> Requirements;
    public int difficulty;//Maybe should be calculated based on the items used??
    public int GoldReward;
    public RequestTypeData(List<Player.InventorySlot> items, int gold)
    {
        Requirements = items;
        foreach(Player.InventorySlot item in Requirements)
        {
            difficulty += item.item.Value * item.quantity;
        }
        GoldReward = gold;
    }
}

