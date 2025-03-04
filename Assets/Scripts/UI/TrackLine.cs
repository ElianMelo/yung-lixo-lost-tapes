using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackLine : MonoBehaviour
{
    [HideInInspector]
    public AlbumsTapes SelectedTrack;
    [HideInInspector]
    public AudioClip clip;
    [HideInInspector]
    public string cardName;
    [HideInInspector]
    public AlbumMenuController controller;

    public TMP_Text textField;

    private string fullTextValue;
    private string hiddenTextValue;

    private bool isActive = false;

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
        controller.PlayTrack(this);
    }
}
