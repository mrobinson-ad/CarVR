using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Door : MonoBehaviour
{
    public DoorType doorType;
    public float closedY, openY;
    public bool isOpen = false;
    public GameObject doorHandle;
    private Vector3 defaultScale;

    private InteractionController interactionController;

    private void Awake()
    {
        defaultScale = doorHandle.transform.localScale;
        interactionController = FindObjectOfType<InteractionController>();

        if (interactionController == null)
        {
            Debug.LogError("InteractionController not found in the scene.");
            return;
        }
        XRBaseInteractable interactable = doorHandle.GetComponent<XRBaseInteractable>();

        if (interactable == null)
        {
            Debug.LogError("XR Simple Interactable component not found on the door handle.");
            return;
        }

        interactable.selectExited.RemoveAllListeners();
        interactable.selectExited.AddListener((interaction) => OnHandleInteract());

        //interactable.hoverEntered.AddListener((interaction) => OnHoverEnter());
        //interactable.hoverExited.AddListener((interaction) => OnHoverExit());

    }

    private void OnHandleInteract()
    {
        if (interactionController != null)
        {
            interactionController.ToggleDoor(doorType);
        }
    }

   /* private void OnHoverEnter()
    {
        doorHandle.transform.localScale = defaultScale * 1.1f;
    }

    private void OnHoverExit()
    {
        doorHandle.transform.localScale = defaultScale;
    } */
}

