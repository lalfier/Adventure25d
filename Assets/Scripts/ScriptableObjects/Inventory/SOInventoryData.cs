using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory")]
public class SOInventoryData : ScriptableObject
{
    public List<InventorySlot> inventoryList = new List<InventorySlot>();   //List of all items in inventory

    public void AddItem(SOItemData itemData)
    {
        //Check is there same item in inventory
        bool hasItem = false;
        for (int i = 0; i < inventoryList.Capacity; i++)
        {
            if(inventoryList[i].item == itemData)
            {
                hasItem = true;
                break;
            }
        }

        //If not add it
        if (!hasItem)
        {
            inventoryList.Add(new InventorySlot(itemData));
        }
    }
}

[Serializable]
public class InventorySlot
{
    public SOItemData item;

    public InventorySlot(SOItemData itemData)
    {
        item = itemData;
    }
}
