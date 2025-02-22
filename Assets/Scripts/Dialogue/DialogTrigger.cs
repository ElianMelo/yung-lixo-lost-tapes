using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogData CurrentDialogData;
    private Collider dialogCollider;

    private void Start()
    {
        dialogCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (PauseMananger.Instance.CurrentState == GamePauseState.Talking) return;

            if (!InterfaceSystem.Instance.OpenedMenu && !InterfaceSystem.Instance.OpenedSettings)
            {
                PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Paused);
                InterfaceSystem.Instance.OpenMenu();
            }
            else
            {
                PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Walking);
                InterfaceSystem.Instance.CloseMenu();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InterfaceSystem.Instance.SetDialogData(CurrentDialogData);
            InterfaceSystem.Instance.InitDialog();
            dialogCollider.enabled = false;
        }
    }
}
