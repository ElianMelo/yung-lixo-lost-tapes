using TMPro;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public TMP_Text notesTextField;
    private int noteCount = 0;

    void Start()
    {
        noteCount = 0;
        notesTextField.text = noteCount.ToString();
    }

    public void IncreaseNoteCount()
    {
        noteCount++;
        notesTextField.text = noteCount.ToString();
    }

    public void DecreaseNoteCount()
    {
        noteCount--;
        noteCount = noteCount < 0 ? 0 : noteCount;
        notesTextField.text = noteCount.ToString();
    }
}
