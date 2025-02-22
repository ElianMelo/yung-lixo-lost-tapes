using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InterfaceSystem : MonoBehaviour
{
    [SerializeField]
    private MusicTapeController musicTapeController;

    public static InterfaceSystem Instance;

    public DialogManager dialogManager;
    public MenuManager menuManager;
    public settingsManager settingsManager;
    public GameObject help;
    public TMP_Text helpText;

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
        help.SetActive(false);
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
        help.SetActive(true);
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
        help.SetActive(false);
        OpenMenu();
        OpenedSettings = false;
        OpenedHelp = false;
    }

    public void StartMusicTape(AlbumsTapes tape)
    {
        musicTapeController.StartMusicTape(tape);
    }
}
