using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation02_After : MonoBehaviour
{  

    public Text Chat;
    public GameObject chatBox;

    string writerText = "";

    List<string> talkData = new List<string>();

    public bool isStartPuzzleGame = false;

    public AudioSource soundEffect;
    public AudioClip ConverEffect;

    int starScore_Pre;

    private UserDataInfo userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;

        starScore_Pre = userData.stage1_Score;

        //PlayerPrefs.SetInt("PlayerWhere", 1);
        //PlayerPrefs.SetInt("TutorialClear", 1); // 0: 아직 클리어 못함  1 : 클리어 했음 
        //UserData.instance.SetUserDataInfo("playerPos", 1);
        UserData.instance.SetUserDataInfo("playerPos", 1);

        /*
        if(PlayerPrefs.HasKey("02_Clear") == false)
        {
            PlayerPrefs.SetInt("stage1_Score", 0);
        } 
        */

        Chat.text = "";

        StartCoroutine(Wait());
    }


    IEnumerator Wait() //퍼즐 게임 끝날 때까지 기다림
    {
        while (true)
        {
            yield return null;

            if (GetComponent<CameraMoving02>().isMainCamOpen == true)
            {
                if(GetComponent<CameraMoving02>().distance2 < 0.5f)
                {
                    AddTalkData();

                    Chat.gameObject.SetActive(true);
                    chatBox.SetActive(true);

                    StartCoroutine(GoNextConversation());
                    StartCoroutine(ChatConversation(talkData[0], 0.1f));
                       
                    //PlayerPrefs.SetInt("02_Clear", 1);
                    //DataBaseSave.instance.writeClearData_1(AnonymousLogin.user.UserId, true);
                    UserData.instance.SetUserDataInfo("clear01", true);

                    /*
                    if (PlayerPrefs.GetInt("stage1_Score") <= GetComponent<StarScore02>().howMuchStarScore)
                    {
                        PlayerPrefs.SetInt("stage1_Score", GetComponent<StarScore02>().howMuchStarScore);
                    }
                    */


                    if (starScore_Pre < GetComponent<StarScore02>().howMuchStarScore)
                    {
                        //DataBaseSave.instance.writeNewScore_1(AnonymousLogin.user.UserId, GetComponent<StarScore02>().howMuchStarScore);
                        UserData.instance.SetUserDataInfo("stage1_Score", GetComponent<StarScore02>().howMuchStarScore);

                        if (GetComponent<StarScore02>().howMuchStarScore == 3)
                        {
                            //DataBaseSave.instance.writeNoteData_Change(AnonymousLogin.user.UserId, true);
                            UserData.instance.SetUserDataInfo("NoteChange", true);
                        }
                    }
                    

                    break;
                }                
            }
        }
    }

    void AddTalkData()
    {
        if (GetComponent<StarScore02>().howMuchStarScore >= 1)
        {
            talkData.Add("이상한 함수 그래프 같은 걸 해결하고 나니, 테이프 위치가 정상적으로 돌아왔다");
            talkData.Add("덕분에 엉망이 된 급식줄 문제가 해결됐다");
            talkData.Add("어떻게 된건지 모르겠지만, 마법사가 된 기분이다.");
            talkData.Add("이미 잠들어 버린거");
            talkData.Add("조금만 더 꿈 속을 즐기다 가도 괜찮을 것 같아.");
        }
        else if (GetComponent<StarScore02>().howMuchStarScore == 0)
        {
            talkData.Add("벌써 점심 시간이 끝나버렸다.");
            talkData.Add("머뭇거리다가 테이프를 제대로 돌려놓지 못했다.");
            talkData.Add("결국 순서가 잘 지켜지지 않아서 2학년 급식이 끝나기도 전에 1학년이 도착해 버렸다.");
            talkData.Add("꿈이라도 문제 해결은 쉽지 않네");
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

            if(GetComponent<PauseButton>().isPauseScreenOn == false)
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
                            Chat.gameObject.SetActive(false);
                            chatBox.SetActive(false);

                            yield return new WaitForSeconds(1f);

                            //PlayerPrefs.SetInt("PlayerWhere", 1);
                            UserData.instance.SetUserDataInfo("playerPos", 1);

                            GetComponent<GoToMainScene>().isReadyToGoToMain = true;
                            //SceneManager.LoadScene("02. Tutorial 2");

                            break;
                        }

                    }
                }
            }
            
        }

    }
}
