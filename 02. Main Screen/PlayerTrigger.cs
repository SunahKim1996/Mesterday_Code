using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerTrigger : MonoBehaviour
{
    //[HideInInspector] public bool isNoteOrDiaryOn = false; //TODO

    UserDataInfo userData;
    Coroutine exploreCor = null;

    [SerializeField] Camera playerCam;

    [Header("Other Manager")]
    [SerializeField] SpeechBubbleManager speechBubbleManager;
    [SerializeField] DialogManager dialogManager;

    [Header("Obj List")]
    [SerializeField] GameObject playerWall_1;
    [SerializeField] GameObject playerWall_2;

    [SerializeField] List<GameObject> tutorialObjList = new List<GameObject>();

    void Start()
    {
        userData = UserData.instance.userData;
        ToggleTutorialObjList(false);
    }

    void ToggleTutorialObjList(bool state)
    {
        for (int i = 0; i < tutorialObjList.Count; i++)
            tutorialObjList[i].SetActive(state);
    }


    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "TutorialPoint_1":
                StartObjectTutorial();
                break;

            case "TutorialPoint_2":
                //TODO
                //GameObject.Find("GameManager").GetComponent<TutorialConversation2_2>().isTutorial2On = true;

                if (userData.clearTutorial)
                    return;

                if (exploreCor != null)
                {
                    StopCoroutine(exploreCor);
                    exploreCor = null;
                }

                dialogManager.StartChat();
                break;

            case "wall":
                string[] dialogList = { "더 이상 앞으로 갈 수 없어" };
                speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                break;
        }                   
    }

    void StartObjectTutorial()
    {
        playerWall_2.SetActive(false);
        playerWall_1.SetActive(false);

        if (userData.clearTutorial)
            return;

        exploreCor = StartCoroutine(Explore());
        ToggleTutorialObjList(true);

        string[] dialogList =
        {
            "주변에 물건이 많네",
            "한번 손을 대볼까?"
        };
        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
    }

    IEnumerator Explore()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                if (!speechBubbleManager.isTalking)
                {
                    RaycastHit hit;
                    Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);

                    string hitTagName = hit.collider.gameObject.tag;
                    ShowObjectTutorial(hit.collider.gameObject, hitTagName);
                }         
            }
        }
    }

    void ShowObjectTutorial(GameObject targetObj, string tagName)
    {
        List<string> dialogList = new List<string>();

        switch (tagName)
        {
            case ("chair"):
                dialogList.Add("의자다");
                dialogList.Add("지옥같은 강의실 의자에 앉다가\n오랜만에 보니 반가운걸");
                break;

            case ("desk"):
                dialogList.Add("책상이다");
                dialogList.Add("고등학교 때 쓰던 것 같이 생겼다");
                break;

            case ("notice"):
                dialogList.Add("알림판이다");
                dialogList.Add($"- {userData.schoolName} 고등학교 제 40회 영어 경시대회 참가자 모집 - ");
                dialogList.Add($"{userData.schoolName} 고등학교는 내가 나온 학교인데. . .");
                break;

            case ("mirror"):
                dialogList.Add("전신 거울이다");
                dialogList.Add("자세히 보니 나는 교복을 입고,\n삼선 슬리퍼를 신고 있다");
                break;

            case ("book"):
                dialogList.Add($"- 고등학교 1학년 {userData.nickName} -");
                dialogList.Add("내가 공부하던 수학 교과서인 것 같다");
                break;

            default:
                return;
        }

        SoundManager.instance.PlaySFX(SoundClip.getSFX_2, 0.4f);
        targetObj.transform.GetChild(0).gameObject.SetActive(false);

        speechBubbleManager.StartSpeechBubbleGuide(dialogList.ToArray());
    }
}
