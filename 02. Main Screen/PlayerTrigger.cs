using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum  PlayerDoorState
{
    None,      // 처음 상태 
    Opening,   // 문 열리는 중 
    Locked,    // 문 잠김
}

public class PlayerTrigger : MonoBehaviour
{
    [HideInInspector] public PlayerDoorState playerDoorState;

    [SerializeField] Camera playerCam;
    [SerializeField] Text goStageTxt;

    int curDoorIndex;

    //[HideInInspector] public bool isNoteOrDiaryOn = false; //TODO

    [SerializeField] GameObject playerWall_1;
    [SerializeField] GameObject playerWall_2;

    [SerializeField] List<GameObject> tutorialObjList = new List<GameObject>();
    [SerializeField] List<Transform> doorList;

    [SerializeField] SpeechBubbleManager speechBubbleManager;
    [SerializeField] DialogManager dialogManager;
    UserDataInfo userData;


    void Start()
    {
        userData = UserData.instance.userData;
        playerDoorState = PlayerDoorState.None;

        ToggleTutorialObjList(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider == null)
                return;

            string hitTagName = hit.collider.gameObject.tag;

            if (hitTagName == $"stage{curDoorIndex}Door")
            {
                Vector3 lookAtPos = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.LookAt(lookAtPos);

                bool isCanOpenDoor = true;

                if (curDoorIndex > 1)
                {
                    string fieldName = $"clear0{curDoorIndex - 1}";
                    FieldInfo fieldInfo = typeof(UserData).GetField($"clear0{curDoorIndex - 1}");
                    bool isPreStageClear = (bool)fieldInfo.GetValue(userData);

                    // 이전 스테이지 클리어는 했는데 별점수가 0인 경우, 
                    if (isPreStageClear)
                    {
                        fieldName = $"stage{curDoorIndex - 1}_score";
                        fieldInfo = typeof(UserData).GetField($"stage{curDoorIndex - 1}_score");
                        int starScore = (int)fieldInfo.GetValue(userData);

                        if (starScore <= 0)
                        {
                            isCanOpenDoor = false;
                            string errorMsg = "이전 문제를 제대로 해결해야 해";
                            StartCoroutine(LockedDoor(errorMsg));
                        }
                    }

                    // 이전 스테이지 클리어 못한 경우,
                    else
                    {
                        isCanOpenDoor = false;
                        string errorMsg = "지금은 들어갈 수 없어";
                        StartCoroutine(LockedDoor(errorMsg));
                    }                    
                }

                if (isCanOpenDoor)
                {
                    playerDoorState = PlayerDoorState.Opening;
                    SoundManager.instance.PlaySFX(SoundClip.doorSFX);
                    GetComponent<Animator>().SetInteger("anim", 3);

                    goStageTxt.gameObject.SetActive(true);

                    StartCoroutine(OpenDoor());
                }
            }
        }
    }
    void ToggleTutorialObjList(bool state)
    {
        for (int i = 0; i < tutorialObjList.Count; i++)
            tutorialObjList[i].SetActive(state);
    }

    IEnumerator LockedDoor(string errorText)
    {
        playerDoorState = PlayerDoorState.Locked;
        SoundManager.instance.PlaySFX(SoundClip.doorLockedSFX, 0.4f);
        GetComponent<Animator>().SetInteger("anim", 0);

        string[] dialogList = { errorText };
        speechBubbleManager.StartSpeechBubbleGuide(dialogList);

        yield return new WaitForSeconds(2f);
        playerDoorState = PlayerDoorState.None;
    }
    
    IEnumerator OpenDoor()
    {
        Transform door = doorList[curDoorIndex - 1];

        while (door.localRotation.z < 85f)
        {
            Quaternion targetRot = Quaternion.Euler(-90, 0, 90f);
            door.localRotation = Quaternion.Slerp(door.localRotation, targetRot, Time.deltaTime);
                        
            yield return null;
        }

        LoadingManager.LoadScene(curDoorIndex + 2);
    }

    void StartTutorialPoint_1()
    {
        playerWall_2.SetActive(false);
        playerWall_1.SetActive(false);

        if (userData.clearTutorial)
            return;

        StartCoroutine(Explore());
        ToggleTutorialObjList(true);

        string[] dialogList =
        {
            "주변에 물건이 많네",
            "한번 손을 대볼까?"
        };
        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "TutorialPoint_1":
                StartTutorialPoint_1();
                break;

            case "tutorial":
                //TODO
                //GameObject.Find("GameManager").GetComponent<TutorialConversation2_2>().isTutorial2On = true;

                if (userData.clearTutorial == false)
                    dialogManager.RestartChat();
                break;

            case "wall":
                string[] dialogList = { "더 이상 앞으로 갈 수 없어" };
                speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                break;

            case "endingPoint":
                curDoorIndex = 7;
                break;

        }



        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            if (other.tag == $"stage{i}")
            {
                curDoorIndex = i;
                break;
            }                
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            if (other.transform.tag == $"stage{i}")
            {
                curDoorIndex = 0;
                break;
            }
        }
    }

    

    bool isTalking = false;

    void GetSound()
    {
        SoundManager.instance.PlaySFX(SoundClip.getSFX_2, 0.4f);
    }

    void Common(GameObject targetObj)
    {
        SoundManager.instance.PlaySFX(SoundClip.getSFX_2, 0.4f);
        targetObj.transform.GetChild(0).gameObject.SetActive(false);
    }


    IEnumerator Explore()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);


                string hitTagName = hit.collider.gameObject.tag;

                if (hit.collider != null && !speechBubbleManager.isTalking)
                {                    
                    if (hitTagName == "chair")
                    {
                        Common(hit.collider.gameObject);

                        string[] dialogList =
                        {
                            "의자다",
                            "지옥같은 강의실 의자에 앉다가" + System.Environment.NewLine + " 오랜만에 보니 반가운걸"
                        };
                        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                    }
                    else if (hitTagName == "desk")
                    {
                        Common(hit.collider.gameObject);

                        string[] dialogList =
                        {
                            "책상이다",
                            "고등학교 때 쓰던 것 같이 생겼다."
                        };
                        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                    }

                    else if (hitTagName == "notice")
                    {
                        Common(hit.collider.gameObject);

                        string[] dialogList =
                        {
                            "알림판이다.",
                            "- " + userData.schoolName + " 고등학교 제 40회 영어 경시대회 참가자 모집 - ",
                            userData.schoolName + " 고등학교는 내가 나온 학교인데. . ."
                        };
                        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                    }
                    else if (hitTagName == "mirror")
                    {
                        Common(hit.collider.gameObject);

                        string[] dialogList =
                        {
                            "전신 거울이다.",
                            "자세히 보니 나는 교복을 입고, " + System.Environment.NewLine + "삼선 슬리퍼를 신고 있다.",
                        };
                        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                    }
                    else if (hitTagName == "book")
                    {
                        Common(hit.collider.gameObject);

                        string[] dialogList =
                        {
                            "- 고등학교 1학년 " + userData.nickName + " -",
                            "내가 공부하던 수학 교과서인 것 같다.",
                        };
                        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                    }
                }
                
            }
        }
    }
}
