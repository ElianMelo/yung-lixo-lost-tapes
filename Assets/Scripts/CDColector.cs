using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDColector : MonoBehaviour
{
    public GameObject particleSystemObject;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            var instance = Instantiate(particleSystemObject, transform.position, Quaternion.Euler(-90f,0f,0f));
            instance.GetComponent<ParticleSystem>().Play(); 
            Destroy(gameObject, 2f);
        }
    }
}
