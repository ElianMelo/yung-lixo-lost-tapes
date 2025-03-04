using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource tapeMusicAudioSource;
    [SerializeField] private List<AudioSource> soundChannelList;
    [SerializeField] private AlbumDataSO albumDataSO;

    public static MusicSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        backgroundMusicAudioSource.clip = audioClip;
    }

    public TrackData GetTapeData(AlbumsTapes tape)
    {
        foreach (var currentTape in albumDataSO.tracksClips)
        {
            if(currentTape.tape == tape)
            {
                return currentTape;
            }
        }
        return null;
    }

    public void PlayTapeMusic(AlbumsTapes tape)
    {
        AudioClip clip = albumDataSO.tracksClips.FirstOrDefault(t =>  t.tape == tape).clip;
        PlaySelectedTape(clip);
        InterfaceSystem.Instance.SetupAlbumMenuTrack();
    }

    public void PlaySound(AudioClip audioClip)
    {

    }

    private void PlaySelectedTape(AudioClip audioClip)
    {
        tapeMusicAudioSource.clip = audioClip;
        tapeMusicAudioSource.Play();
    }

    public void PlayTape()
    {
        tapeMusicAudioSource.Play();
    }

    public void PauseTape()
    {
        tapeMusicAudioSource.Pause();
    }

    public void StopTape()
    {
        tapeMusicAudioSource.Stop();
    }
}
