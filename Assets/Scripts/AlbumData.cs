using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumDataSO", menuName = "ScriptableObjects/AlbumDataSO", order = 1)]
public class AlbumDataSO : ScriptableObject
{
    public List<TrackData> tracksClips = new List<TrackData>();
}

[Serializable]
public class TrackData
{
    public AlbumsTapes tape;
    public AudioClip clip;
    public string name;
}

public enum AlbumsTapes
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
