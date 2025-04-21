using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    static PlayerTrigger _instance;

    public static PlayerTrigger Instance
    {
        get { return _instance; }
    }

    public GameObject speechBubble;
    public Text speechTxt;
    public Camera playerCam;

    public Text goStageTxt;

    int wherePlayer;

    //string schoolName;
    //string nickName;

    public AudioSource soundEffect;
    public AudioClip doorEffect;
    public AudioClip doorLockedEffect;
    public AudioClip getEffect_2;

    public bool isNoteOrDiaryOn = false;

    private UserDataInfo userData;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        userData = UserData.instance.userData;

        isDoorOpened = 0;
        //schoolName = PlayerPrefs.GetString("schoolName");
        //nickName = PlayerPrefs.GetString("nickName");

        for (int i = 0; i < objectPoint.Count; i++)
        {
            objectPoint[i].SetActive(false);
        }
    }

    public static int isDoorOpened = 0; // 0: 처음 상태 , 1: 문 열림,  2: 문 잠김
    Vector3 lookAtPos;

    private void Update()
    {
        //Debug.Log(wherePlayer);
        speechBubble.GetComponent<RectTransform>().position
            = playerCam.WorldToScreenPoint(GameObject.Find("Player").GetComponent<Transform>().position + new Vector3(0f, 0.9f, 0f));


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (isNoteOrDiaryOn == false)
            {
                if (hit.collider != null)
                {
                    lookAtPos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);

                    if (wherePlayer == 1)
                    {
                        if (hit.collider.gameObject.transform.tag == "stage1Door")
                        {
                            DoorSound();
                            this.GetComponent<Animator>().SetInteger("anim", 3);
                            if (userData.clearTutorial)
                            {
                                DoorSound();
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                            }

                            // Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, null, 1));
                        }
                    }
                    else if (wherePlayer == 2)
                    {
                        if (hit.collider.gameObject.transform.tag == "stage2Door")
                        {
                            if (userData.clear01 && userData.stage1_Score != 0)
                            {
                                DoorSound();
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                            }

                            //Vector3 Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, userData.clear01.ToString(), userData.stage1_Score));
                        }
                    }
                    else if (wherePlayer == 3)
                    {
                        if (hit.collider.gameObject.transform.tag == "stage3Door")
                        {

                            if (userData.clear02 && userData.stage2_Score != 0)
                            {
                                DoorSound();
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                            }

                            //Vector3 Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, userData.clear02.ToString(), userData.stage2_Score));
                        }
                    }
                    else if (wherePlayer == 4)
                    {
                        if (hit.collider.gameObject.transform.tag == "stage4Door")
                        {
                            if (userData.clear03 && userData.stage3_Score != 0)
                            {
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                                DoorSound();
                            }

                            //Vector3 Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, userData.clear03.ToString(), userData.stage3_Score));
                        }
                    }
                    else if (wherePlayer == 5)
                    {
                        if (hit.collider.gameObject.transform.tag == "stage5Door")
                        {
                            if (userData.clear04 && userData.stage4_Score != 0)
                            {
                                DoorSound();
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                            }

                            //Vector3 Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, userData.clear04.ToString(), userData.stage4_Score));
                        }
                    }
                    else if (wherePlayer == 6)
                    {
                        if (hit.collider.gameObject.transform.tag == "stage6Door")
                        {
                            if (userData.clear05 && userData.stage5_Score != 0)
                            {
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                                DoorSound();
                            }

                            //Vector3 Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, userData.clear05.ToString(), userData.stage5_Score));
                        }
                    }
                    else if (wherePlayer == 7)
                    {
                        if (hit.collider.gameObject.transform.tag == "endingDoor")
                        {
                            if (userData.clear06 && userData.stage6_Score != 0)
                            {
                                this.GetComponent<Animator>().SetInteger("anim", 3);
                                DoorSound();
                            }

                            //Vector3 Pos = new Vector3(hit.collider.gameObject.transform.position.x, transform.position.y, hit.collider.gameObject.transform.position.z);
                            //transform.LookAt(Pos);
                            StartCoroutine(OpenDoorLogic(hit.collider.gameObject, wherePlayer, userData.clear06.ToString(), userData.stage6_Score));
                        }
                    }
                }
            }                
        }
    }

    void DoorSound()
    {
        soundEffect.PlayOneShot(doorEffect);
        soundEffect.volume = SoundManager.doorEffect;
    }

    IEnumerator OpenDoorLogic(GameObject door, int wherePlayer, string clearData, int preStageStarPoint)
    {
        while (true)
        {
            yield return null;

            if (clearData == "True" || clearData == null)
            {
                if (preStageStarPoint == 0)
                {
                    isDoorOpened = 2;
                    transform.LookAt(lookAtPos);

                    soundEffect.PlayOneShot(doorLockedEffect);
                    soundEffect.volume = SoundManager.doorLockedEffect;

                    speechTxt.text = "이전 문제를 제대로 해결해야 해";
                    speechBubble.SetActive(true);
                    yield return new WaitForSeconds(2f);
                    speechBubble.SetActive(false);

                    isDoorOpened = 0;

                    break;
                }
                else
                {
                    isDoorOpened = 1;
                    transform.LookAt(lookAtPos);

                    Quaternion targetRotation = Quaternion.Euler(-90, 0, 90f);
                    door.transform.parent.gameObject.transform.localRotation = Quaternion.Slerp(door.transform.parent.gameObject.transform.localRotation, targetRotation, Time.deltaTime);


                    goStageTxt.gameObject.SetActive(true);

                    if (door.transform.parent.gameObject.transform.localRotation == Quaternion.Euler(-90, 0, 85f))
                    {

                        if (wherePlayer == 1)
                        {
                            LoadingManager.LoadScene(3);
                            //SceneManager.LoadScene("02. Tutorial Game");
                        }
                        else if (wherePlayer == 2)
                        {
                            LoadingManager.LoadScene(4);
                            //SceneManager.LoadScene(4); 
                        }
                        else if (wherePlayer == 3)
                        {
                            LoadingManager.LoadScene(5);
                            //SceneManager.LoadScene(5);
                        }
                        else if (wherePlayer == 4)
                        {
                            LoadingManager.LoadScene(6);
                            //SceneManager.LoadScene(7);
                        }
                        else if (wherePlayer == 5)
                        {
                            LoadingManager.LoadScene(7);
                            //SceneManager.LoadScene(6);
                        }
                        else if (wherePlayer == 6)
                        {
                            LoadingManager.LoadScene(8);
                        }
                        else if (wherePlayer == 7)
                        {
                            LoadingManager.LoadScene(9);
                        }
                    }
                }
                
            }
            else if(clearData == "False")
            {
                isDoorOpened = 2;

                soundEffect.PlayOneShot(doorLockedEffect);
                soundEffect.volume = SoundManager.doorLockedEffect;

                speechTxt.text = "지금은 들어갈 수 없어";
                speechBubble.SetActive(true);
                yield return new WaitForSeconds(2f);
                speechBubble.SetActive(false);

                isDoorOpened = 0;

                break;
            }
            

        }      
    }

    public GameObject playerWall_1;
    public GameObject playerWall_2;

    public List<GameObject> objectPoint = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name == "TutorialPoint_1")
        {
            playerWall_2.SetActive(false);
            playerWall_1.SetActive(false);

            if (userData.clearTutorial == false)
            {
                StartCoroutine(Tutorial_1());

                for(int i = 0; i < objectPoint.Count; i++)
                {
                    objectPoint[i].SetActive(true);
                }                
            }
        }
        else if (other.transform.tag == "tutorial")
        {
            GameObject.Find("GameManager").GetComponent<TutorialConversation2_2>().isTutorial2On = true;

            if (userData.clearTutorial == false)
            {                
                StartCoroutine(DoorTutorial());
            }            
        }       
        
        if(other.transform.tag == "stage1")
        {
            wherePlayer = 1;
        }
        else if (other.transform.tag == "stage2")
        {
            wherePlayer = 2;
        }
        else if (other.transform.tag == "stage3")
        {
            wherePlayer = 3;
        }
        else if (other.transform.tag == "stage4")
        {
            wherePlayer = 4;
        }
        else if (other.transform.tag == "stage5")
        {
            wherePlayer = 5;
        }
        else if (other.transform.tag == "stage6")
        {
            wherePlayer = 6;
        }
        else if (other.transform.tag == "wall")
        {
            StartCoroutine(NormalSpeech("더 이상 앞으로 갈 수 없어"));
        }
        else if (other.transform.tag == "endingPoint")
        {
            wherePlayer = 7;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "stage1" || other.transform.tag == "stage2" || other.transform.tag == "stage3" || other.transform.tag == "stage4" || other.transform.tag == "stage5" || other.transform.tag == "stage6")
        {
            wherePlayer = 0;
        }
    }

    IEnumerator Tutorial_1()
    {
        speechTxt.text = "주변에 물건이 많네";
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(2f);
        speechBubble.SetActive(false);

        speechTxt.text = "한번 손을 대볼까?";
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(2f);
        speechBubble.SetActive(false);

        StartCoroutine(Explore());
    }

    bool isTalking = false;

    void GetSound()
    {
        soundEffect.PlayOneShot(getEffect_2);
        soundEffect.volume = SoundManager.getEffect_2;
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

                if(GameObject.Find("GameManager").GetComponent<TutorialConversation2_2>().isTutorial2On == false)
                {
                    if (hit.collider != null)
                    {

                        if (hit.collider.gameObject.transform.tag == "chair")
                        {

                            if (isTalking == false)
                            {
                                isTalking = true;

                                GetSound();
                                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                                StartCoroutine(NormalSpeech("의자다"));
                                yield return new WaitForSeconds(2f);
                                StartCoroutine(NormalSpeech("지옥같은 강의실 의자에 앉다가" + System.Environment.NewLine + " 오랜만에 보니 반가운걸"));
                                yield return new WaitForSeconds(2f);

                                isTalking = false;
                            }
                        }
                        else if (hit.collider.gameObject.transform.tag == "desk")
                        {
                            if (isTalking == false)
                            {
                                isTalking = true;

                                GetSound();
                                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                                StartCoroutine(NormalSpeech("책상이다"));
                                yield return new WaitForSeconds(2f);
                                StartCoroutine(NormalSpeech("고등학교 때 쓰던 것 같이 생겼다."));
                                yield return new WaitForSeconds(2f);

                                isTalking = false;
                            }
                        }
                        else if (hit.collider.gameObject.transform.tag == "notice")
                        {
                            if (isTalking == false)
                            {
                                isTalking = true;

                                GetSound();
                                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                                StartCoroutine(NormalSpeech("알림판이다."));
                                yield return new WaitForSeconds(2f);
                                StartCoroutine(NormalSpeech("- " + userData.schoolName + " 고등학교 제 40회 영어 경시대회 참가자 모집 - "));
                                yield return new WaitForSeconds(2f);
                                StartCoroutine(NormalSpeech(userData.schoolName + " 고등학교는 내가 나온 학교인데. . ."));
                                yield return new WaitForSeconds(2f);

                                isTalking = false;
                            }
                        }
                        else if (hit.collider.gameObject.transform.tag == "mirror")
                        {
                            if (isTalking == false)
                            {
                                isTalking = true;

                                GetSound();
                                hit.collider.gameObject.transform.parent.transform.GetChild(0).gameObject.SetActive(false);

                                StartCoroutine(NormalSpeech("전신 거울이다."));
                                yield return new WaitForSeconds(2f);
                                StartCoroutine(NormalSpeech("자세히 보니 나는 교복을 입고, " + System.Environment.NewLine + "삼선 슬리퍼를 신고 있다."));
                                yield return new WaitForSeconds(2f);

                                isTalking = false;
                            }
                        }
                        else if (hit.collider.gameObject.transform.tag == "book")
                        {
                            if (isTalking == false)
                            {
                                isTalking = true;

                                GetSound();
                                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                                StartCoroutine(NormalSpeech("- 고등학교 1학년 " + userData.nickName + " -"));
                                yield return new WaitForSeconds(2f);
                                StartCoroutine(NormalSpeech("내가 공부하던 수학 교과서인 것 같다."));
                                yield return new WaitForSeconds(2f);

                                isTalking = false;
                            }
                        }
                    }
                }                
            }
        }
    }

    IEnumerator NormalSpeech(string Txt)
    {
        speechTxt.text = Txt;
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(2f);
        speechBubble.SetActive(false);
    }

    public bool isDoorTutorialStart = false;

    IEnumerator DoorTutorial()
    {
        while (true)
        {
            yield return null;

            if (isDoorTutorialStart == true)
            {
                speechTxt.text = "일단 좀 더 돌아다녀 보는게 좋겠다";
                speechBubble.SetActive(true);
                yield return new WaitForSeconds(2f);
                speechBubble.SetActive(false);

                speechTxt.text = "앞에 문이 엄청 많네";
                speechBubble.SetActive(true);
                yield return new WaitForSeconds(2f);
                speechBubble.SetActive(false);

                speechTxt.text = "문패를 보니 교실인 것 같아";
                speechBubble.SetActive(true);
                yield return new WaitForSeconds(2f);
                speechBubble.SetActive(false);

                speechTxt.text = "가까이 가서 문을 열어보자";
                speechBubble.SetActive(true);
                yield return new WaitForSeconds(2f);
                speechBubble.SetActive(false);


                break;
            }
        }
    }
}
