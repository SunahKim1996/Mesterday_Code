using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogMainTutorial : DialogManager
{
    private UserDataInfo userData;

    [SerializeField] GameObject player;
    Animator playerAnimator;
    PlayerMove playerMove;

    [SerializeField] GameObject dialogFrame;
    [SerializeField] SpeechBubbleManager speechBubbleManager;

    void Start()
    {       
        userData = UserData.instance.userData;
        if (userData.clearTutorial)
            return;

        playerAnimator = player.GetComponent<Animator>();
        playerMove = player.GetComponent<PlayerMove>();
        playerAnimator.SetInteger("anim", 0);

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
        UserData.instance.SetUserDataInfo("clearTutorial", true);
        dialogFrame.SetActive(false);

        string[] dialogList =
        {
            "일단 좀 더 돌아다녀 보는게 좋겠다",
            "앞에 문이 엄청 많네",
            "문패를 보니 교실인 것 같아",
            "가까이 가서 문을 열어보자",
        };
        speechBubbleManager.StartSpeechBubbleGuide(dialogList);
    }

    protected override Action GetStartCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 0:
                callback = () =>
                {
                    playerAnimator.SetInteger("anim", 2);
                };
                break;

            case 6:
                callback = () =>
                {
                    playerMove.ToggleTouchEffect(false);
                    dialogFrame.SetActive(true);
                    playerAnimator.SetInteger("anim", 0);
                };
                break;

            default:
                break;
        }

        callback += () => { SoundManager.instance.PlaySFX(SoundClip.dialogSFX, 0.4f); };

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
                    PauseChat();

                    dialogFrame.SetActive(false);
                    playerAnimator.SetInteger("anim", 0);                    

                    string[] dialogList =
                    {
                        "일단 좀 돌아다녀 보자",
                        "바닥을 터치하면\n움직일 수 있는 것 같아"
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
