using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation04_After : MonoBehaviour
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

        starScore_Pre = userData.stage3_Score;

        //PlayerPrefs.SetInt("PlayerWhere", 3);
        UserData.instance.SetUserDataInfo("playerPos", 3);

        /*
        if (PlayerPrefs.HasKey("04_Clear") == false)
        {
            PlayerPrefs.SetInt("stage3_Score", 0);
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

            if(GetComponent<CameraMoving04>().isMainCamOpen == true && GetComponent<CameraMoving04>().distance2 < 5f)
            {
                AddTalkData();

                Chat.gameObject.SetActive(true);
                chatBox.SetActive(true);

                StartCoroutine(GoNextConversation());
                StartCoroutine(ChatConversation(talkData[0], 0.1f));
                
                //PlayerPrefs.SetInt("04_Clear", 1);
                //DataBaseSave.instance.writeClearData_3(AnonymousLogin.user.UserId, true);
                UserData.instance.SetUserDataInfo("clear03", true);

                /*
                if (PlayerPrefs.GetInt("stage3_Score") <= GetComponent<StarScore04>().howMuchStarScore)
                {
                    PlayerPrefs.SetInt("stage3_Score", GetComponent<StarScore04>().howMuchStarScore);
                    DataBaseSave.instance.writeNewScore_3(AnonymousLogin.user.UserId, GetComponent<StarScore04>().howMuchStarScore);
                }
                */

                if (starScore_Pre < GetComponent<StarScore04>().howMuchStarScore)
                {
                    //DataBaseSave.instance.writeNewScore_3(AnonymousLogin.user.UserId, GetComponent<StarScore04>().howMuchStarScore);
                    UserData.instance.SetUserDataInfo("stage3_Score", GetComponent<StarScore04>().howMuchStarScore);

                    if (GetComponent<StarScore04>().howMuchStarScore == 3)
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
        if(GameObject.Find("r_Player").GetComponent<Player04>().howMuchItem == 0)
        {
            Debug.Log(GameObject.Find("r_Player").GetComponent<Player04>().howMuchItem);
            talkData.Add("한 명도 못잡았다. . .");
            talkData.Add("그래도 오랜만에 뛰었더니 재밌었어");
        }
        else if (GameObject.Find("r_Player").GetComponent<Player04>().howMuchItem == 10)
        {
            talkData.Add("모두 잡았다!");
            talkData.Add("역시 꿈 속이라 그런가");
            talkData.Add("아무리 뛰어도 숨이 차지 않았어");
            talkData.Add("오랜만에 뛰었더니 재밌었다.");
        }
        else if(GameObject.Find("r_Player").GetComponent<Player04>().howMuchItem > 0)
        {
            Debug.Log(GameObject.Find("r_Player").GetComponent<Player04>().howMuchItem);
            talkData.Add(GameObject.Find("r_Player").GetComponent<Player04>().howMuchItem + "명 잡았다!");
            talkData.Add("오랜만에 뛰었더니 재밌었어");
        }

        talkData.Add("흙 덮힌 운동장에서 먼지나게 뛰어본 게 언제인지 모르겠다.");
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

                            //PlayerPrefs.SetInt("PlayerWhere", 3);
                            UserData.instance.SetUserDataInfo("playerPos", 3);

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
