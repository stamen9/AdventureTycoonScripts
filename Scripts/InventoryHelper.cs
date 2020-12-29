using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelper : MonoBehaviour
{
    public static InventoryHelper Instance;

    private Inventory inv;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            inv = InventoryPanel.GetComponentInChildren<Inventory>();
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject InventoryPanel;
    public void OpenInventory()
    {
        if(inv.setNeedsUpdate)
        {
            inv.UpdateInventorySlots();
        }
        InventoryPanel.SetActive(true);
    }
    public void CloseInventory()
    {
        InventoryPanel.SetActive(false);
    }

    public void InventoryNeedsUpdate()
    {
        inv.InventoryNeedsUpdate();
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
