using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDColector : MonoBehaviour
{
    public GameObject particleSystemObject;
    public AudioSource audioSource;
    public AudioClip clip;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            audioSource.clip = clip;
            audioSource.Play();
            var instance = Instantiate(particleSystemObject, transform.position, Quaternion.Euler(-90f,0f,0f));
            instance.GetComponent<ParticleSystem>().Play();
            transform.DOScale(new Vector3(0f, 0f, 0f), 1.5f);
            Destroy(gameObject, 16f);
        }
    }
}
