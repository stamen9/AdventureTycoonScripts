using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject SlotPrefab;

    public List<ItemSlot> SlotCollection = new List<ItemSlot>();

    public bool setNeedsUpdate = false;
    void Start()
    {
        SetUpInventorySlots();
        UpdateInventorySlots();
    }

    public void SetUpInventorySlots()
    {
        for (int i = 0; i < 40; i++)
        {
            GameObject NewSlotObject = Instantiate(SlotPrefab, this.transform);
            SlotCollection.Add(new ItemSlot(NewSlotObject));
        }
    }

    public void UpdateInventorySlots()
    { 
        int SlotId = 0;
        foreach(Player.InventorySlot item in Player.Instance.PlayerInventory)
        {
            if(SlotId >= SlotCollection.Count)
            {
                GameObject NewSlotObject = Instantiate(SlotPrefab, this.transform);
                SlotCollection.Add(new ItemSlot(NewSlotObject));
            }
            SlotCollection[SlotId++].SetItem(item.item,item.quantity);
            
        }
        for(int i = SlotId; i < SlotCollection.Count; i++)
        {
            SlotCollection[i].ClearItem();
        }
    }

    public void InventoryNeedsUpdate()
    {
        if(gameObject.activeSelf)
        {
            UpdateInventorySlots();
        }
        else
        {
            setNeedsUpdate = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
