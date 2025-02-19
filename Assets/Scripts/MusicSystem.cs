using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    [SerializeField]
    private AudioSource backgroundMusicAudioSource;
    [SerializeField]
    private AudioSource tapeMusicAudioSource;
    [SerializeField]
    private List<AudioSource> soundChannelList;
    [SerializeField]
    private AlbumDataSO albumDataSO;

    public static MusicSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        backgroundMusicAudioSource.clip = audioClip;
    }

    public void PlayTapeMusic(AllTapes tape)
    {
        AudioClip clip = albumDataSO.tracksClips.FirstOrDefault(t =>  t.track == tape).clip;
        PlaySelectedTape(clip);
    }

    public void PlaySound(AudioClip audioClip)
    {

    }

    private void PlaySelectedTape(AudioClip audioClip)
    {
        tapeMusicAudioSource.clip = audioClip;
        tapeMusicAudioSource.Play();
    }
}
