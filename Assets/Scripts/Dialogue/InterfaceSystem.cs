using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceSystem : MonoBehaviour
{
    [SerializeField]
    private MusicTapeController musicTapeController;

    public static InterfaceSystem Instance;

    public DialogManager dialogManager;
    public MenuManager menuManager;
    public settingsManager settingsManager;

    private DialogData dialogData;

    public bool OpenedMenu = false;
    public bool OpenedSettings = false;
    public bool OpenedHelp = false;

    private void Awake()
    {
        Instance = this;
    }

    public void SetDialogData(DialogData dialogData)
    {
        this.dialogData = dialogData;
    }

    public void InitDialog()
    {
        dialogManager.gameObject.SetActive(true);
        dialogManager.InitDialog(this.dialogData);
        PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Paused);
    }

    public void OpenMenu()
    {
        menuManager.gameObject.SetActive(true);
        OpenedMenu = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public void CloseMenu()
    {
        menuManager.gameObject.SetActive(false);
        settingsManager.gameObject.SetActive(false);
        OpenedMenu = false;
        OpenedSettings = false;
        OpenedHelp = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void OpenHelpMenu()
    {
        CloseMenu();
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        OpenedHelp = true;
    }

    public void OpenSettingsMenu() 
    {
        CloseMenu();
        settingsManager.gameObject.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        OpenedSettings = true;

    }

    public void BackToMenu()
    {
        settingsManager.gameObject.SetActive(false);
        OpenMenu();
        OpenedSettings = false;
        OpenedHelp = false;
    }

    public void StartMusicTape(AlbumsTapes tape)
    {
        musicTapeController.StartMusicTape(tape);
    }
}
