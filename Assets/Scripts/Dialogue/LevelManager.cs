using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum Level
    {
        Menu,
        Tutorial,
        Headquarters1,
        Treeman,
        Headquarters2, 
        Bikerman,
        Headquarters3,
        Turtleman,
        Headquarters4,
        Credits
    }

    public Level CurrentLevel = Level.Menu;
    public AudioSource audioSource;

    public static LevelManager Instance;

    public const string MenuScene = "Menu";
    public const string TutorialScene = "TutorialLevel";
    public const string HeadquartersScene = "Headquarters";
    public const string TreemanScene = "TreeLevel";
    public const string BikermanScene = "BikermanLevel";
    public const string TurtlemanScene = "TurtleLevel";
    public const string CreditsScene = "Credits";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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

    public void GoCreditsLevel()
    {
        CurrentLevel = Level.Credits;
        var SceneName = CreditsScene;
        PlayerCurrentClip(null);
        SceneManager.LoadScene(SceneName);
    }

    public void GoNextLevel()
    {
        string SceneName = HeadquartersScene;
        switch (CurrentLevel)
        {
            case Level.Menu:
                CurrentLevel = Level.Tutorial;
                SceneName = TutorialScene;
                break;
            case Level.Tutorial:
                CurrentLevel = Level.Headquarters1;
                SceneName = HeadquartersScene;
                break;
            case Level.Headquarters1:
                CurrentLevel = Level.Treeman;
                SceneName = TreemanScene;
                break;
            case Level.Treeman:
                CurrentLevel = Level.Headquarters2;
                SceneName = HeadquartersScene;
                break;
            case Level.Headquarters2:
                CurrentLevel = Level.Bikerman;
                SceneName = BikermanScene;
                break;
            case Level.Bikerman:
                CurrentLevel = Level.Headquarters3;
                SceneName = HeadquartersScene;
                break;
            case Level.Headquarters3:
                CurrentLevel = Level.Turtleman;
                SceneName = TurtlemanScene;
                break;
            case Level.Turtleman:
                CurrentLevel = Level.Headquarters4;
                SceneName = HeadquartersScene;
                break;
            case Level.Headquarters4:
                CurrentLevel = Level.Credits;
                SceneName = CreditsScene;
                break;
            case Level.Credits:
                CurrentLevel = Level.Menu;
                SceneName = MenuScene;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
                break;
            default:
                break;
        }
        // PlayerCurrentClip(currentClip);
        SceneManager.LoadScene(SceneName);
    }

    private void PlayerCurrentClip(AudioClip clip, bool force = false)
    {
        if (clip == null) {
            audioSource.Stop();
            return;
        }
        if (audioSource.clip == clip && !force) return;
        audioSource.clip = clip;
        audioSource.Stop();
        audioSource.Play();
    }

    public void ResetCurrentLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
