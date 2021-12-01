using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Access this one through its public static methods
    static GameManager current;

    [SerializeField]
    float waitTimeInDialog;

    PlayerController currentPlayer;
    Interactable hoveredInteractable;
    Interactable currentInteractable;
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
        //Dont take any input
        if (!takeInput)
        {
            return;
        }

        //Move to pos on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            currentPlayer.MoveToPosition();
        }

        //Move to interactable on right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            if (hoveredInteractable == null)
            {
                return;
            }

            if (hoveredInteractable.type != InteractableType.Inventory)
            {
                currentInteractable = hoveredInteractable;
                currentPlayer.MoveToInteractable(currentInteractable);
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

    public static void StartDialog(InteractableNpc npc)
    {
        current.StartCoroutine("StartDialogRoutine", npc);
    }

    IEnumerator StartDialogRoutine(InteractableNpc npc)
    {
        //Disable input
        takeInput = false;
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

        //Hide dispaly over heads and enable input
        currentPlayer.HideDescriptionText();
        npc.HideDialogText();
        takeInput = true;
    }
}
