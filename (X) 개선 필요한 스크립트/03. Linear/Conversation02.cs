using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Conversation02 : MonoBehaviour
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
        talkData.Add("문을 열고 들어왔더니 급식실 앞에 도착했다.");
        talkData.Add("학생들이 질서 없이 서 있어서 앞으로 나아갈 수가 없다.");
        talkData.Add("급식실 앞에는 학생들이 줄을 제대로 설 수 있게 색깔 테이프가 붙여져 있었는데");
        talkData.Add("도대체 무슨 일인건지, 급식실 안으로 향해야 하는 테이프가 다른 곳을 향하고 있었다.");
        talkData.Add("마침 급식줄 감독해주시는 선생님도 출장이라 급식실 앞이 더 정신이 없는 것 같다.");
        talkData.Add("고등학교 시절의 나였다면 한숨 한번 쉬고 아무렇게나 순서를 기다렸을텐데");
        talkData.Add("꿈이니까");
        talkData.Add("좀 나서봐도 괜찮지 않을까");
        talkData.Add("흐트러진 급식줄이 정리될 수 있도록 테이프 위치를 바꾸는게 좋을 것 같다.");

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
                            GetComponent<CameraMoving02>().isCamMoving = true;

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
