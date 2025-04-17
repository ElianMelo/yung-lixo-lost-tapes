using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlbumMenuController : MonoBehaviour
{
    public static string DefaultTrackValue = "01 - -----------------------";

    [SerializeField] private AlbumDataSO albumDataSO;
    [SerializeField] private Transform parentListOfTracks;
    [SerializeField] private GameObject trackPrefab;

    [SerializeField] private TMP_Text currentTrackText;
    [SerializeField] private Image currentTrackProgress;

    private List<TrackLine> tracks = new List<TrackLine>();

    public GameObject albumMenuVisuals;
    public AlbumsTapes currentTape;

    [Header("Control Album Interface")]
    [SerializeField] private GameObject albumAreaObject;
    [SerializeField] private GameObject tutorialAreaObject;
    [SerializeField] private GameObject cartaDevAreaObject;
    [SerializeField] private GameObject cartaArtAreaObject;

    void Start()
    {
        foreach (var currentTrack in albumDataSO.tracksClips)
        {
            GameObject trackInstance = Instantiate(trackPrefab, parentListOfTracks);
            TrackLine trackLine = trackInstance.GetComponent<TrackLine>();
            string fullTextValue = currentTrack.number + " - " + currentTrack.name;
            string hiddenTextValue = currentTrack.number + " - -----------------------";
            trackLine.selectedTrack = currentTrack.tape;
            trackLine.clip = currentTrack.clip;
            trackLine.SetFullTextValue(fullTextValue);
            trackLine.SetHiddenTextValue(hiddenTextValue);
            trackLine.SetupTextFieldPlaceholder();
            trackLine.SetController(this);
            tracks.Add(trackLine);
        }
    }

    public void ShowAlbum()
    {
        DisableAllAreas();
        albumAreaObject.SetActive(true);
    }

    public void ShowTutorial()
    {
        DisableAllAreas();
        tutorialAreaObject.SetActive(true);
    }

    public void ShowCartaDev()
    {
        DisableAllAreas();
        cartaDevAreaObject.SetActive(true);
    }

    public void ShowCartaArt()
    {
        DisableAllAreas();
        cartaArtAreaObject.SetActive(true);
    }

    private void DisableAllAreas()
    {
        albumAreaObject.SetActive(false);
        tutorialAreaObject.SetActive(false);
        cartaDevAreaObject.SetActive(false);
        cartaArtAreaObject.SetActive(false);
    }

    public void RevealTrack(AlbumsTapes tape)
    {
        foreach (var track in tracks)
        {
            if (track.selectedTrack == tape)
            {
                currentTape = tape;
                track.SetupTextField();
                return;
            }
        }
    }

    public void ResumeAlbumTrack()
    {
        TrackData tapeData = MusicSystem.Instance.GetTapeData(currentTape);
        currentTrackProgress.DOFillAmount(1f, tapeData.clip.length).SetEase(Ease.Linear);
    }

    public void PauseAlbumTrack()
    {
        DOTween.Kill(currentTrackProgress);
    }

    public void StopAlbumTrack()
    {
        currentTrackProgress.fillAmount = 0f;
        DOTween.Kill(currentTrackProgress);
    }

    public void SetupAlbumMenuTrack()
    {
        TrackData tapeData = MusicSystem.Instance.GetTapeData(currentTape);
        currentTrackText.text = tapeData.name;
        currentTrackProgress.fillAmount = 0;
        DOTween.Kill(currentTrackProgress);
        currentTrackProgress.DOFillAmount(1f, tapeData.clip.length).SetEase(Ease.Linear);
    }

    public void Activate()
    {
        albumMenuVisuals.SetActive(true);
    }

    public void Deactivate()
    {
        albumMenuVisuals.SetActive(false);
    }

    public void PlayTape()
    {
        ResumeAlbumTrack();
        MusicSystem.Instance.PlayTape();
    }

    public void StopTape()
    {
        StopAlbumTrack();
        MusicSystem.Instance.StopTape();
    }

    public void PauseTape()
    {
        PauseAlbumTrack();
        MusicSystem.Instance.PauseTape();
    }

}
