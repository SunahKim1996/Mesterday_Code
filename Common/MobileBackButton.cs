using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileBackButton : MonoBehaviour
{
    //public AudioSource soundEffect;
    //public AudioClip normalButtonEffect;

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
        if (Input.GetKeyDown(KeyCode.Escape))
            commonUI.ToggleUI(uiType, true);
    }

    public void OnConfirmButton()
    {
        //Sound
        //soundEffect.PlayOneShot(normalButtonEffect);
        //soundEffect.volume = SoundManager.normalButtonEffect;

        Application.Quit();
    }

    public void OnCancelButton()
    {
        //Sound
        //soundEffect.PlayOneShot(normalButtonEffect);
        //soundEffect.volume = SoundManager.normalButtonEffect;

        commonUI.ToggleUI(uiType, false);
    }
}
