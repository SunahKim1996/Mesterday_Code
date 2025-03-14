using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Conversation03 : MonoBehaviour
{
    public Text Chat;
    public GameObject chatBox;

    string writerText = "";

    List<string> talkData = new List<string>();

    public bool isStartPuzzleGame = false;

    public AudioSource soundEffect;
    public AudioClip ConverEffect;

    // Start is called before the first frame update
    void Start()
    {
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
        talkData.Add("여긴 강당인가 보네");
        talkData.Add("강당 벽에 이미 지난 학교 행사의 현수막이 걸려있다.");
        talkData.Add("강당 바닥은 . . .");
        talkData.Add("여기저기에 정리되지 않은 공들이 굴러다니고 있다.");
        talkData.Add("체육시간이 끝나면 치워야하는데, 다들 그냥 반으로 돌아간 모양이야.");
        talkData.Add("체육 선생님이 보시면 그 반은 다음 시간에 운동장을 뛰어야 할지도 모른다.");
        talkData.Add("하는 수 없지, 내가 공을 치워야겠다.");
        talkData.Add("공은 멈춰있을 때 잡아야 해");
        talkData.Add("그렇지 않으면 공을 밟고 넘어질지도 몰라");
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
                            GetComponent<CameraMoving03>().isCamMoving = true;

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
