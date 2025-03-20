using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalatroEffectPlayback : MonoBehaviour
{
    public Renderer render;
    public float updateRate;
    private float playback;

    void Update()
    {
        playback += updateRate;
        render.material.SetFloat("TIMESTAMP", playback);
    }
}
