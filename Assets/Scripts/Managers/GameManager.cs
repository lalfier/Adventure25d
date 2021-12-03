using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Access this one through its public static methods
    static GameManager current;

    [SerializeField]
    SOInventoryData inventory;      //Player inventory
    [SerializeField]
    float waitTimeInDialog;         //Wait between dialog lines (seconds)
    [SerializeField]
    float waitTimeInDescrption;     //Wait after description is shown (seconds)
    [SerializeField]
    float waitTimeInCombine;        //Wait after combine is shown (seconds)

    PlayerController currentPlayer;
    Interactable hoveredInteractable;
    Interactable currentInteractable;
    InteractableItem selectedInteractableItem;
    bool takeInput = true;

    void Awake()
    {
        //If an GameManager exists and it is not this...
        if (current != null && current != this)
        {
            //...destroy this. There can be only one GameManager
            Destroy(gameObject);
            return;
        }

        //This is the current GameManager and it should persist between scene loads
        current = this;
        DontDestroyOnLoad(gameObject);

        //Subscribe
        Interactable.OnInteracted += SetHoveredInteractable;
    }

    void Update()
    {
        //Do not take any input
        if (!takeInput)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (hoveredInteractable != null && hoveredInteractable.type == InteractableType.Item)
            {
                //Set selected inventory item and mouse cursor icon
                selectedInteractableItem = (InteractableItem)hoveredInteractable;
                Cursor.SetCursor(selectedInteractableItem.GetItemData().dragIcon, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                //Move to pos on left mouse click
                currentPlayer.MoveToPosition();
            }            
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if (hoveredInteractable == null)
            {
                return;
            }

            if (hoveredInteractable.type != InteractableType.Item)
            {
                //Move to interactable on right mouse click
                currentInteractable = hoveredInteractable;
                currentPlayer.MoveToInteractable(currentInteractable);
            }
            else
            {
                //If right click on inventory item
                if(selectedInteractableItem == null)
                {
                    //Read description
                    ReadDescription((InteractableItem)hoveredInteractable);
                }
                else
                {
                    //Combine items
                    InteractableItem hoveredItem = (InteractableItem)hoveredInteractable;
                    CombineInteractables(hoveredItem.GetItemData().combineItemName);
                }
            }
        }
    }

    void SetHoveredInteractable(Interactable interactable)
    {
        //Set interactable from event
        hoveredInteractable = interactable;
    }

    public static void SetPlayer(PlayerController player)
    {
        //Set player reference
        current.currentPlayer = player;
    }

    public static bool TakeInputEnabled()
    {
        return current.takeInput;
    }

    public static Interactable GetCurrentInteractable()
    {
        //Return current interactable
        return current.currentInteractable;
    }

    public static void ResetCurrentInteractable()
    {
        //Set current interactable to null
        current.currentInteractable = null;
    }

    public static InteractableItem GetSelectedInteractableItem()
    {
        //Return selected interactable item
        return current.selectedInteractableItem;
    }

    public static void ResetSelectedInteractableItem()
    {
        //Set selected interactable item to null and reset mouse cursor
        current.selectedInteractableItem = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public static SOInventoryData GetInventory()
    {
        //Return inventory
        return current.inventory;
    }

    public static void StartDialog(InteractableNpc npc)
    {
        current.StartCoroutine("StartDialogRoutine", npc);
    }

    IEnumerator StartDialogRoutine(InteractableNpc npc)
    {
        //Disable input
        takeInput = false;
        AudioManager.PlayStingAudio(npc.GetNpcData().stingSound);
        yield return new WaitForSeconds(0.3f);

        //Go through dialog array
        for (int i = 0; i < npc.GetNpcData().dialog.Length; i++)
        {
            //If player is first than he is even number else is odd
            if(npc.GetNpcData().playerStartsDialog)
            {
                if (i%2 == 0)
                {
                    currentPlayer.SetDescriptionText(npc.GetNpcData().dialog[i]);
                    npc.HideDialogText();
                }
                else
                {
                    currentPlayer.HideDescriptionText();
                    npc.SetDialogText(npc.GetNpcData().dialog[i]);
                }
            }
            else
            {
                if (i%2 == 0)
                {
                    currentPlayer.HideDescriptionText();
                    npc.SetDialogText(npc.GetNpcData().dialog[i]);
                }
                else
                {
                    currentPlayer.SetDescriptionText(npc.GetNpcData().dialog[i]);
                    npc.HideDialogText();
                }
            }
            yield return new WaitForSeconds(waitTimeInDialog);
        }

        //Hide display over heads and enable input
        currentPlayer.HideDescriptionText();
        npc.HideDialogText();
        takeInput = true;
    }

    public static void ReadDescription(InteractableHotspot hotspot)
    {
        current.StartCoroutine("ReadDescriptionRoutine", hotspot);
    }

    IEnumerator ReadDescriptionRoutine(InteractableHotspot hotspot)
    {
        //Disable input
        takeInput = false;
        AudioManager.PlayStingAudio(hotspot.GetHotspotData().stingSound);
        yield return new WaitForSeconds(0.3f);

        //Set description text over player head
        currentPlayer.SetDescriptionText(hotspot.GetHotspotData().description);
        yield return new WaitForSeconds(waitTimeInDescrption);

        //Hide display over head and enable input
        currentPlayer.HideDescriptionText();
        takeInput = true;
    }

    public static void ReadDescription(InteractableItem item)
    {
        current.StartCoroutine("ReadDescriptionRoutine", item);
    }

    IEnumerator ReadDescriptionRoutine(InteractableItem item)
    {
        //Disable input
        takeInput = false;
        AudioManager.PlayStingAudio(item.GetItemData().stingSound);
        yield return new WaitForSeconds(0.3f);

        //Set description text over player head
        currentPlayer.SetDescriptionText(item.GetItemData().description);
        yield return new WaitForSeconds(waitTimeInDescrption);

        //Hide display over head and enable input
        currentPlayer.HideDescriptionText();
        takeInput = true;
    }

    public static void CombineInteractables(string itemToCobine)
    {
        current.StartCoroutine("CombineInteractablesRoutine", itemToCobine);
    }

    IEnumerator CombineInteractablesRoutine(string itemToCobine)
    {
        //Disable input
        takeInput = false;
        AudioManager.PlayStingAudio(selectedInteractableItem.GetItemData().stingSound);
        yield return new WaitForSeconds(0.3f);
                
        if (itemToCobine.Equals(selectedInteractableItem.GetItemData().itemName))
        {
            //Set combine text over player head
            currentPlayer.SetDescriptionText(selectedInteractableItem.GetItemData().OnCombineSuccess);
            //Do logic for combine
        }
        else
        {
            currentPlayer.SetDescriptionText("I can't use this here!");
        }
        ResetSelectedInteractableItem();
        yield return new WaitForSeconds(waitTimeInCombine);

        //Hide display over head, reset selected item and enable input
        currentPlayer.HideDescriptionText();
        takeInput = true;
    }
}
