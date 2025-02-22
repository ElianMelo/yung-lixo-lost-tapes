using UnityEngine;

public enum GamePauseState
{
    Walking,
    Talking,
    Paused
}

public class PauseMananger : MonoBehaviour
{
    public static PauseMananger Instance;

    public GamePauseState CurrentState = GamePauseState.Walking;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeGamePauseState(GamePauseState newState)
    {
        CurrentState = newState;
    }
    
}
