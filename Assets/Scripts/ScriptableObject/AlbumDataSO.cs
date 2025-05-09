using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumDataSO", menuName = "ScriptableObjects/AlbumDataSO", order = 1)]
public class AlbumDataSO : ScriptableObject
{
    public string albumName;
    public Sprite albumCover;
    public List<TrackData> tracksClips = new List<TrackData>();
    public List<AudioClip> transitionClips = new List<AudioClip>();
}

[Serializable]
public class TrackData
{
    public AlbumsTapes tape;
    public AudioClip clip;
    public string name;
    public string number;
}

public enum AlbumsTapes
{
    // Validation
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
    WeGold,
    // BittersweetMemories
    Saude,
    OFilhoProdigo,
    LetItPour,
    TheStory,
    BackHome,
    MelhorNao,
    Oldschool,
    DirtyDancing,
    PrefiroMorrer,
    QuasePerfeito,
    MaquinaDoTempo,
    FloresDeOutroCarnaval,
    // 13LentesDeUmFinalFeliz
    AuRevoir,
    Carruagem,
    Agradeco,
    Corrida,
    JoiasDaFamilia,
    NuncaParo,
    Memorias,
    LostInTranslation,
    Primavera,
    LetMeSeeTheSun,
    Adeus,
    DeixaDoer,
    NaoMeFacaFalar,
    // Cybertapes
    Noite,
    GhostChroma,
    DiamantesEPeixes,
    Akira,
    NineMM,
    Tapa,
    Festa,
    SmoothAF,
    Fantasmas,
    Hitomi
}
