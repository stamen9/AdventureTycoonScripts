using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot
{
    public GameObject Slot;
    public Item ShowItem;
    public ItemSlot(GameObject _Slot)
    {
        Slot = _Slot;
    }

    public void SetItem(Item ItemToShow, int quantity)
    {
        ShowItem = ItemToShow;
        Slot.GetComponentsInChildren<Image>()[1].sprite = ShowItem.Icon;
        Slot.GetComponentInChildren<TMPro.TMP_Text>().text = quantity.ToString();

    }

    public void ClearItem()
    {
        ShowItem = null;
        Slot.GetComponentsInChildren<Image>()[1].sprite = null;
        Slot.GetComponentInChildren<TMPro.TMP_Text>().text = "";
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
