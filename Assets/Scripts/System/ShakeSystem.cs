using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeSystem : MonoBehaviour
{
    CinemachineImpulseSource impulseSource;

    public static ShakeSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake()
    {
        impulseSource.GenerateImpulse();
    }
}
