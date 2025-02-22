using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void OnResumeClick()
    {
        PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Walking);
        InterfaceSystem.Instance.CloseMenu();
    }

    public void OnSettingsClick()
    {
        InterfaceSystem.Instance.OpenSettingsMenu();
    }

    public void OnHelpClick()
    {
        InterfaceSystem.Instance.OpenHelpMenu();
    }

    public void OnLeaveGameClick()
    {
        //Debug.Log("Exiting the game...");
        Application.Quit();
    }
}
