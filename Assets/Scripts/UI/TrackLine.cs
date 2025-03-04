using TMPro;
using UnityEngine;

public class TrackLine : MonoBehaviour
{
    [HideInInspector] public AlbumsTapes selectedTrack;
    [HideInInspector] public AudioClip clip;
    [HideInInspector] public string cardName;

    public TMP_Text textField;

    private string fullTextValue;
    private string hiddenTextValue;
    private AlbumMenuController albumMenuController;

    private bool isActive = false;

    public void SetController(AlbumMenuController controller)
    {
        albumMenuController = controller;
    }

    public void SetFullTextValue(string text)
    {
        fullTextValue = text;
    }

    public void SetHiddenTextValue(string text)
    {
        hiddenTextValue = text;
    }

    public void SetupTextFieldPlaceholder()
    {
        textField.text = hiddenTextValue;
    }

    public void SetupTextField()
    {
        isActive = true;
        textField.text = fullTextValue;
    }

    public void PlayCurrentTrack()
    {
        if (!isActive) return;
        albumMenuController.currentTape = selectedTrack;
        MusicSystem.Instance.PlayTapeMusic(selectedTrack);
        InterfaceSystem.Instance.SetupAlbumMenuTrack();
    }
}
