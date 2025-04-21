using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation05_After : MonoBehaviour
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

        starScore_Pre = userData.stage6_Score;

        //PlayerPrefs.SetInt("PlayerWhere", 5);
        UserData.instance.SetUserDataInfo("playerPos", 6);

        /*
        if (PlayerPrefs.HasKey("06_Clear") == false)
        {
            PlayerPrefs.SetInt("stage5_Score", 0);
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

            if (GetComponent<CameraMoving05>().isMainCamOpen == true && GetComponent<CameraMoving05>().distance2 < 0.5f)
            {
                AddTalkData();

                Chat.gameObject.SetActive(true);
                chatBox.SetActive(true);

                StartCoroutine(GoNextConversation());
                StartCoroutine(ChatConversation(talkData[0], 0.1f));
                
                //PlayerPrefs.SetInt("06_Clear", 1);
                //DataBaseSave.instance.writeClearData_6(AnonymousLogin.user.UserId, true);
                UserData.instance.SetUserDataInfo("clear06", true);

                /*
                if (PlayerPrefs.GetInt("stage5_Score") <= GetComponent<StarScore05>().howMuchStarScore)
                {
                    PlayerPrefs.SetInt("stage5_Score", GetComponent<StarScore05>().howMuchStarScore);
                    DataBaseSave.instance.writeNewScore_4(AnonymousLogin.user.UserId, GetComponent<StarScore05>().howMuchStarScore);
                }
                */

                if (starScore_Pre < GetComponent<StarScore05>().howMuchStarScore)
                {
                    //DataBaseSave.instance.writeNewScore_6(AnonymousLogin.user.UserId, GetComponent<StarScore05>().howMuchStarScore);
                    UserData.instance.SetUserDataInfo("stage6_Score", GetComponent<StarScore05>().howMuchStarScore);

                    if (GetComponent<StarScore05>().howMuchStarScore == 3)
                    {
                        //DataBaseSave.instance.writeNoteData_Change(AnonymousLogin.user.UserId, true);
                        UserData.instance.SetUserDataInfo("NoteChange", true);
                    }
                }

                break;
            }
        }
    }

    void AddTalkData()
    {
        if (GetComponent<StarScore05>().howMuchStarScore > 1)
        {
            talkData.Add("계단이 친구가 있는 곳까지 연결됐다!");
            talkData.Add("여길 올라가면 친구를 잡을 수 있어");
            talkData.Add("고등학교 땐 그러지 못했지만, 이번엔 친구의 말을 들어주자");
            talkData.Add("꿈인게 아쉽다.");
        }
        else if (GetComponent<StarScore05>().howMuchStarScore == 1)
        {
            talkData.Add("계단이 친구가 있는 곳까지 연결됐다!");
            talkData.Add("조금만 더 걸렸으면 친구가 가버릴 뻔 했어.");
            talkData.Add("이제 여길 올라가면 친구를 잡을 수 있을거야");
            talkData.Add("고등학교 땐 그러지 못했지만, 이번엔 친구의 말을 들어주자");
            talkData.Add("꿈인게 아쉽다.");
        }
        else if (GetComponent<StarScore05>().howMuchStarScore == 0)
        {
            talkData.Add("계단을 연결하고 있는 사이에 친구가 가버렸다.");
            talkData.Add("꿈에서라도 너와 다시 이야기 해보고 싶었는데. . .");
            talkData.Add("아쉽다.");
            talkData.Add("너는 지금 어떻게 지내고 있을까");
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
                            Chat.gameObject.SetActive(false);
                            chatBox.SetActive(false);

                            yield return new WaitForSeconds(0.3f);

                            //PlayerPrefs.SetInt("PlayerWhere", 5);
                            UserData.instance.SetUserDataInfo("playerPos", 6);

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
