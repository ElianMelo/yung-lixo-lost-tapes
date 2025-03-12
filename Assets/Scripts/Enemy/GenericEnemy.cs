using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovementController>().transform;
    }

    void Update()
    {
        agent.destination = player.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 upward = transform.TransformDirection(Vector3.up);
            Vector3 toOther = Vector3.Normalize(collision.transform.position - transform.position);

            
            if (Vector3.Dot(upward, toOther) < 0.3f)
            {
                // player take damage
                collision.gameObject.GetComponent<PlayerMovementController>().Knockback(collision.transform.position - transform.position);
            } else
            {
                // enemy take damage
                Destroy(gameObject);
            }

        }
    }
}
