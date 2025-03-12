using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "ScriptableObjects/SoundDataSO", order = 1)]
public class SoundDataSO : ScriptableObject
{
    public List<SoundClipData> soundClips = new List<SoundClipData>();
}

[Serializable]
public class SoundClipData
{
    public SoundEffects sound;
    public AudioClip clip;
}

public enum SoundEffects
{
    Attack,
    CollectCD,
    CollectCoin,
    Jump,
    JumpPad,
    TakeDamage
}