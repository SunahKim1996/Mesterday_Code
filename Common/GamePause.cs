using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public static bool isPauseTime = false;

    [SerializeField] Canvas pauseCanvas;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject pausePopup;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // 씬 변경 이벤트 등록
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnDisable()
    {
        // 이벤트 해제 (메모리 누수 방지)
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    // 씬이 바뀔 때 호출되는 함수 (접근제한자 상관없음)
    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        bool isCanShow = newScene.buildIndex != 0 && newScene.buildIndex != 10 && newScene.buildIndex != 11;
        pauseCanvas.gameObject.SetActive(isCanShow);
    }

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
        pauseUI.SetActive(false);
        pausePopup.SetActive(false);
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
