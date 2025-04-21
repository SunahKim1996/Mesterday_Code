using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Conversation04 : MonoBehaviour
{
    public Text Chat;
    public GameObject chatBox;

    string writerText = "";

    List<string> talkData = new List<string>();

    public bool isStartPuzzleGame = false;

    public AudioSource soundEffect;
    public AudioClip ConverEffect;
    public AudioClip peopleEffect;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect.PlayOneShot(peopleEffect);
        soundEffect.volume = SoundManager.peopleEffect;

        Chat.text = "";
        StartCoroutine(GoNextConversation());
        AddTalkData();

        StartCoroutine(ChatConversation(talkData[0], 0.1f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddTalkData()
    {
        talkData.Add("꿈 속을 탐험하는 동안 체육시간이 되었다.");
        talkData.Add("이번주 체육은 시험이 끝난 기념으로 자유활동이기 때문에 다같이 경도를 하기로 했다.");
        talkData.Add("오랜만이네.");
        talkData.Add("졸업한 이후로 몸쓰는 일을 해본 적이 없는데. . .");
        talkData.Add("일반적으로 경도는 경찰이 도둑들을 잡는 놀이인데");
        talkData.Add("반장이 규칙을 추가하자는 제안을 했다.");
        talkData.Add("반장이 발을 이용해서 흙바닥에 크게 원을 그리더니");
        talkData.Add("경찰은 이 원을 따라서만 움직일 수 있다고 한다.");
        talkData.Add("그리고 가위바위보에 져서 내가 경찰이 되었다.");
        talkData.Add("원래라면 술래는 자신 없지만");
        talkData.Add("꿈 속이라서 그런지 신기한 능력을 쓸 수 있어서 다 잡을 수 있을 것 같다.");

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

            if (GetComponent<PauseButton>().isPauseScreenOn == false)
            {
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
                            GetComponent<CameraMoving04>().isCamMoving = true;

                            Chat.gameObject.SetActive(false);
                            chatBox.SetActive(false);

                            break;
                        }

                    }
                }
            }
            
        }

    }
}
