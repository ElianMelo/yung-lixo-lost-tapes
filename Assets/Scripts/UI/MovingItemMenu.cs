using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MovingItemMenu : MonoBehaviour
{
    private RectTransform rectTransform;
    private float time;
    private Vector3 pos;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        pos = rectTransform.transform.position;
        time = Random.Range(1f, 1.5f);
        rectTransform.DOMove(new Vector3(pos.x + Random.Range(-10.7f, 10.7f), pos.y + Random.Range(-10.7f, 10.7f), pos.z), time)
            .SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        time = Random.Range(1f, 1.5f);
        rectTransform.DOMove(new Vector3(pos.x + Random.Range(-10.7f, 10.7f), pos.y + Random.Range(-10.7f, 10.7f), pos.z), time)
            .SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        StartCoroutine(Move());
    }
}
