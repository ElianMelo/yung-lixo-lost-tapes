using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollector : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused) return;
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            MusicSystem.Instance.PlaySound(SoundEffects.CollectCoin);
            InterfaceSystem.Instance.IncreaseNote();
            VFXSystem.Instance.PlayStarGenericVFX(transform.position);
            transform.DOScale(new Vector3(0f, 0f, 0f), 1.5f);
            Destroy(gameObject, 17f);
        }
    }
}
