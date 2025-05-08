using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static bool isPauseTime = false;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject pausePopup;

    public void OnTogglePauseUI(bool isPause)
    {
        isPauseTime = isPause;

        Time.timeScale = isPause ? 0 : 1;
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);
        pauseUI.SetActive(isPause);
    }

    public void OnGoBackMain()
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);
        Time.timeScale = 1;

        SoundManager.instance.StopBGM();
        LoadingManager.LoadScene(2);  //메인화면으로 이동
    }

    public void OnToggleQuitUI(bool state)
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);
        pausePopup.SetActive(state);
    }

    public void OnQuit()
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);
        Application.Quit();
    }
}
