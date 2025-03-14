using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation06_After : MonoBehaviour
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

        starScore_Pre = userData.stage4_Score;

        //PlayerPrefs.SetInt("PlayerWhere", 4);
        UserData.instance.SetUserDataInfo("playerPos", 4);

        /*
        if (PlayerPrefs.HasKey("05_Clear") == false)
        {
            PlayerPrefs.SetInt("stage4_Score", 0);
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

            if (GetComponent<CameraMoving06>().isMainCamOpen == true && GetComponent<CameraMoving06>().distance2 < 1f)
            {
                AddTalkData();

                Chat.gameObject.SetActive(true);
                chatBox.SetActive(true);

                StartCoroutine(GoNextConversation());
                StartCoroutine(ChatConversation(talkData[0], 0.1f));

                //PlayerPrefs.SetInt("05_Clear", 1);
                //DataBaseSave.instance.writeClearData_4(AnonymousLogin.user.UserId, true);
                UserData.instance.SetUserDataInfo("clear04", true);

                /*
                if (PlayerPrefs.GetInt("stage4_Score") <= GetComponent<StarScore06>().howMuchStarScore)
                {
                    PlayerPrefs.SetInt("stage4_Score", GetComponent<StarScore06>().howMuchStarScore);
                    DataBaseSave.instance.writeNewScore_5(AnonymousLogin.user.UserId, GetComponent<StarScore06>().howMuchStarScore);
                }
                */

                if (starScore_Pre < GetComponent<StarScore06>().howMuchStarScore)
                {
                    //DataBaseSave.instance.writeNewScore_4(AnonymousLogin.user.UserId, GetComponent<StarScore06>().howMuchStarScore);
                    UserData.instance.SetUserDataInfo("stage4_Score", GetComponent<StarScore06>().howMuchStarScore);

                    if (GetComponent<StarScore06>().howMuchStarScore == 3)
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
        if(GetComponent<StarScore06>().howMuchStarScore > 0)
        {
            talkData.Add("다행이다, 시간 안에 고칠 수 있었어.");
            talkData.Add("스피커에서 노래가 정상적으로 나오고 있다.");
            talkData.Add("꿈 속에선 같이 혼나지 않아서 다행이야");
        }
        else
        {
            talkData.Add("벌써 시간이 이렇게. . .");
            talkData.Add("기계를 고치지 못한 채 점심시간이 끝나버렸다.");
            talkData.Add("선생님들이 고쳐주시길 기다리는 수밖에 없을 것 같다.");
            talkData.Add("아쉽지만, 이만 교실로 돌아가야겠어.");
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

                            //PlayerPrefs.SetInt("PlayerWhere", 4);
                            UserData.instance.SetUserDataInfo("playerPos", 4);

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
