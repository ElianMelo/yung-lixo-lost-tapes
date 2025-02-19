using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTapeController : MonoBehaviour
{
    [SerializeField]
    private GameObject musicTapeCanvas;

    private int finalPosition = 530;
    private int initialPosition = 0;

    public void StartMusicTape()
    {
        musicTapeCanvas.transform.DOMove(musicTapeCanvas.transform.position + new Vector3(finalPosition, 0f, 0f), 2f);
    }
}
