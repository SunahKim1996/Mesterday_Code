using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogTutorial : DialogManager
{    
    [SerializeField] GameObject FadeOutImage;
    InputDialog inputDialog;

    void Start()
    {
        inputDialog = GetComponent<InputDialog>();
        dialogEndCallback = EndDialog;

        LoadJsonData(StartChat);
    }

    void EndDialog()
    {
        FadeOutImage.SetActive(true);
        StartCoroutine(FadeOut());
    }

    protected override Action GetStartCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                callback = () =>
                {
                    SoundManager.instance.PlaySFX(SoundClip.keyboardSFX);
                };
                break;
            case 5:
                callback = () =>
                {
                    SoundManager.instance.PlaySFX(SoundClip.sighSFX);
                };
                break;
            default:
                break;
        }

        return callback;
    }

    protected override Action GetEndCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 1:
                callback = () =>
                {
                    PauseChat();
                    inputDialog.ToggleInputField(InputType.NickName, true);
                };
                break;
            case 2:
                callback = () =>
                {
                    PauseChat();
                    inputDialog.ToggleInputField(InputType.SchoolName, true);
                };
                break;
            default:
                break;
        }

        return callback;
    }

    IEnumerator FadeOut()
    {            
        for (int i = 0; i < 10; i++)                       
        {
            Color color = FadeOutImage.GetComponent<Image>().color;

            float f = i / 10.0f;
            color.a = f;
            FadeOutImage.GetComponent<Image>().color = color;

            PlayerPrefs.SetInt("01_Clear", 1);

            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(2);

        yield return null;                                    
    }
}
