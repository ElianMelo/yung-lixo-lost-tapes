using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    private bool canDecrease = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovementController>().transform;
    }

    void Update()
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused)
        {
            agent.destination = transform.position;
            return;
        }
        agent.destination = player.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementController playerMovementController = collision.gameObject.GetComponent<PlayerMovementController>();

            if(playerMovementController.isAttacking)
            {
                Death();
            } else
            {
                DecreseNote();
                MusicSystem.Instance.PlaySound(SoundEffects.TakeDamage);
                playerMovementController.Knockback(transform.position - collision.transform.position);
            }
        }
    }

    private void DecreseNote()
    {
        if (canDecrease)
        {
            canDecrease = false;
            Invoke(nameof(RestoreDecreseNote), 1f);
            InterfaceSystem.Instance.DecreaseNote();
        }
    }

    private void RestoreDecreseNote()
    {
        canDecrease = true;
    }

    public void Death()
    {
        MusicSystem.Instance.PlaySound(SoundEffects.TakeDamage);
        ShakeSystem.Instance.Shake();
        VFXSystem.Instance.PlayStarGenericVFX(transform.position);
        Destroy(gameObject);
    }
}
