using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class StarScoreManager : MonoBehaviour
{
    UserDataInfo userData;

    [SerializeField] List<GameObject> stageStarList;

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
            string fieldName = $"stage{i}_score";
            FieldInfo fieldInfo = typeof(UserDataInfo).GetField(fieldName);

            if (fieldInfo == null)
                continue;

            int starScore = (int)fieldInfo.GetValue(userData);
            List<GameObject> starList = stageStarList[i - 1].GetComponentsInChildren<GameObject>().ToList();
            starList.Remove(stageStarList[i - 1]);

            for (int j = 0; j < starList.Count; j++)
                starList[j].GetComponent<Renderer>().material = (j < starScore) ? clearStarMat : noClearStarMat;
        }

        //TODO: 다이어리도 해야함
    }
}
