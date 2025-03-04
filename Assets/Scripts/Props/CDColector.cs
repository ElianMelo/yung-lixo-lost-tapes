using DG.Tweening;
using UnityEngine;

public class CDColector : MonoBehaviour
{
    public GameObject particleSystemObject;
    public AlbumsTapes tape;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            InterfaceSystem.Instance.StartMusicTape(tape);
            InterfaceSystem.Instance.RevealTrack(tape);
            MusicSystem.Instance.PlayTapeMusic(tape);
            var instance = Instantiate(particleSystemObject, transform.position, Quaternion.Euler(-90f,0f,0f));
            instance.GetComponent<ParticleSystem>().Play();
            transform.DOScale(new Vector3(0f, 0f, 0f), 1.5f);
            Destroy(gameObject, 16f);
        }
    }
}
