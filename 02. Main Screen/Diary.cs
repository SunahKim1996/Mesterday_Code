using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    UserDataInfo userData;

    [SerializeField] GameObject diaryUI;
    [SerializeField] List<GameObject> startButtonList;

    //HERE: Diary 나 Note 켜져있을 때 ray 안먹게 해야함

    void InitButton()
    {
        for (int i = 0; i < startButtonList.Count; i++)
            startButtonList[i].SetActive(false);
    }

    void Start()
    {
        userData = UserData.instance.userData;

        InitButton();

        if (userData.clearTutorial)
            ShowStartButton(0);
        else
            return;

        //HERE: 이전 거가 CLAER 고 START 이 0 높으면으로 수정해야함 
        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            string fieldName = $"clear0{i}";
            FieldInfo fieldInfo = typeof(UserDataInfo).GetField(fieldName);
            bool isClear = (bool)fieldInfo.GetValue(userData);

            fieldName = $"stage{i}_Score";
            fieldInfo = typeof(UserDataInfo).GetField(fieldName);
            int starScore = (int)fieldInfo.GetValue(userData);

            if (isClear && starScore > 0)
                ShowStartButton(i);
        }
    }

    void ShowStartButton(int index)
    {
        startButtonList[index].SetActive(true);

        int i = index;
        startButtonList[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            //HERE : 버튼 안됨.. 확인 필요 
            LoadingManager.LoadScene(i + 3);
        });
    }

    public void OnToggleDiaryUI(bool state)
    {
        diaryUI.SetActive(state);
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);

        //PlayerTrigger.Instance.isNoteOrDiaryOn = true; //TODO
    }

    /*
    public void BackButton()
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);

        DiaryScreen.SetActive(false);
        NoteBtn.SetActive(true);

        //PlayerTrigger.Instance.isNoteOrDiaryOn = false; //TODO

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
     */
}
