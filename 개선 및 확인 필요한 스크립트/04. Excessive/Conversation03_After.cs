using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation03_After : MonoBehaviour
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

        starScore_Pre = userData.stage2_Score;

        //PlayerPrefs.SetInt("PlayerWhere", 2);
        UserData.instance.SetUserDataInfo("playerPos", 2);

        /*
        if (PlayerPrefs.HasKey("03_Clear") == false)
        {
            PlayerPrefs.SetInt("stage2_Score", 0);
        }
        */

        Chat.text = "";

        StartCoroutine(Wait());
    }

    public int playerHaveItem;

    IEnumerator Wait() //퍼즐 게임 끝날 때까지 기다림
    {
        while (true)
        {
            yield return null;

            if (GetComponent<CameraMoving03>().isMainCamOpen == true && GetComponent<CameraMoving03>().distance2 < 5f)
            {
                AddTalkData();

                Chat.gameObject.SetActive(true);
                chatBox.SetActive(true);

                StartCoroutine(GoNextConversation());
                StartCoroutine(ChatConversation(talkData[0], 0.1f));
                
                //PlayerPrefs.SetInt("03_Clear", 1);
                //DataBaseSave.instance.writeClearData_2(AnonymousLogin.user.UserId, true);
                UserData.instance.SetUserDataInfo("clear02", true);

                /*
                if (PlayerPrefs.GetInt("stage2_Score") <= GetComponent<StarScore03>().howMuchStarScore)
                {
                    PlayerPrefs.SetInt("stage2_Score", GetComponent<StarScore03>().howMuchStarScore);
                    DataBaseSave.instance.writeNewScore_2(AnonymousLogin.user.UserId, GetComponent<StarScore03>().howMuchStarScore);
                }
                */

                if (starScore_Pre < GetComponent<StarScore03>().howMuchStarScore)
                {
                    //DataBaseSave.instance.writeNewScore_2(AnonymousLogin.user.UserId, GetComponent<StarScore03>().howMuchStarScore);
                    UserData.instance.SetUserDataInfo("stage2_Score", GetComponent<StarScore03>().howMuchStarScore);

                    if (GetComponent<StarScore03>().howMuchStarScore == 3)
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
        if (playerHaveItem == 0)
        {
            talkData.Add("공이 너무 움직여서 잡기가 힘들다. . .");
            talkData.Add("포기하고 체육 부장에게 말하는 게 나을 것 같아.");
        }
        else if (playerHaveItem == 5)
        {
            talkData.Add("공을 모두 치웠다.");
            talkData.Add("다들 체육 시간에 혼나지 않아도 되겠어");
        }
        else if (playerHaveItem > 0)
        {            
            talkData.Add("공을 " + playerHaveItem + "개 치웠다.");
            talkData.Add("전부 치우진 못했지만 이정도면 되겠지");
            talkData.Add("나머지는 체육 부장에게 맡겨야 겠다.");
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

                            //PlayerPrefs.SetInt("PlayerWhere", 2);
                            UserData.instance.SetUserDataInfo("playerPos", 2);
                            
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
