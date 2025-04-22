using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum DialogType
{
    Tutorial,
}

[System.Serializable]
public class DialogInfo
{
    public string dialogText;
    public Action startCallback = null;
    public Action endCallback = null;
    public bool isCanNextChat = false; // endCallback 이후 다음 대사로 넘어갈 수 있을 때 true 로 변경
}

public abstract class DialogManager : MonoBehaviour
{
    public class DialogueData
    {
        public List<string> dialogTextList;
    }

    protected DialogType dialogType;
    [SerializeField] protected TextAsset dialogJsonData;

    protected DialogueData dialogueData;
    protected List<DialogInfo> dialogueDataList = new List<DialogInfo>();
    [SerializeField] protected Text dialogUI;
    protected Action dialogEndCallback; // 모든 대사가 끝났을 때 처리 

    int talkNum = 0;
    bool isTalking = false;
    float chatSpeed = 0.1f;
    bool isStart = false;

    Coroutine chatCor = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isStart)
            NextChat();
    }

    protected void StartChat()
    {
        Chat();
        isStart = true;
    }

    void NextChat()
    {
        // 대사 출력 연출 중이라면, 연출 스킵 
        if (isTalking)
        {
            if (chatCor != null)
            {
                StopCoroutine(chatCor);
                chatCor = null;
            }                

            isTalking = false;
            dialogUI.text = dialogueDataList[talkNum].dialogText;
        }

        // 대사 연출이 끝난 상태라면, 다음 대사 출력 시작
        else
        {
            if (!dialogueDataList[talkNum].isCanNextChat && dialogueDataList[talkNum].endCallback != null)
                dialogueDataList[talkNum].endCallback();

            else if (talkNum < (dialogueDataList.Count - 1))
            {
                talkNum += 1;
                Chat();
            }

            // 모든 대사 출력 완료하면, EndCallback
            else
                dialogEndCallback();

        }
    }

    void Chat()
    {
        chatCor = StartCoroutine(
                    ChatConversation(dialogueDataList[talkNum].dialogText, chatSpeed));

        if (dialogueDataList[talkNum].startCallback != null)
            dialogueDataList[talkNum].startCallback();
    }

    IEnumerator ChatConversation(string narration, float speed)
    {
        isTalking = true;
        dialogUI.text = "";
        string writerText = "";

        for (int i = 0; i < narration.Length; i++)
        {
            writerText += narration[i];
            dialogUI.text = writerText;
            yield return new WaitForSeconds(speed);
        }

        isTalking = false;
    }
    public void RefreshIsCanNextChat()
    {
        dialogueDataList[talkNum].isCanNextChat = true;
        NextChat();
    }
}
