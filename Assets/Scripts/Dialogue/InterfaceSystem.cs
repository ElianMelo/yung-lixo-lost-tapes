using UnityEngine;

public enum MenuStates
{
    Disabled,
    Enabled,
    Settings,
    Album,
}

public class InterfaceSystem : MonoBehaviour
{
    [SerializeField] private MusicTapeController musicTapeController;

    public static InterfaceSystem Instance;

    public DialogManager dialogManager;
    public MenuManager menuManager;
    public settingsManager settingsManager;
    public AlbumMenuController albumMenuController;

    private DialogData dialogData;

    public MenuStates currentMenuState = MenuStates.Disabled;

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

    public void RevealTrack(AlbumsTapes tape)
    {
        albumMenuController.RevealTrack(tape);
    }

    public void OpenAlbumMenu()
    {
        musicTapeController.EarlyInterfaceReturn();
        albumMenuController.Activate();
        currentMenuState = MenuStates.Album;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseAlbumMenu()
    {
        albumMenuController.Deactivate();
        currentMenuState = MenuStates.Disabled;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenMenu()
    {
        musicTapeController.EarlyInterfaceReturn();
        menuManager.gameObject.SetActive(true);
        currentMenuState = MenuStates.Enabled;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        menuManager.gameObject.SetActive(false);
        settingsManager.gameObject.SetActive(false);
        currentMenuState = MenuStates.Disabled;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettingsMenu() 
    {
        CloseMenu();
        settingsManager.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currentMenuState = MenuStates.Settings;
    }

    public void BackToMenu()
    {
        settingsManager.gameObject.SetActive(false);
        OpenMenu();
        currentMenuState = MenuStates.Enabled;
    }

    public void StartMusicTape(AlbumsTapes tape)
    {
        musicTapeController.StartMusicTape(tape);
    }

    public void SetupAlbumMenuTrack()
    {
        albumMenuController.SetupAlbumMenuTrack();
    }
}
