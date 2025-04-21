using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialConversation2 : MonoBehaviour
{
    public GameObject conversationBox;
    public GameObject txt;

    public Text Chat;
    string writerText = "";

    public GameObject speechBubble;
    public Text speechBubbleTxt;

    public AudioSource soundEffect;
    public AudioClip ConverEffect;

    List<string> talkData = new List<string>();

    private UserDataInfo userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;

        GameObject.Find("Player").GetComponent<Animator>().SetInteger("anim", 0);

        Chat.text = "";
        AddTalkData();

        if(userData.clearTutorial == false)
        {
            StartCoroutine(ConversationBoxOn());
        }
        else
        {
            TouchPointMoving.isTalking = false;
        }
    }

    void AddTalkData()
    {
        talkData.Add("!");
        talkData.Add("여긴... 어디지..?");
        talkData.Add("주변에 이상한 게 둥둥 떠다녀");
        talkData.Add("분명 방에서 자소서 쓰고 있었는데...");
        talkData.Add("정신을 차려보니 이상한 곳에 서있다");
        talkData.Add("도대체 어떻게 된걸까");

    }

    IEnumerator ConversationBoxOn()
    {
        while(true)
        {
            yield return new WaitForSeconds(13f);

            conversationBox.SetActive(true);
            txt.SetActive(true);

            GameObject.Find("Player").GetComponent<Animator>().SetInteger("anim", 2);

            StartCoroutine(GoNextConversation());
            StartCoroutine(ChatConversation(talkData[0], 0.1f));

            break;

        }
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

                        GameObject.Find("Player").GetComponent<Animator>().SetInteger("anim", 0);

                        TouchPointMoving.isTalking = false;

                        speechBubble.SetActive(true);
                        speechBubbleTxt.text = "일단 좀 돌아다녀 보자";
                        yield return new WaitForSeconds(2f);
                        speechBubble.SetActive(false);

                        yield return new WaitForSeconds(0.1f);

                        speechBubble.SetActive(true);
                        speechBubbleTxt.text = "바닥을 터치하면 " + System.Environment.NewLine + "움직일 수 있는 것 같아";
                        yield return new WaitForSeconds(2f);
                        speechBubble.SetActive(false);

                        StopAllCoroutines();

                        break;
                    }

                }
            }
        }

    }
}
