using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileBackButton : MonoBehaviour
{
    CommonUI commonUI;
    commonUIType uiType;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        commonUI = GetComponent<CommonUI>();
        uiType = commonUIType.BackButton;
    }

    void Update()
    {
        if (commonUI == null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            commonUI.ToggleUI(uiType, true);
    }

    public void OnConfirmButton()
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);

        Application.Quit();
    }

    public void OnCancelButton()
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);

        commonUI.ToggleUI(uiType, false);
    }
}
