using UnityEngine;
using UnityEngine.UI;

public class InteractableNpc : Interactable
{
    [SerializeField]
    Text dialogText;       //Display dialog text
    [SerializeField]
    SONpcData npcData;      //Npc data from scriptable object

    void Awake()
    {
        //Set name from SO
        interactableName = npcData.npcName;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        StartActionOnPlayer(other);
    }

    protected override void OnTriggerStay(Collider other)
    {
        StartActionOnPlayer(other);
    }

    void StartActionOnPlayer(Collider player)
    {
        //If clicked interactable is same as this stop player, reset interactable and start action
        if (GameManager.GetCurrentInteractable() == this)
        {
            player.GetComponent<PlayerController>().StopPlayer();
            GameManager.ResetCurrentInteractable();
            OnMouseExit();
            GameManager.StartDialog(this);
        }
    }

    public SONpcData GetNpcData()
    {
        return npcData;
    }

    public void SetDialogText(string text)
    {
        //Set dialog text over npc head
        dialogText.text = text;
        dialogText.gameObject.SetActive(true);
    }

    public void HideDialogText()
    {
        //Hide dialog text over npc head
        dialogText.gameObject.SetActive(false);
    }
}
