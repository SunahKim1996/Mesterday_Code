using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialConversation2_2 : MonoBehaviour
{
    public GameObject conversationBox;
    public GameObject txt;

    public Text Chat;
    string writerText = "";

    List<string> talkData = new List<string>();

    public AudioSource soundEffect;
    public AudioClip ConverEffect;

    private UserDataInfo userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;

        Chat.text = "";
        AddTalkData();

        StartCoroutine(Wait());
    }

    public bool isTutorial2On = false;

    IEnumerator Wait()
    {
        while (true)
        {
            yield return null;

            if (isTutorial2On == true)
            {
                if (userData.clearTutorial == false)
                {
                    ConversationBoxOn();

                    TouchPointMoving.isTalking = true;
                    GameObject.Find("Player").GetComponent<Animator>().SetInteger("anim", 0);

                    break;
                }
                else
                {
                    TouchPointMoving.isTalking = false;

                    break;
                }
            }
        }
    }

    void AddTalkData()
    {
        talkData.Add("꿈... 인건가..?");
        talkData.Add("교실, 책상, 고등학교 책...");
        talkData.Add("마치 고등학교 때로 돌아온 것 같은 느낌이다.");
        talkData.Add("어째서 이런 꿈을 꾸고 있는 걸까");
        talkData.Add("그나저나 도대체 언제 잠든거지 ? ");
        talkData.Add("지금 자고 있을 시간 없는데. . .");
        talkData.Add("주말 내로 마쳐야 할 과제도 있고");
        talkData.Add("내일은 알바도 있는데 늦잠자서 늦을 순 없어");
        talkData.Add("얼른 깨어나야 해.");
        talkData.Add("어떻게 하면 깰 수 있는지는 모르겠지만. . .");
    }

    void ConversationBoxOn()
    {
        conversationBox.SetActive(true);
        txt.SetActive(true);

        StartCoroutine(GoNextConversation());
        StartCoroutine(ChatConversation(talkData[0], 0.1f));
    }    

    IEnumerator ChatConversation(string narration, float speed)
    {
        isTalking = true;

        int a = 0;
        writerText = "";

        soundEffect.PlayOneShot(ConverEffect);
        soundEffect.volume = SoundManager.ConverEffect;

        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            Chat.text = writerText;
            yield return new WaitForSeconds(speed);

            if (isStop == true)
            {
                isStop = false;
                break;
            }
        }

        isTalking = false;
    }

    int talkNum = 0;
    bool isTalking = false;
    bool isStop = false;

    IEnumerator GoNextConversation()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                if (isTalking == true)
                {
                    isStop = true;
                    StopCoroutine(ChatConversation(talkData[talkNum], 0.1f));
                    isTalking = false;
                    Chat.text = talkData[talkNum];
                }

                else
                {
                    if (talkNum < (talkData.Count - 1))
                    {
                        talkNum += 1;
                        StartCoroutine(ChatConversation(talkData[talkNum], 0.1f));
                    }
                    else
                    {
                        conversationBox.SetActive(false);
                        txt.SetActive(false);

                        //DataBaseSave.instance.writeClearData_tutorial(AnonymousLogin.user.UserId, true);
                        UserData.instance.SetUserDataInfo("clearTutorial", true);

                        TouchPointMoving.isTalking = false;
                        GameObject.Find("Player").GetComponent<Animator>().SetInteger("anim", 1);

                        GameObject.Find("Player").GetComponent<PlayerTrigger>().isDoorTutorialStart = true;

                        StopAllCoroutines();

                        break;
                    }

                }
            }
        }

    }
}

