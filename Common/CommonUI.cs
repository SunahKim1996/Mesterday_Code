using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum commonUIType
{
    Network,
    BackButton,
}

public class CommonUI : MonoBehaviour
{
    public GameObject commonCanvas;
    public GameObject backButtonUI;
    public GameObject networkUI;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ToggleUI(commonUIType uiType, bool state)
    {
        commonCanvas.SetActive(state);

        if (!state)
            return;

        bool showNetwork = (uiType == commonUIType.Network);
        bool showBackButton = (uiType == commonUIType.BackButton);

        networkUI.SetActive(showNetwork);
        backButtonUI.SetActive(showBackButton);
    }
}
