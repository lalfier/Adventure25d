using UnityEngine;

public class InteractableHotspot : Interactable
{
    [SerializeField]
    SOHotspotData hotspotData;      //Hotspot data from scriptable object

    void Awake()
    {
        //Set name from SO
        interactableName = hotspotData.hotspotName;
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
            if(GameManager.GetSelectedInteractableItem() == null)
            {
                GameManager.ReadDescription(this);
            }
            else
            {
                GameManager.CombineInteractables(hotspotData.combineItemName);
            }            
        }
    }

    public SOHotspotData GetHotspotData()
    {
        return hotspotData;
    }
}
