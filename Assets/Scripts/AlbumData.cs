using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumDataSO", menuName = "ScriptableObjects/AlbumDataSO", order = 1)]
public class AlbumDataSO : ScriptableObject
{
    public List<TrackAudioClip> tracksClips = new List<TrackAudioClip>();
}

[Serializable]
public class TrackAudioClip
{
    public AllTapes track;
    public AudioClip clip;
}

public enum AllTapes
{
    SucessoFM,
    RumoAVitoria,
    Goddamn,
    IWalk,
    WaitingToFly,
    Sonhar,
    ImInLove,
    ForMyFamily,
    GoingHome,
    TheLordAndMe,
    AnjosDaGuarda,
    WeGold
}
