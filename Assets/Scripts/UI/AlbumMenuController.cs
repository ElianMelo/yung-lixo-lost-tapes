using DG.Tweening;
using System.Collections;
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
    public AlbumsTapes currentTape = AlbumsTapes.Saude;

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

    public void ResumeAlbumTrack(float tapeTime = 0f)
    {
        TrackData tapeData = MusicSystem.Instance.GetTapeData(currentTape);
        currentTrackProgress.DOFillAmount(1f, tapeData.clip.length - tapeTime).SetEase(Ease.Linear);
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
        StartCoroutine(SetupAlbumMenuTrackCoroutine());
    }

    private IEnumerator SetupAlbumMenuTrackCoroutine()
    {
        TrackData tapeData = MusicSystem.Instance.GetTapeData(currentTape);
        currentTrackText.text = tapeData.name;
        currentTrackProgress.fillAmount = 0;
        DOTween.Kill(currentTrackProgress);
        yield return new WaitForSeconds(MusicSystem.Instance.SelectedTransitionDuration());
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
        // cannot interact without getting any disc
        if (currentTape == AlbumsTapes.Saude) return;
        StartCoroutine(PlayTapeCoroutine());
    }

    private IEnumerator PlayTapeCoroutine()
    {
        yield return new WaitForSeconds(MusicSystem.Instance.SelectedTransitionDuration());
        ResumeAlbumTrack();
        MusicSystem.Instance.PlayTape();
    }

    public void UnPauseTape()
    {
        // cannot interact without getting any disc
        if (currentTape == AlbumsTapes.Saude) return;
        if (MusicSystem.Instance.GetTapeTime() == 0) { 
            StopAlbumTrack(); 
            PlayTape(); 
            return; 
        }
        ResumeAlbumTrack(MusicSystem.Instance.GetTapeTime());
        MusicSystem.Instance.UnPauseTape();
    }

    public void StopTape()
    {
        // cannot interact without getting any disc
        if (currentTape == AlbumsTapes.Saude) return;
        StopAlbumTrack();
        MusicSystem.Instance.StopTape();
    }

    public void PauseTape()
    {
        // cannot interact without getting any disc
        if (currentTape == AlbumsTapes.Saude) return;
        PauseAlbumTrack();
        MusicSystem.Instance.PauseTape();
    }

}
