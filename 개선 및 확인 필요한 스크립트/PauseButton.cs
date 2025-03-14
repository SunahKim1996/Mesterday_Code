using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseScreen_1;
    public GameObject pauseScreen_2;

    public bool isPauseScreenOn = false;

    public AudioSource soundEffect;
    public AudioClip normalButtonEffect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPauseScreenOn = true;
            pauseScreen_1.SetActive(true);
        }
    }

    public void PauseButtonOn1()
    {
        Time.timeScale = 0;

        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        isPauseScreenOn = true;
        pauseScreen_1.SetActive(true);
    }

    public void Btn_KeepGoin()
    {
        Time.timeScale = 1;

        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        isPauseScreenOn = false;
        pauseScreen_1.SetActive(false);
    }

    public void Btn_goMain()
    {
        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        Time.timeScale = 1;

        GetComponent<GoToMainScene>().isReadyToGoToMain = true;  //메인화면으로 이동
        //SceneManager.LoadScene("02. Tutorial 2");
    }

    public void Btn_logOut()
    {
        soundEffect.PlayOneShot(normalButtonEffect);
        soundEffect.volume = SoundManager.normalButtonEffect;

        pauseScreen_2.SetActive(true);
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

        pauseScreen_2.SetActive(false);
    }
}
