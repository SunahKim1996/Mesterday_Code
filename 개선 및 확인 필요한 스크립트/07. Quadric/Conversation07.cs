using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Conversation07 : MonoBehaviour
{
    public Text Chat;
    public GameObject chatBox;

    string writerText = "";

    List<string> talkData = new List<string>();

    public bool isStartPuzzleGame = false;

    public AudioSource soundEffect;
    public AudioClip ConverEffect;
    public AudioClip jingleEffect;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect.PlayOneShot(jingleEffect);
        soundEffect.volume = SoundManager.jingleEffect;

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
        talkData.Add("점심 시간을 알리는 종소리가 울리자마자 학생들이 교실을 우르르 빠져나갔다.");
        talkData.Add("이 때만 되면 다들 굶주린 사자 같았는데.");
        talkData.Add("점심을 먹고 나면 친구들이랑 학교 주변을 산책했던 기억이 난다.");
        talkData.Add("바쁘고 지치는 학교 생활에 빠질 수 없는 힐링 타임이었지.");
        talkData.Add("그 때를 생각하면서 운동장 쪽으로 산책을 하고 있는데");
        talkData.Add("축구공 하나가 내 발 밑으로 굴러왔다.");
        talkData.Add("고개를 들어보니 체육복을 입은 학생이 공을 던져달라며 손을 흔들고 있었다.");
        talkData.Add("점심을 먹고 남은 시간동안 축구를 하고 있는 모양이다.");
        talkData.Add("공을 저쪽으로 던져주자");
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
                            soundEffect.Stop();

                            GetComponent<CameraMoving07>().isCamMoving = true;

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
