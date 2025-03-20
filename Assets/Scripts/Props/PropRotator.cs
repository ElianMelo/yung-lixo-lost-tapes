using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotator : MonoBehaviour
{
    public float rotateAmount;
    public float rotateFactor;

    public float xStartRotation = 0f;
    public float zStartRotation = 0f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(xStartRotation, Random.Range(0f, 360f), zStartRotation);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            transform.rotation * Quaternion.Euler(0f, rotateAmount, 0f), rotateFactor);
    }
}
