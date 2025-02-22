using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicTapeController : MonoBehaviour
{
    [SerializeField]
    private RectTransform musicTapeCanvas;
    //[SerializeField]
    //private RectTransform musicTapeCanvasTarget;
    [SerializeField]
    private TMP_Text musicTapeName;
    [SerializeField]
    private Slider musicTapeTime;

    private int initialPosition = 530;
    private int finalPosition = 0;

    public void StartMusicTape(AlbumsTapes tapes)
    {
        TrackData tapeData = MusicSystem.Instance.GetTapeData(tapes);
        musicTapeName.text = tapeData.name;
        musicTapeTime.value = 0f;
        musicTapeCanvas.DOAnchorPosX(finalPosition, 2f);
        musicTapeTime.DOValue(1f, tapeData.clip.length).SetEase(Ease.Linear);
        StartCoroutine(StopMusicTapeCoroutine(tapeData.clip.length - 2f));
    }

    private IEnumerator StopMusicTapeCoroutine(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        StopMusicTape();
    }

    private void StopMusicTape()
    {
        musicTapeCanvas.DOAnchorPosX(initialPosition, 2f).OnComplete(() =>
        {
            musicTapeTime.value = 0f;
        });
    }
}
