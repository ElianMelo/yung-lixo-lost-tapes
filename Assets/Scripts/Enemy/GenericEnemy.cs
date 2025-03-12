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
            PlayerMovementController playerMovementController = collision.gameObject.GetComponent<PlayerMovementController>();

            if(playerMovementController.isAttacking)
            {
                Death();
            } else
            {
                MusicSystem.Instance.PlaySound(SoundEffects.TakeDamage);
                playerMovementController.Knockback(transform.position - collision.transform.position);
            }
        }
    }

    public void Death()
    {
        MusicSystem.Instance.PlaySound(SoundEffects.TakeDamage);
        VFXSystem.Instance.PlayCDCollectVFX(transform.position);
        Destroy(gameObject);
    }
}
