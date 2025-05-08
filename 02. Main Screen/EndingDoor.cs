using System.Collections;
using UnityEngine;

public class EndingDoor : MonoBehaviour
{
    [SerializeField] GameObject[] endingObj;

    [SerializeField] GameObject player;
    [SerializeField] SpeechBubbleManager speechBubbleManager;

    UserDataInfo userData;

    void Start()
    {
        userData = UserData.instance.userData;

        if (userData.clear06 && userData.stage6_Score > 0)//1-6 스테이지 별 1개 이상으로 클리어
        {
            ToggleEndingObj(true);

            if (userData.EndingOpen)
            {
                Vector3 targetPos = endingObj[0].transform.position;
                targetPos.z = 0f;
                player.transform.LookAt(targetPos);

                string[] dialogList = { "새로운 문이 나타났어 !" };
                speechBubbleManager.StartSpeechBubbleGuide(dialogList);

                UserData.instance.SetUserDataInfo("EndingOpen", true);
            }
        }
        else
            ToggleEndingObj(false);
    }

    void ToggleEndingObj(bool state)
    {
        for (int i = 0; i < endingObj.Length; i++)
            endingObj[i].SetActive(state);
    }
}
