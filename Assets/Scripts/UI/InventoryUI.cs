using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryButton; //Button to show inventory panel
    [SerializeField]
    GameObject inventoryPanel;  //Place to display inventory items

    Dictionary<InventorySlot, InteractableItem> itemsDisplayed = new Dictionary<InventorySlot, InteractableItem>();

    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void ShowInventoryPanel()
    {
        inventoryButton.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        inventoryButton.SetActive(true);
    }

    void CreateDisplay()
    {
        for (int i = 0; i < GameManager.GetInventory().inventoryList.Count; i++)
        {
            CreateInventoryItem(i);
        }
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < GameManager.GetInventory().inventoryList.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(GameManager.GetInventory().inventoryList[i]))
            {
                CreateInventoryItem(i);
            }
        }
    }

    void CreateInventoryItem(int index)
    {
        //Create inventory item from scriptable object and show it on inventory panel
        InteractableItem invItem = Instantiate(GameManager.GetInventory().inventoryList[index].item.inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        invItem.transform.SetParent(inventoryPanel.transform);
        itemsDisplayed.Add(GameManager.GetInventory().inventoryList[index], invItem);
    }
}
