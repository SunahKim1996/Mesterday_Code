using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class StarScoreManager : MonoBehaviour
{
    UserDataInfo userData;

    [SerializeField] List<GameObject> stageStarList;
    [SerializeField] List<GameObject> diaryStarsList;

    [SerializeField] Material clearStarMat;
    [SerializeField] Material noClearStarMat;

    void Start()
    {
        userData = UserData.instance.userData;
        ShowStarPoint();
    }

    /// <summary>
    /// 별점수 보이기
    /// </summary>
    void ShowStarPoint()
    {
        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            string fieldName = $"clear0{i}";
            FieldInfo fieldInfo = typeof(UserDataInfo).GetField(fieldName);

            if (fieldInfo == null)
                continue;

            bool isClear = (bool)fieldInfo.GetValue(userData);
            stageStarList[i - 1].SetActive(isClear);

            if (isClear)
                RefreshStarPoint();
        }
    }

    /// <summary>
    /// 별점수 갱신 
    /// </summary>
    void RefreshStarPoint()
    {
        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            string fieldName = $"stage{i}_Score";
            FieldInfo fieldInfo = typeof(UserDataInfo).GetField(fieldName);

            if (fieldInfo == null)
                continue;

            int starScore = (int)fieldInfo.GetValue(userData);

            // 문 위 별 점수 갱신 
            List<GameObject> doorStarList = stageStarList[i - 1].GetComponentsInChildren<GameObject>().ToList();
            doorStarList.Remove(stageStarList[i - 1]);

            for (int j = 0; j < doorStarList.Count; j++)
                doorStarList[j].GetComponent<Renderer>().material = (j < starScore) ? clearStarMat : noClearStarMat;

            // 다이어리 별 점수 갱신
            List<GameObject> diaryStarList = diaryStarsList[i - 1].GetComponentsInChildren<GameObject>().ToList();
            diaryStarList.Remove(diaryStarsList[i - 1]);

            for (int j = 0; j < diaryStarList.Count; j++)
                diaryStarList[j].GetComponent<Renderer>().material = (j < starScore) ? clearStarMat : noClearStarMat;
        }
    }
}
