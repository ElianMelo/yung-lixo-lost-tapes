using Dreamteck.Splines;
using UnityEngine;

public class CannonTrigger : MonoBehaviour
{
    public GameObject interactCanvas;
    public SplineComputer splineComputer;
    private PlayerMovementController playerMovementController;

    private bool canInteract = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            interactCanvas.SetActive(false);
            canInteract = false; 
            StartFlying();
        }
    }

    private void StartFlying()
    {
        playerMovementController.SetupSplineComputer(splineComputer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerMovementController = other.GetComponent<PlayerMovementController>();
            interactCanvas.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovementController = null;
            interactCanvas.SetActive(false);
            canInteract = false;
        }
    }
}
