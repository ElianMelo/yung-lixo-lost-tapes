using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum Level
    {
        Menu,
        Level
    }

    public Level CurrentLevel = Level.Menu;

    public static LevelManager Instance;

    public const string MenuScene = "Menu";
    public const string LevelScene = "Level";

    private void Awake()
    {
        Instance = this;
    }

    public void GoFirstLevel()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void GoMenuLevel()
    {
        CurrentLevel = Level.Menu;
        SceneManager.LoadScene(MenuScene);
    }

    public void GoMainLevel()
    {
        CurrentLevel = Level.Level;
        SceneManager.LoadScene(LevelScene);
    }

    public void ResetCurrentLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
