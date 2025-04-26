using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicTapeController : MonoBehaviour
{
    [SerializeField]
    private RectTransform musicTapeCanvas;
    [SerializeField]
    private TMP_Text musicTapeName;
    [SerializeField]
    private Image musicTapeTime;

    private int initialPosition = 530;
    private int finalPosition = 0;

    public void StartMusicTape(AlbumsTapes tapes)
    {
        StartCoroutine(StartMusicTapeCoroutine(tapes));
    }

    private IEnumerator StartMusicTapeCoroutine(AlbumsTapes tapes)
    {
        TrackData tapeData = MusicSystem.Instance.GetTapeData(tapes);
        musicTapeName.text = tapeData.name;
        musicTapeTime.fillAmount = 0;
        DOTween.Kill(musicTapeTime);
        yield return new WaitForSeconds(MusicSystem.Instance.SelectedTransitionDuration());
        musicTapeTime.DOFillAmount(1f, tapeData.clip.length).SetEase(Ease.Linear);
        musicTapeCanvas.DOAnchorPosX(finalPosition, 2f);
        StartCoroutine(StopMusicTapeCoroutine(tapeData.clip.length - 2f));
    }

    public void EarlyInterfaceReturn()
    {
        StopAllCoroutines();
        DOTween.Kill(musicTapeCanvas);
        musicTapeCanvas.DOAnchorPosX(initialPosition, 0.1f).SetEase(Ease.OutExpo);
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
            musicTapeTime.fillAmount = 0f;
        });
    }
}
