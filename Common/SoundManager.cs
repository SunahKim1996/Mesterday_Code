using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo: 앞글자 대문자로 변경
public enum SoundClip
{
    LoginBGM,
    MainBGM,
    GameBGM,
    FinalStageBGM,
    EndingBGM,

    dialogSFX,
    GameButtonSFX,
    AnswerSFX,
    ErrorSFX,
    cameraSFX,
    clearSFX,
    doorSFX,
    doorLockedSFX,
    keyboardSFX,
    sighSFX,
    dragSFX,
    swipeSFX,
    ButtonSFX,
    getSFX,
    getSFX_2,
    peopleSFX,
    callingSFX,
    getCallSFX,
    timerSFX,
    jingleSFX,
    PaperSFX,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<AudioClip> clips;

    public AudioSource bgmSound;
    public AudioSource sfxSound;

    public Transform sfxParent;

    public Queue<AudioSource> pool;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        pool = new Queue<AudioSource>();
    }

    // SFX ==================================================================================
    public void PlaySFX(SoundClip clip, float volume = 1f)
    {
        AudioSource audioSource;

        if (pool.Count == 0)
        {
            audioSource = Instantiate(sfxSound, sfxParent);
        }
        else
        {
            audioSource = pool.Dequeue();
            audioSource.transform.parent = sfxParent;
        }

        audioSource.clip = clips[(int)clip];
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(SoundStop(audioSource));
    }

    IEnumerator SoundStop(AudioSource audioSource)
    {
        while (audioSource.isPlaying)
            yield return null;

        pool.Enqueue(audioSource);
    }

    // BGM ==================================================================================

    public void PlayBGM(SoundClip clip, bool isLoop = true)
    {
        if (bgmSound.clip == clips[(int)clip] && bgmSound.isPlaying)
            return;

        bgmSound.clip = clips[(int)clip];
        bgmSound.loop = isLoop;

        bgmSound.Play();
    }

    public void StopBGM()
    {
        bgmSound.Stop();
    }

    public void BGMSound(bool isOn, float volume)
    {
        if (isOn)
            bgmSound.volume = volume;
        else
            bgmSound.volume = 0;
    }
}
