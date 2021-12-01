using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryButton;
    [SerializeField]
    GameObject inventoryPanel;

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
}
