using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialConversation : MonoBehaviour
{
    public Text Chat;
    string writerText = "";

    List<string> talkData = new List<string>();

    public GameObject FadeOutImage;

    public AudioSource soundEffect;
    public AudioClip keyboardEffect;
    public AudioClip sighEffect;
    public AudioClip converEffect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }
    

    IEnumerator Wait()
    {
        while (true)
        {
            yield return null;

            if (GetComponent<InputConversation>().isInPutOver == true)
            {
                Chat.text = "";
                StartCoroutine(GoNextConversation());
                AddTalkData();

                StartCoroutine(ChatConversation(talkData[0], 0.1f));

                break;
            }
        }
    }

    void AddTalkData()
    {
        talkData.Add("[ 누구보다 잘 할 자신 있습니다. ]");
        talkData.Add("[ 저의 역량을 다하여 성실히 임할 것을 ]. . . ");
        talkData.Add(". . . 이게 아냐");
        talkData.Add("너무 뻔한 내용을 쓸 수는 없지");
        talkData.Add("이런 내용이라면 어디서도 안받아줄거야");
        talkData.Add(". . .");
        talkData.Add("졸리다");
        talkData.Add("졸려서 아무 생각도 안나");
        talkData.Add("전공 과제도 아직 못끝냈는데. . .");
        talkData.Add("해야할 일 투성이네");
        talkData.Add(". . .");
        talkData.Add("졸업을 앞 둔 대학생은 바쁘다");
        talkData.Add("알바도 해야 하는데 시간은 없고");
        talkData.Add("회사 서류 심사는 번번이 떨어지고. . .");
        talkData.Add("1학년 때는 고등학교 시절 친구들에게 기대기도 했는데");
        talkData.Add("이제는 다들 바쁜건지, 단톡방이 조용하다");
        talkData.Add("잘 지내고 있을까");
        talkData.Add("할 일이 몰리고, 가망없는 미래 생각으로 답답해질 때면");
        talkData.Add("고등학교 때로 다시 돌아가고 싶다는 생각을 하게 된다");
        talkData.Add("공부는 싫었지만 지금보다는 걱정 없었던 그 때로");
        talkData.Add(". . .");

    }

    IEnumerator ChatConversation(string narration, float speed)
    {
        isTalking = true;

        int a = 0;
        writerText = "";

        if (talkNum < 2)
        {
            soundEffect.PlayOneShot(keyboardEffect);
            soundEffect.volume = SoundManager.keyboardEffect;
        }
        else if (talkNum == 2)
        {
            soundEffect.PlayOneShot(sighEffect);
            soundEffect.volume = SoundManager.sighEffect;
        }
        else
        {
            //soundEffect.PlayOneShot(converEffect);
            //soundEffect.volume = SoundManager.ConverEffect;
        }

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
                        FadeOutImage.SetActive(true);
                        StartCoroutine(FadeOut());

                        break;
                    }
                    
                }             
            }
        }      
        
    }    

    IEnumerator FadeOut()
    {            
        for (int i = 0; i < 10; i++)                       
        {
            Color color = FadeOutImage.GetComponent<Image>().color;

            float f = i / 10.0f;
            color.a = f;
            FadeOutImage.GetComponent<Image>().color = color;

            PlayerPrefs.SetInt("01_Clear", 1);

            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(2);

        yield return null;                                    
    }
}
