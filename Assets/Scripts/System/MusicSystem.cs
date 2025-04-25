using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource tapeMusicAudioSource;
    [SerializeField] private List<AudioSource> soundChannelList;
    [SerializeField] private List<AudioClip> backgroundClips;
    [SerializeField] private AudioSource transitionSource;
    [SerializeField] private AlbumDataSO albumDataSO;
    [SerializeField] private SoundDataSO soundDataSO;

    public static MusicSystem Instance;

    private AudioClip selectedTransition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectedTransition = albumDataSO.transitionClips[Random.Range(0, albumDataSO.transitionClips.Count)];
        PlayBackgroundMusic(backgroundClips[Random.Range(0, backgroundClips.Count)]);
    }

    private void Update()
    {
        if(tapeMusicAudioSource.isPlaying || transitionSource.isPlaying)
        {
            backgroundMusicAudioSource.Pause();
        } else
        {
            if(!backgroundMusicAudioSource.isPlaying && !transitionSource.isPlaying)
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

    public void SelectTransition()
    {
        selectedTransition = albumDataSO.transitionClips[Random.Range(0, albumDataSO.transitionClips.Count)];
        transitionSource.clip = selectedTransition;
        transitionSource.Play();
    }

    public void PlayTapeMusic(AlbumsTapes tape)
    {
        StartCoroutine(PlayerTapeMusicWithTransition(tape));
    }

    public float SelectedTransitionDuration()
    {
        return selectedTransition.length;
    }

    private IEnumerator PlayerTapeMusicWithTransition(AlbumsTapes tape)
    {
        StopTape();
        yield return new WaitForSeconds(selectedTransition.length);
        AudioClip clip = albumDataSO.tracksClips.FirstOrDefault(t => t.tape == tape).clip;
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
