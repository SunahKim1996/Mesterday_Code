using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerDoorState
{
    None,      // 처음 상태 
    Opening,   // 문 열리는 중 
    Locked,    // 문 잠김
}

public class PlayerDoor : MonoBehaviour
{
    UserDataInfo userData;
    [SerializeField] SpeechBubbleManager speechBubbleManager;

    [HideInInspector] public PlayerDoorState playerDoorState;

    [SerializeField] Camera playerCam;
    [SerializeField] Text goStageTxt;
    [SerializeField] List<Transform> doorList;

    int curDoorIndex;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;
        playerDoorState = PlayerDoorState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerDoorState == PlayerDoorState.Opening)
                return;

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
                    FieldInfo fieldInfo = typeof(UserDataInfo).GetField($"clear0{curDoorIndex - 1}");
                    bool isPreStageClear = (bool)fieldInfo.GetValue(userData);

                    // 이전 스테이지 클리어는 했는데 별점수가 0인 경우, 
                    if (isPreStageClear)
                    {
                        fieldName = $"stage{curDoorIndex - 1}_score";
                        fieldInfo = typeof(UserDataInfo).GetField($"stage{curDoorIndex - 1}_score");
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

                // 해당 스테이지 입장 
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

        while (door.localRotation.z < 0.45f)
        {
            Quaternion targetRot = Quaternion.Euler(-90, 0, 90f);
            door.localRotation = Quaternion.Slerp(door.localRotation, targetRot, Time.deltaTime);

            yield return null;
        }

        SoundManager.instance.StopBGM();
        LoadingManager.LoadScene(curDoorIndex + 2);
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            if (other.tag == $"stage{i}")
            {
                curDoorIndex = i;
                break;
            }
        }

        if (other.tag == "endingPoint")
            curDoorIndex = 7;
    }

    void OnTriggerExit(Collider other)
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
}
