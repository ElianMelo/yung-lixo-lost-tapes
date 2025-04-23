using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitter : MonoBehaviour
{
    public float minForce = 100f;
    public float maxForce = 500f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) {
            // Get the Rigidbody component of target
            Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            // Calculate the direction ball-target
            Vector3 hitDirection = (transform.position - collision.transform.position).normalized;
            // Apply a force to the ball in the direction of the hit
            targetRigidbody.AddForce(hitDirection * Random.Range(minForce, maxForce), ForceMode.Impulse);
        }
    }
}
