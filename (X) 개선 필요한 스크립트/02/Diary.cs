using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public GameObject DiaryScreen;
    public GameObject NoteBtn;

    public bool isUserDataChange = false;

    public GameObject StartBtn_1_1;
    public GameObject StartBtn_1_2;
    public GameObject StartBtn_1_3;
    public GameObject StartBtn_1_4;
    public GameObject StartBtn_1_5;
    public GameObject StartBtn_1_6;

    public GameObject backBtn;

    public AudioSource soundEffect;
    public AudioClip normalButtonEffect;

    private UserDataInfo userData;

    private void Start()
    {
        userData = UserData.instance.userData;

        backBtn.SetActive(false);
        NoteBtn.SetActive(false);
        DiaryScreen.SetActive(false);

        StartBtn_1_1.SetActive(false);
        StartBtn_1_2.SetActive(false);
        StartBtn_1_3.SetActive(false);
        StartBtn_1_4.SetActive(false);
        StartBtn_1_5.SetActive(false);
        StartBtn_1_6.SetActive(false);

        if (userData.clearTutorial)
        {
            NoteBtn.SetActive(true);
            StartBtn_1_1.SetActive(true);
        }

        if (userData.clear01 && userData.stage1_Score != 0)
        {
            StartBtn_1_2.SetActive(true);
        }

        if (userData.clear02 && userData.stage2_Score != 0)
        {
            StartBtn_1_3.SetActive(true);
        }

        if (userData.clear03 && userData.stage3_Score != 0)
        {
            StartBtn_1_4.SetActive(true);
        }

        if (userData.clear04 && userData.stage4_Score != 0)
        {
            StartBtn_1_5.SetActive(true);
        }

        if (userData.clear05 && userData.stage5_Score != 0)
        {
            StartBtn_1_6.SetActive(true);
        }
    }

    public void DiaryButton()
    {
        backBtn.SetActive(true);

        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        DiaryScreen.SetActive(true);
        NoteBtn.SetActive(false);

        PlayerTrigger.Instance.isNoteOrDiaryOn = true;
        StarPoint.Instance.isDiaryOn = true;
    }

    public void BackButton()
    {
        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        DiaryScreen.SetActive(false);
        NoteBtn.SetActive(true);

        PlayerTrigger.Instance.isNoteOrDiaryOn = false;
        StarPoint.Instance.isDiaryOn = false;

        backBtn.SetActive(false);
    }

    public void StartBtn_1_1_On()
    {
        LoadingManager.LoadScene(3);
    }

    public void StartBtn_1_2_On()
    {
        LoadingManager.LoadScene(4);
    }

    public void StartBtn_1_3_On()
    {
        LoadingManager.LoadScene(5);
    }

    public void StartBtn_1_4_On()
    {
        LoadingManager.LoadScene(6);
    }

    public void StartBtn_1_5_On()
    {
        LoadingManager.LoadScene(7);
    }

    public void StartBtn_1_6_On()
    {
        LoadingManager.LoadScene(8);
    }
}
