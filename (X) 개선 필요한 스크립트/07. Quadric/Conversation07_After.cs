using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation07_After : MonoBehaviour
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

        starScore_Pre = userData.stage5_Score;

        //PlayerPrefs.SetInt("PlayerWhere", 3);
        UserData.instance.SetUserDataInfo("playerPos", 5);

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

            if(GetComponent<CameraMoving07>().isMainCamOpen == true && GetComponent<CameraMoving07>().distance2 < 5f)
            {
                AddTalkData();

                Chat.gameObject.SetActive(true);
                chatBox.SetActive(true);

                StartCoroutine(GoNextConversation());
                StartCoroutine(ChatConversation(talkData[0], 0.1f));
                
                //PlayerPrefs.SetInt("04_Clear", 1);
                //DataBaseSave.instance.writeClearData_5(AnonymousLogin.user.UserId, true);
                UserData.instance.SetUserDataInfo("clear05", true);

                /*
                if (PlayerPrefs.GetInt("stage3_Score") <= GetComponent<StarScore04>().howMuchStarScore)
                {
                    PlayerPrefs.SetInt("stage3_Score", GetComponent<StarScore04>().howMuchStarScore);
                    DataBaseSave.instance.writeNewScore_3(AnonymousLogin.user.UserId, GetComponent<StarScore04>().howMuchStarScore);
                }
                */

                if (starScore_Pre < GetComponent<StarScore07>().howMuchStarScore)
                {
                    //DataBaseSave.instance.writeNewScore_5(AnonymousLogin.user.UserId, GetComponent<StarScore07>().howMuchStarScore);
                    UserData.instance.SetUserDataInfo("stage5_Score", GetComponent<StarScore07>().howMuchStarScore);

                    if (GetComponent<StarScore07>().howMuchStarScore == 3)
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
        if (GetComponent<StarScore07>().howMuchStarScore > 1)
        {
            talkData.Add("축구공을 제대로 던져줄 수 있었다.");
            talkData.Add("건네받은 학생은 고맙다고 소리친 다음 축구를 계속했다.");
            talkData.Add("나는 이제 마저 산책을 해야지.");
        }
        else if (GetComponent<StarScore07>().howMuchStarScore == 1)
        {
            talkData.Add("축구공이 자꾸 미끄러지긴 했지만, 결국 무사히 던져줄 수 있었다.");
            talkData.Add("건네받은 학생은 고맙다고 소리친 다음 축구를 계속했다.");
            talkData.Add("나는 이제 마저 산책을 해야지.");
        }
        else
        {
            talkData.Add("축구공을 자꾸 놓치는 바람에 제대로 건네줄 수 없었다.");
            talkData.Add("결국 학생이 내 앞으로 뛰어오더니 직접 공을 가지고 돌아갔다.");
            talkData.Add("어쩔 수 없었어. . .");
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

                            //PlayerPrefs.SetInt("PlayerWhere", 3);
                            UserData.instance.SetUserDataInfo("playerPos", 5);

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
