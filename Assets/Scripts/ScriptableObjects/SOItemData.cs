using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Item")]
public class SOItemData : ScriptableObject
{
    public string itemName;
    [TextArea(3, 20)]
    public string description;
    public string combineItemName;
    [TextArea(3, 20)]
    public string OnCombineSuccess;
    public AudioClip stingSound;
    public InteractableItem inventoryPrefab;
    public Texture2D dragIcon;
}
