using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Npc")]
public class SONpcData  : ScriptableObject
{
    public string npcName;
    public string[] dialog;
    public bool playerStartsDialog;
    public string combineItemName;
    public AudioClip stingSound;
}
