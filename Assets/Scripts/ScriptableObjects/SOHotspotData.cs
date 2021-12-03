using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Hotspot")]
public class SOHotspotData : ScriptableObject
{
    public string hotspotName;
    [TextArea(3, 20)]
    public string description;
    public string combineItemName;
    public AudioClip stingSound;
}
