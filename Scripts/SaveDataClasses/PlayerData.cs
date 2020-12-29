using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    [System.Serializable]
    public class InventorySlotData
    {
        public string item;
        public int quantity;

        //Go with string for items
        //It seems natural to directly sirealize scriptable objects but its not ok.
        //Unity is doing some behind the scenes stuff that makes the proces more tricky then it has to be
        public InventorySlotData(Player.InventorySlot slot)
        {
            item = slot.item.key;
            quantity = slot.quantity;
        }
    }
    
    public int gold;
    public int fame;
    public bool hasBlacksmith;

    public InventorySlotData[] inventory;

    public PlayerData(Player player)
    {
        gold = player.Gold;
        fame = player.Fame;
        hasBlacksmith = player.hasBlacksmith;

        inventory = new InventorySlotData[Player.Instance.PlayerInventory.Count];
        for(int i = 0 ; i < Player.Instance.PlayerInventory.Count; i++)
        {
            inventory[i] = new InventorySlotData(Player.Instance.PlayerInventory[i]);
        }
    }
}
