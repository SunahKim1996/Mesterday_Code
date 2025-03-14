using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Conversation06 : MonoBehaviour
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
        Debug.Log(GetComponent<CameraMoving06>().isCamMoving);
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
        talkData.Add("여긴 방송실이네.");
        talkData.Add("처음 방송실에 들어가봤던 날 선생님께 혼난 기억이 있다.");
        talkData.Add("점심시간 마다 들리던 방송이 그 날은 안들려서 방송실에 가봤었는데");
        talkData.Add("방송반이었던 친구가 기계가 고장났다며 쩔쩔 매고 있었다.");
        talkData.Add("같이 이것 저것 만져보다가 결국 기계를 고장내서 혼이 났었지");
        talkData.Add("지금 꿈 속의 친구는 그 때와 같은 상황에 놓여있는 것 같았다.");
        talkData.Add("이번엔 제대로 도와줄 수 있을 것 같은데.");
        talkData.Add("방송실 기계를 고쳐볼까?");
        talkData.Add("점심시간이 끝나기 전에 고쳐야할 것 같아");
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
                            GetComponent<CameraMoving06>().isCamMoving = true;

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
