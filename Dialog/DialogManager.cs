using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogInfo
{
    public string dialogText;
    public Action startCallback = null;
    public Action endCallback = null;
}

public abstract class DialogManager : MonoBehaviour
{
    public class DialogueData
    {
        public List<string> dialogTextList;
    }

    [SerializeField] protected TextAsset dialogJsonData;

    protected DialogueData dialogueData;
    protected List<DialogInfo> dialogueDataList = new List<DialogInfo>();
    [SerializeField] protected Text dialogUI;
    protected Action dialogEndCallback; // ��� ��簡 ������ �� ó�� 

    int talkNum = 0;    
    float chatSpeed = 0.1f;
    [HideInInspector] public bool isTalking = false;
    [HideInInspector] public bool isChatPause = true;

    Coroutine chatCor = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isChatPause)
            NextChat();
    }

    protected void LoadJsonData(Action endCallback = null)
    {
        // Json Data �ε� 
        dialogueData = JsonUtility.FromJson<DialogueData>(dialogJsonData.text);

        for (int i = 0; i < dialogueData.dialogTextList.Count; i++)
        {
            DialogInfo info = new DialogInfo();
            info.dialogText = dialogueData.dialogTextList[i];
            info.startCallback = GetStartCallback(i);
            info.endCallback = GetEndCallback(i);

            dialogueDataList.Add(info);
        }

        // ��� ��� ���� 
        if (endCallback != null)
            endCallback();
    }

    protected abstract Action GetStartCallback(int index);
    protected abstract Action GetEndCallback(int index);

    /// <summary>
    /// �ߴ� �� �ٽ� ������ ���� �ش� �޼ҵ� ��� 
    /// </summary>
    public void StartChat()
    {        
        isChatPause = false;
        Chat();
    }

    void NextChat()
    {
        // ��� ��� ���� ���̶��, ���� ��ŵ 
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

        // ��� ������ ���� ���¶��, ���� ��� ��� ����
        else
        {
            if (dialogueDataList[talkNum].endCallback != null)
            {
                dialogueDataList[talkNum].endCallback();
                talkNum++;
            }               

            else if (talkNum < (dialogueDataList.Count - 1))
            {
                talkNum++;
                Chat();
            }

            // ��� ��� ��� �Ϸ��ϸ�, EndCallback
            else
            {
                PauseChat();

                if (dialogEndCallback != null)
                    dialogEndCallback();
            }
        }
    }

    void Chat()
    {
        chatCor = StartCoroutine(
                    ChatConversation(dialogueDataList[talkNum].dialogText, chatSpeed));

        if (dialogueDataList[talkNum].startCallback != null)
            dialogueDataList[talkNum].startCallback();
    }

    protected void PauseChat()
    {
        isChatPause = true;
        isTalking = false;

        if (chatCor != null)
        {
            StopCoroutine (chatCor);
            chatCor = null;
        }
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
        chatCor = null;
    }
}
