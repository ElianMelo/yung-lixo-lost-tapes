using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class CannonTrigger : MonoBehaviour
{
    public GameObject interactCanvas;
    public SplineComputer splineComputer;
    public Transform model1;
    public Transform model2;
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
        Sequence sequence = DOTween.Sequence();
        sequence.Append(model1.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.Linear));
        sequence.Append(model2.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.Linear));
        sequence.Append(model1.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f).SetEase(Ease.Linear));
        sequence.Append(model2.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f).SetEase(Ease.Linear));
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
