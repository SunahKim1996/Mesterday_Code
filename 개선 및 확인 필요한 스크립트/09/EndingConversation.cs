using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingConversation : MonoBehaviour
{
    public GameObject conversationBox;
    public Text Chat;
    string writerText = "";

    List<string> talkData = new List<string>();

    public GameObject FadeOutImage;

    public GameObject friend;
    public GameObject player;
    public GameObject falledFriend;

    // Start is called before the first frame update
    void Start()
    {
        UserData.instance.SetUserDataInfo("playerPos", 7);

        isConversationOn = true;
        StartCoroutine(FriendMove());

        Chat.text = "";
        StartCoroutine(GoNextConversation());
        AddTalkData();
        
        StartCoroutine(ChatConversation(talkData[0], 0.1f));
    }

    private void Update()
    {
        speechBubble.GetComponent<RectTransform>().position
            = Camera.main.WorldToScreenPoint(player.GetComponent<Transform>().position + new Vector3(-0.2f, 1.1f, 0f));
    }

    IEnumerator FriendMove()
    {
        yield return new WaitForSeconds(8f);

        friend.GetComponent<Animator>().SetInteger("end", 1);

        Vector3 friendPos = new Vector3(falledFriend.transform.position.x, friend.transform.position.y, falledFriend.transform.position.z);
        friend.transform.position = Vector3.MoveTowards(friend.transform.position,friendPos, Time.deltaTime * 1.5f);


        while (true)
        {
            yield return null;

            if(friend.transform.position != falledFriend.transform.position)
            {
                friend.transform.position = Vector3.MoveTowards(friend.transform.position, falledFriend.transform.position, Time.deltaTime * 1.5f);
            }
        }
        
    }

    void AddTalkData()
    {
        talkData.Add("지금의 나는 알고 있다.");
        talkData.Add("성인이 되고 여러가지 일을 겪으면서");
        talkData.Add("친구와 싸웠던 이유가 정말 별 게 아니라는 걸");
        talkData.Add("내가 조금 더 참고 좀 더 친구의 말을 들어줬다면");
        talkData.Add("지금까지도 좋은 사이로 남아있을 거라는 것을 말이다.");
        talkData.Add("고등학교 시절의 나보다는, 지금의 내가 더 많은 것을 경험한 상태니까.");
        talkData.Add("그러고보면 수학 문제도 비슷하다.");
        talkData.Add("문제를 많이 풀어본 만큼 그 다음의 문제를 푸는 데 능숙해질 수 있다.");
        talkData.Add("이런거 깨달으라고 그렇게 싫었던 수학을 배웠던 걸까.");
        talkData.Add("비록 꿈이라서, 현실로 돌아가면 친구와 다시 멀어진 상태가 되겠지만");
        talkData.Add("그래도 무언가를 하나 깨닫고 가서 마음 한켠이 편해졌다.");
        talkData.Add("꿈에서 깨면. . .");
        talkData.Add("친구에게 연락을 해봐야겠다.");
    }

    IEnumerator ChatConversation(string narration, float speed)
    {
        isTalking = true;

        int a = 0;
        writerText = "";

        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            Chat.text = writerText;
            yield return new WaitForSeconds(speed);
            
            if(isStop == true)
            {
                isStop = false;
                break;
            }
        }

        isTalking = false;
    }

    int talkNum = 0;
    bool isTalking = false;

    public static bool isConversationOn = true;
    bool isStop = false;

    IEnumerator GoNextConversation()
    {
        while(true)
        {
            yield return null;

            if(Input.GetMouseButtonDown(0))
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
                        isConversationOn = false;

                        StartCoroutine(HintSpeechBubbleOn());

                        FadeOutImage.SetActive(true);
                        //StartCoroutine(FadeOut());
                        conversationBox.SetActive(false);
                        Chat.gameObject.SetActive(false);

                        StartCoroutine(SpeechBubbleOn());
                        StartCoroutine(FriendPlayerDistance());

                        break;
                    }
                    
                }             
            }
        }      
        
    }

    public GameObject speechBubble;
    public Text specchBubbleTxt;

    IEnumerator SpeechBubbleOn()
    {        
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(2f);
        speechBubble.SetActive(false);
    }

    IEnumerator HintSpeechBubbleOn()
    {
        while (true)
        {
            yield return null;

            if (TouchPointWhere07.isPlayerKnowWalking == false)
            {
                yield return new WaitForSeconds(7f);

                if(TouchPointWhere07.isPlayerKnowWalking == false)
                {
                    specchBubbleTxt.text = "바닥을 터치해보자";

                    speechBubble.SetActive(true);
                    yield return new WaitForSeconds(2f);
                    speechBubble.SetActive(false);
                }                
            }
        }        
    }

    IEnumerator FriendPlayerDistance()
    {
        while (true)
        {
            yield return null;

            if (Vector3.Distance(friend.transform.position, player.transform.position) < 1f)
            {
                StartCoroutine(FadeOut());

                break;
            }
        }        
    }

    IEnumerator FadeOut()
    {            
        for (int i = 0; i < 11; i++)                       
        {
            Color color = FadeOutImage.GetComponent<Image>().color;

            float f = i / 10.0f;
            color.a = f;
            FadeOutImage.GetComponent<Image>().color = color;

            PlayerPrefs.SetInt("01_Clear", 1);

            yield return new WaitForSeconds(0.1f);
        }

        //GetComponent<TitleCamera>().isTitleOn = true;
        //PlayerPrefs.SetInt("EndingClear", 1);

        GetComponent<EndingSound>().isCallingStart = true;

        //SceneManager.LoadScene(0);

        yield return null;                                    
    }
}
