using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogMainTutorial : DialogManager
{
    private UserDataInfo userData;

    [SerializeField] GameObject player;
    [SerializeField] GameObject dialogFrame;

    [SerializeField] SpeechBubbleManager speechBubbleManager;

    void Start()
    {        
        userData = UserData.instance.userData;
        if (userData.clearTutorial)
            return;

        TouchPointMoving.isTalking = false; //TODO

        player.GetComponent<Animator>().SetInteger("anim", 0);

        dialogEndCallback = EndDialog;
        LoadJsonData();
    }    

    public void StartDialog()
    {
        dialogFrame.SetActive(true);
        StartChat();
    }

    void EndDialog()
    {
        //TODO?
    }

    protected override Action GetStartCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 0:
                callback = () =>
                {
                    player.GetComponent<Animator>().SetInteger("anim", 2);
                };
                break;
            default:
                break;
        }

        return callback;
    }

    protected override Action GetEndCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 5:
                callback = () =>
                {
                    isChatPause = true;

                    dialogFrame.SetActive(false);
                    player.GetComponent<Animator>().SetInteger("anim", 0);
                    TouchPointMoving.isTalking = false; //TODO

                    string[] dialogList =
                    {
                        "일단 좀 돌아다녀 보자",
                        "바닥을 터치하면 " + System.Environment.NewLine + "움직일 수 있는 것 같아"
                    };

                    speechBubbleManager.StartSpeechBubbleGuide(dialogList);
                };
                break;
            default:
                break;
        }

        return callback;
    }
}
