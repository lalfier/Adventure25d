using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speedNearBackground;  //Agent speed when near background (Z = 0)
    [SerializeField]
    LayerMask mouseLayer;       //Layer to register mouse clicks
    [SerializeField]
    Text descriptionText;       //Display description text(dialogs, items, ...)

    NavMeshAgent agent;
    Animator animator;
    Vector3 nextPosition = Vector3.zero;

    void Awake()
    {
        //Get components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        //Set reference to game manager
        GameManager.SetPlayer(this);
    }

    void Update()
    {
        //Change agent speed based on Z position
        agent.speed = (Mathf.Abs(transform.position.z / 10) + speedNearBackground);

        //Stop player when close to position
        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y - agent.baseOffset, transform.position.z), nextPosition) < agent.stoppingDistance*5 && animator.GetBool("isWalking"))
        {
            StopPlayer();
        }
    }

    public void MoveToPosition()
    {
        //If left mouse click hits ground start moving player to that pos
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseLayer))
        {
            GameManager.ResetCurrentInteractable();
            GameManager.ResetSelectedInteractableItem();
            agent.SetDestination(hit.point);
            nextPosition = hit.point;
            //Change animation state
            animator.SetBool("isWalking", true);
        }
    }

    public void MoveToInteractable(Interactable currentInteractable)
    {
        //If right mouse click hits interactable start moving player interactable pos
        agent.SetDestination(currentInteractable.transform.position);
        nextPosition = currentInteractable.transform.position;
        //Change animation state
        animator.SetBool("isWalking", true);
    }

    public void StopPlayer()
    {
        //Stop walking
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        //Change animation state
        animator.SetBool("isWalking", false);
    }

    public void SetDescriptionText(string text)
    {
        //Set description text over player head
        descriptionText.text = text;
        descriptionText.gameObject.SetActive(true);
    }

    public void HideDescriptionText()
    {
        //Hide description text over player head
        descriptionText.gameObject.SetActive(false);
    }
}
