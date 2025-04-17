using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource tapeMusicAudioSource;
    [SerializeField] private List<AudioSource> soundChannelList;
    [SerializeField] private List<AudioClip> backgroundClips;
    [SerializeField] private AlbumDataSO albumDataSO;
    [SerializeField] private SoundDataSO soundDataSO;

    public static MusicSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayBackgroundMusic(backgroundClips[Random.Range(0, backgroundClips.Count)]);
    }

    private void Update()
    {
        if(tapeMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Pause();
        } else
        {
            if(!backgroundMusicAudioSource.isPlaying)
            {
                backgroundMusicAudioSource.Play();
            }
        }
    }

    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        backgroundMusicAudioSource.clip = audioClip;
        backgroundMusicAudioSource.Play();
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

    public void PlaySound(SoundEffects soundEffects)
    {
        foreach (var soundClipData in soundDataSO.soundClips)
        {
            if(soundClipData.sound == soundEffects)
            {
                PlaySoundEffect(soundClipData.clip);
            }
        }
    }

    private void PlaySoundEffect(AudioClip audioClip)
    {
        foreach (var currentChannel in soundChannelList)
        {
            if (currentChannel.isPlaying)
            {
                continue;
            }
            currentChannel.clip = audioClip;
            currentChannel.Play();
            return;
        }
    }

    private void PlaySelectedTape(AudioClip audioClip)
    {
        tapeMusicAudioSource.clip = audioClip;
        tapeMusicAudioSource.Play();
    }

    public float GetTapeTime()
    {
        return tapeMusicAudioSource.time;
    }

    public void PlayTape()
    {
        tapeMusicAudioSource.Play();
    }

    public void UnPauseTape()
    {
        tapeMusicAudioSource.UnPause();
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
