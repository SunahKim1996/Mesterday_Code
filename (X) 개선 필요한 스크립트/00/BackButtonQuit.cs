using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonQuit : MonoBehaviour
{
    public GameObject reConfirmDialog;

    public AudioSource soundEffect;
    public AudioClip normalButtonEffect;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            reConfirmDialog.SetActive(true);
        }
    }

    public void Yes()
    {
        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        Application.Quit();
    }

    public void No()
    {
        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        reConfirmDialog.SetActive(false);
    }
}
