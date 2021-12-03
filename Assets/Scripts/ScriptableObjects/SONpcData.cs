using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Npc")]
public class SONpcData  : ScriptableObject
{
    public string npcName;
    [TextArea(3,20)]
    public string[] dialog;
    public bool playerStartsDialog;
    public string combineItemName;
    public AudioClip stingSound;
}
