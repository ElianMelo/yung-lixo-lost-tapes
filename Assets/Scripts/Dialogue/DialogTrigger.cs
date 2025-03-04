using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogData CurrentDialogData;
    public GameObject interactCanvas;

    private bool canInteract = false;

    private void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            interactCanvas.SetActive(false);
            canInteract = false; 
            StartDialog();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (PauseMananger.Instance.CurrentState == GamePauseState.Talking) return;

            if (InterfaceSystem.Instance.currentMenuState == MenuStates.Disabled)
            {
                PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Paused);
                InterfaceSystem.Instance.OpenAlbumMenu();
            }
            else
            {
                PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Walking);
                InterfaceSystem.Instance.CloseAlbumMenu();
            }
        }
    }

    private void StartDialog()
    {
        InterfaceSystem.Instance.SetDialogData(CurrentDialogData);
        InterfaceSystem.Instance.InitDialog();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactCanvas.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(false);
            canInteract = false;
        }
    }
}
