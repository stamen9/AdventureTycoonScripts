using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public event EventHandler PostValueChangeTrigger;

    public class PlayerEventArgs: EventArgs
    {
        public int GoldEventArg;
        public int FameEventArg;
        public PlayerEventArgs(int _gold, int _fame)
        {
            GoldEventArg = _gold;
            FameEventArg = _fame;
        }
    }

    private int gold;
    public int Gold {
        get { return gold; }
        set
        {
            gold = value;
            //should i have a class that inherits EventArgs to pass args or abuse the singleton?
            PostValueChangeTrigger?.Invoke(this, new PlayerEventArgs(Gold, Fame));
        }
    }

    //bool collection start
    public bool hasBlacksmith = false;
    //bool collection end

    private int fame;
    public int Fame
    {
        get { return fame; }
        set {
            fame = value;
            //should i have a class that inherits EventArgs to pass args or abuse the singleton?
            PostValueChangeTrigger?.Invoke(this,new PlayerEventArgs(Gold,Fame));
        } 
    }

    //Not sure if making all these classes serializable will work out :S
    [System.Serializable]
    public class InventorySlot
    {
        public Item item;
        public int quantity;

        public InventorySlot()
        {
        }

        public InventorySlot(Item it, int quant) : this()
        {
            item = it;
            quantity = quant;
        }

        public void AddQuantity(int quant)
        {
            quantity = quantity + quant;
            Debug.Log(item.Name + " quantity inc to " + quantity);
        }

        public bool SubtractQuantity(int quant)
        {
            //safty mesure?
            if(this.quantity < quant)
            {
                return false;
            }
            this.quantity -= quant;
            return true;
        }
    }

    //Should be its own class?
    //public List<Tuple<Item, int>> Inventory = new List<Tuple<Item, int>>();
    public List<InventorySlot> PlayerInventory = new List<InventorySlot>();


    public void CallSave()
    {
        SaveSystem.SaveAllData();
    }

    public void CallLoad()
    {
        SaveSystem.LoadAllData();
    }


    public void AddResources(Item item, int quant)
    {
        for(int i = 0; i < PlayerInventory.Count; i++)
        {
            if(PlayerInventory[i].item.Name == item.Name)
            {
                Debug.Log(PlayerInventory[i].item.Name + " == " + item.Name);
                PlayerInventory[i].AddQuantity(quant);
                return;
            }
            Debug.Log(PlayerInventory[i].item.Name + " != " + item.Name);
        }
        
        InventorySlot newItem = new InventorySlot(item, quant);
        PlayerInventory.Add(newItem);
    }

    public bool CheckForResources(List<InventorySlot> RequestedItems)
    {
        bool isFound = false;
        foreach(InventorySlot Requested in RequestedItems)
        {
            foreach(InventorySlot InPosession in PlayerInventory)
            {
                if(Requested.item.Name == InPosession.item.Name)
                {
                    if(Requested.quantity <= InPosession.quantity)
                    {
                        isFound = true;
                        break;
                    }
                }
            }
            if(isFound)
            {
                isFound = false;
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public bool hasGold(int _gold)
    {
        if(Gold >= _gold)
        {
            return true;
        }
        return false;
    }
    public bool TakeGold(int goldToTake)
    {
        if(goldToTake >= Gold)
        {
            Gold -= goldToTake;
            return true;
        }
        return false;
    }


    public void TakeResources(List<InventorySlot> RequestedItems)
    {
        foreach (InventorySlot Requested in RequestedItems)
        {
            //foreach wont work as the enumurator can't change the underling value.
            for (int i = 0; i < PlayerInventory.Count; i++)
            {
                if (Requested.item.Name == PlayerInventory[i].item.Name)
                {

                    PlayerInventory[i].SubtractQuantity(Requested.quantity);
                    if(PlayerInventory[i].quantity == 0)
                    {
                        PlayerInventory.RemoveAt(i);
                    }
                    continue;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = 100;
        Fame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
