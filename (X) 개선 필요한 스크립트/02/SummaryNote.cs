using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummaryNote : MonoBehaviour
{
    public GameObject NoteScreen;
    public GameObject DiaryBtn;

    public GameObject NewSign;

    public List<GameObject> Stamp = new List<GameObject>();
    public List<GameObject> NoText = new List<GameObject>();
    public List<GameObject> Btn = new List<GameObject>();

    public GameObject backBtn;

    public AudioSource soundEffect;
    public AudioClip normalButtonEffect;

    private UserDataInfo userData;

    private void Start()
    {
        userData = UserData.instance.userData;

        backBtn.SetActive(false);
        DiaryBtn.SetActive(false);
        NoteScreen.SetActive(false);
        NewSign.SetActive(false);

        for (int i = 0; i < Stamp.Count; i++)
        {
            NoText[i].SetActive(true);
            Btn[i].SetActive(false);
            Stamp[i].SetActive(false);
            print("stamp Length = " + i);
        }

        if (userData.clearTutorial)
        {
            DiaryBtn.SetActive(true);
        }

        if (userData.clear01 && userData.stage1_Score >= 3)
        {
            Stamp[0].SetActive(true);
            Btn[0].SetActive(true);
            NoText[0].SetActive(false);
            if (userData.NoteChange)
            {
                NewSign.SetActive(true);
            }            
        }

        if (userData.clear02 && userData.stage2_Score >= 3)
        {
            Stamp[1].SetActive(true);
            Btn[1].SetActive(true);
            NoText[1].SetActive(false);
            if (userData.NoteChange)
            {
                NewSign.SetActive(true);
            }
        }

        if (userData.clear03 && userData.stage3_Score >= 3)
        {
            Stamp[2].SetActive(true);
            Btn[2].SetActive(true);
            NoText[2].SetActive(false);
            if (userData.NoteChange)
            {
                NewSign.SetActive(true);
            }
        }

        if (userData.clear04 && userData.stage4_Score >= 3)
        {
            Stamp[3].SetActive(true);
            Btn[3].SetActive(true);
            NoText[3].SetActive(false);
            if (userData.NoteChange)
            {
                NewSign.SetActive(true);
            }
        }

        if (userData.clear05 && userData.stage5_Score >= 3)
        {
            Stamp[4].SetActive(true);
            Btn[4].SetActive(true);
            NoText[4].SetActive(false);
            if (userData.NoteChange)
            {
                NewSign.SetActive(true);
            }
        }

        if (userData.clear06 && userData.stage6_Score >= 3)
        {
            Stamp[5].SetActive(true);
            Btn[5].SetActive(true);
            NoText[5].SetActive(false);
            if (userData.NoteChange)
            {
                NewSign.SetActive(true);
            }
        }
    }

    public void NoteButton()
    {
        //DataBaseSave.instance.writeNoteData_Change(AnonymousLogin.user.UserId, false);
        UserData.instance.SetUserDataInfo("NoteChange", false);

        backBtn.SetActive(true);

        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        NoteScreen.SetActive(true);
        DiaryBtn.SetActive(false);

        NewSign.SetActive(false);

        PlayerTrigger.Instance.isNoteOrDiaryOn = true;
    }

    public void BackButton()
    {
        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        NoteScreen.SetActive(false);
        DiaryBtn.SetActive(true);

        PlayerTrigger.Instance.isNoteOrDiaryOn = false;

        backBtn.SetActive(false);
    }
}
