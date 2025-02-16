using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotator : MonoBehaviour
{
    public float rotateAmount;
    public float rotateFactor;

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            transform.rotation * Quaternion.Euler(0f, rotateAmount, 0f), rotateFactor);
    }
}
