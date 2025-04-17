using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image portraitLeft;
    public Image portraitRight;
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI skipUI;
    public TextMeshProUGUI closeUI;
    public DialogData dialogData;
    public AudioSource audioSource;
    public float letterDelay;

    private int currentDialogIndex;
    private Dialog currentDialog;
    private bool isWriting = false;
    private IEnumerator typeWriterEffectCoroutine;

    public void InitDialog(DialogData dialogData)
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused) return;
        PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Talking);
        this.dialogData = dialogData;
        //Debug.Log(dialogData.dialogs.Count);
        Reset();
    }

    public void Reset()
    {
        portraitLeft.sprite = dialogData.leftPortrait;
        portraitRight.sprite = dialogData.rightPortrait;
        textUI.text = "";
        currentDialog = null;
        currentDialogIndex = -1;
        NextDialog();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Reset();

            //SoundEffectManager.Instance.StopDialogSfx();
            PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Walking);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            this.gameObject.SetActive(false);
            return;
        }

        if (currentDialog != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isWriting)
                {
                    StopCoroutine(typeWriterEffectCoroutine);
                    skipUI.text = "Space - Proximo";
                    textUI.text = "";
                    textUI.text = currentDialog.text;
                    //SoundEffectManager.Instance.StopDialogSfx();
                    isWriting = false;
                }
                else
                {
                    NextDialog();
                }
            }
        }
    }

    private void NextDialog()
    {
        currentDialogIndex += 1;

        if (currentDialogIndex >= dialogData.dialogs.Count)
        {
            Reset();

            //SoundEffectManager.Instance.StopDialogSfx();
            PauseMananger.Instance.ChangeGamePauseState(GamePauseState.Walking);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            this.gameObject.SetActive(false);
            return;
        }

        currentDialog = dialogData.dialogs[currentDialogIndex];
        if(currentDialog.clip != null)
        {
            audioSource.clip = currentDialog.clip;
            audioSource.Play();
        }
        typeWriterEffectCoroutine = TypeWriterEffect();
        StartCoroutine(typeWriterEffectCoroutine);

        // Portrait transparency
        if (currentDialog.isBothPortrait)
        {
            Color colorLeft = portraitLeft.color;
            colorLeft.a = 1f;
            portraitLeft.color = colorLeft;

            Color colorRight = portraitRight.color;
            colorRight.a = 1f;
            portraitRight.color = colorRight;
        }
        else
        {
            Color colorLeft = portraitLeft.color;
            colorLeft.a = currentDialog.isFirstPortrait ? 1f : 0.3f;
            portraitLeft.color = colorLeft;

            Color colorRight = portraitRight.color;
            colorRight.a = currentDialog.isFirstPortrait ? 0.3f : 1f;
            portraitRight.color = colorRight;
        }

        closeUI.gameObject.SetActive(true);
    }

    public IEnumerator TypeWriterEffect()
    {
        char[] chars = currentDialog.text.ToCharArray();
        textUI.text = "";
        isWriting = true;

        foreach (var ch in chars)
        {
            textUI.text += ch;
            yield return new WaitForSeconds(letterDelay);
        }

        isWriting = false;
        yield return null;
    }
}