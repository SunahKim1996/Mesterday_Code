using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public static bool isDiaryOn = false;
    UserDataInfo userData;

    [SerializeField] GameObject diaryUI;
    [SerializeField] List<GameObject> startButtonList;

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

        isDiaryOn = state;
    }
}
