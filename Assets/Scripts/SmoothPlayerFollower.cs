using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPlayerFollower : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed);
    }

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
