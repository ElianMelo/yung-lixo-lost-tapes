using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSystem : MonoBehaviour
{
    [SerializeField]
    private MusicTapeController musicTapeController;

    public static InterfaceSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartMusicTape()
    {
        musicTapeController.StartMusicTape();
    }
}
