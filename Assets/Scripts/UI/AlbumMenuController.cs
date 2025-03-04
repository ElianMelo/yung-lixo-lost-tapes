using System.Collections.Generic;
using UnityEngine;

public class AlbumMenuController : MonoBehaviour
{
    public static string DefaultTrackValue = "01 - -----------------------";

    [SerializeField] private AlbumDataSO albumDataSO;
    [SerializeField] private Transform parentListOfTracks;
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private AudioSource audioSource;

    private List<TrackLine> tracks = new List<TrackLine>();

    public GameObject albumMenuVisuals;

    void Start()
    {
        audioSource.clip = albumDataSO.tracksClips[0].clip;
        foreach (var currentTrack in albumDataSO.tracksClips)
        {
            GameObject trackInstance = Instantiate(trackPrefab, parentListOfTracks);
            TrackLine trackLine = trackInstance.GetComponent<TrackLine>();
            string fullTextValue = currentTrack.number + " - " + currentTrack.name;
            string hiddenTextValue = currentTrack.number + " - -----------------------";
            trackLine.SelectedTrack = currentTrack.tape;
            trackLine.clip = currentTrack.clip;
            trackLine.controller = this;
            trackLine.SetFullTextValue(fullTextValue);
            trackLine.SetHiddenTextValue(hiddenTextValue);
            trackLine.SetupTextFieldPlaceholder();
            tracks.Add(trackLine);
        }
    }

    public void RevealTrack(AlbumsTapes tape)
    {
        foreach (var track in tracks)
        {
            if (track.SelectedTrack == tape)
            {
                track.SetupTextField();
                return;
            }
        }
    }

    public void PlayTrack(TrackLine trackLine)
    {
        audioSource.clip = trackLine.clip;
        audioSource.Play();
    }

    public void PlayCurrentTrack()
    {
        audioSource.Play();
    }

    public void PauseCurrentTrack()
    {
        audioSource.Pause();
    }

    public void StopCurrentTrack()
    {
        audioSource.Stop();
    }

    public void Activate()
    {
        albumMenuVisuals.SetActive(true);
    }

    public void Deactivate()
    {
        albumMenuVisuals.SetActive(false);
    }
    
}
