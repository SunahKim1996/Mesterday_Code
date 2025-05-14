using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Dialog03Linear : DialogManager
{
    [SerializeField] PuzzleManager puzzleManager;

    void Start()
    {
        dialogEndCallback = EndDialog;
        LoadJsonData(StartChat);
    }

    void EndDialog()
    {
        
    }

    protected override Action GetStartCallback(int index)
    {
        Action callback = null;
        return callback;
    }

    protected override Action GetEndCallback(int index)
    {
        Action callback = null;

        switch (index)
        {
            case 8:
                callback = () =>
                {
                    PauseChat();
                    puzzleManager.StartGame();
                };
                break;
            default:
                break;
        }

        return callback;
    }
}
