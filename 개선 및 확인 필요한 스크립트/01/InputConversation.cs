using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputConversation : MonoBehaviour
{
    public Text Chat;
    string writerText = "";

    public GameObject nickNameField;
    public GameObject schoolNameField;

    public InputField nickNameField2;
    public InputField schoolNameField2;

    public Button nickNameConfirm;
    public Button schoolNameConfirm;

    public Text errorTxt;

    public string nickName;
    public string schoolName;

    public AudioSource soundEffect;
    public AudioClip keyboardEffect;

    List<string> talkData = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Chat.text = "";
        StartCoroutine(GoNextConversation());
        AddTalkData_First();

        StartCoroutine(ChatConversation(talkData[0], 0.1f));
    }

    // Update is called once per frame
    void Update()
    {      
        if (nickNameField2.text == "")
        {
            nickNameConfirm.interactable = false;
        }
        else
        {
            nickNameConfirm.interactable = true;
        }


        if (schoolNameField2.text == "")
        {
            schoolNameConfirm.interactable = false;
        }
        else
        {
            schoolNameConfirm.interactable = true;
        }
    }

    void AddTalkData_First()
    {
        talkData.Add("[ 자기소개서 ]");
        talkData.Add("[ 당신의 이름은 무엇입니까 ]");

    }

    IEnumerator ChatConversation(string narration, float speed)
    {
        isTalking = true;

        int a = 0;
        writerText = "";

        soundEffect.PlayOneShot(keyboardEffect);
        soundEffect.volume = SoundManager.keyboardEffect;
        

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
                        if(order == 0)
                        {
                            nickNameField.SetActive(true);
                            order++;

                            break;
                        }
                        else
                        {
                            schoolNameField.SetActive(true);

                            break;
                        }   
                    }
                    
                }             
            }
        }      
        
    }

    int order = 0;
    public bool isInPutOver = false;

    public void ConfirmButton_1()
    {
        if(CheckNickname() == true)
        {
            nickName = nickNameField2.text;
            //PlayerPrefs.SetString("nickName", nickName);

            nickNameField.SetActive(false);

            Chat.text = "";
            StartCoroutine(GoNextConversation());
            AddTalkData_Second();

            StartCoroutine(ChatConversation(talkData[0], 0.1f));
        }
        else
        {
            StartCoroutine(ErrorMessage());
        }
    }

    void AddTalkData_Second()
    {
        talkNum = 0;
        talkData.Remove("[ 자기소개서 ]");
        talkData.Remove("[ 당신의 이름은 무엇입니까 ]");

        talkData.Add("[ 당신의 출신 고등학교의 이름은 무엇입니까 ]");
    }

    public void ConfirmButton_2()
    {
        if (CheckSchoolname() == true)
        {
            schoolNameField.SetActive(false);

            schoolName = schoolNameField2.text;
            //PlayerPrefs.SetString("schoolName", schoolName);

            UserData.instance.SetUserDataInfo("nickName", nickName);
            UserData.instance.SetUserDataInfo("schoolName", schoolName);
            //DataBaseSave.instance.writeNewUser(AnonymousLogin.user.UserId, nickName, schoolName);
            //userData.GetSavedData();

            /*
            userData.nickName = nickName;
            userData.schoolName = schoolName;
            userData.clearTutorial = false;
            */

            schoolNameField.SetActive(false);

            isInPutOver = true;
        }
        else
        {
            StartCoroutine(ErrorMessage());
        }
    }

    IEnumerator ErrorMessage()
    {
        errorTxt.text = "공백이 없어야 합니다.";

        errorTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        errorTxt.gameObject.SetActive(false);
    }

    private bool CheckNickname()
    {
        return Regex.IsMatch(nickNameField2.text, "^[^\\s]+$");
    }

    private bool CheckSchoolname()
    {
        return Regex.IsMatch(schoolNameField2.text, "^[^\\s]+$");
    }
}

