using UnityEngine;

public class InteractableItem : Interactable
{
    [SerializeField]
    SOItemData itemData;      //Hotspot data from scriptable object

    void Awake()
    {
        //Set name from SO
        interactableName = itemData.itemName;
    }

    public SOItemData GetItemData()
    {
        return itemData;
    }
}
