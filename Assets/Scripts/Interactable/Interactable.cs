using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum InteractableType
{
    None,
    Hotspot,
    Npc,
    Item
}

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InteractableType type;   //Type of interactable
    public Text nameText;           //Display name

    //Event
    public delegate void Interact(Interactable item);
    public static event Interact OnInteracted;

    protected string interactableName;

    protected void OnMouseEnter()
    {
        //On start mouse hover over interactable object
        if (!GameManager.TakeInputEnabled())
        {
            return;
        }

        if (OnInteracted != null)
        {
            //Send event with this interactable
            OnInteracted(this);
            nameText.text = interactableName;
            nameText.gameObject.SetActive(true);
        }
    }

    protected void OnMouseExit()
    {
        //On exit mouse hover over interactable object
        if (OnInteracted != null)
        {
            //Send event with null
            OnInteracted(null);
            nameText.gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {        
    }

    protected virtual void OnTriggerStay(Collider other)
    {
    }

    //For UI
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter();
    }

    //For UI
    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit();
    }
}
