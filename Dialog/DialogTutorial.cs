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

        dialogType = DialogType.Tutorial;
        dialogEndCallback = EndDialog;

        LoadJsonData(StartChat);
    }

    void LoadJsonData(Action endCallback)
    {
        // Json Data 로드 
        dialogueData = JsonUtility.FromJson<DialogueData>(dialogJsonData.text);

        for (int i = 0; i < dialogueData.dialogTextList.Count; i++)
        {
            DialogInfo info = new DialogInfo();
            info.dialogText = dialogueData.dialogTextList[i];
            info.startCallback = GetStartCallback(i);
            info.endCallback = GetEndCallback(i);

            dialogueDataList.Add(info);
        }

        // 대사 출력 시작 
        endCallback();
    }

    void EndDialog()
    {
        FadeOutImage.SetActive(true);
        StartCoroutine(FadeOut());
    }

    Action GetStartCallback(int index)
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

    Action GetEndCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 1:
                callback = () =>
                {
                    inputDialog.ToggleInputField(InputType.NickName, true);
                };
                break;
            case 2:
                callback = () =>
                {
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
