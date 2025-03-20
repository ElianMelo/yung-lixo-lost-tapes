using DG.Tweening;
using UnityEngine;

public class CDCollector : MonoBehaviour
{
    public AlbumsTapes tape;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            MusicSystem.Instance.PlaySound(SoundEffects.CollectCD);
            InterfaceSystem.Instance.StartMusicTape(tape);
            InterfaceSystem.Instance.RevealTrack(tape);
            MusicSystem.Instance.PlayTapeMusic(tape);
            VFXSystem.Instance.PlayCDCollectVFX(transform.position);
            transform.DOScale(new Vector3(0f, 0f, 0f), 1.5f);
            Destroy(gameObject, 17f);
        }
    }
}
