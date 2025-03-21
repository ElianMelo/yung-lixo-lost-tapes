using DG.Tweening;
using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private bool isJumping = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused) return;
        if (isJumping) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            isJumping = true;
            MusicSystem.Instance.PlaySound(SoundEffects.JumpPad);
            Sequence sequence = DOTween.Sequence();
            collision.gameObject.GetComponent<PlayerMovementController>().Jump(true);
            sequence.Append(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.Linear));
            sequence.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f).SetEase(Ease.Linear));
            StartCoroutine(RestoreJumping());
        }
    }

    private IEnumerator RestoreJumping()
    {
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

}
