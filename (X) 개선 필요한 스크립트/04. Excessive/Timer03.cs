using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer03 : MonoBehaviour
{
    public float _sec;
    int _min; //제한시간 : 90초
    
    [SerializeField]
    Text _TimerText = null;

    string HowMuchTime;

    public AudioSource soundEffect;
    public AudioClip timerEffect;

    private void Start()
    {
        StartCoroutine(TenSeconds());

        _sec = 0;
        _min = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PauseButton>().isPauseScreenOn == false)
        {
            if (GetComponent<CameraMoving03>().isPuzzleGameStart == true)
            {
                //Debug.Log("timer start");
                Timer();
            }
        }
    }

    IEnumerator TenSeconds()
    {
        while (true)
        {
            yield return null;

            if (HowMuchTime == "00 : 10")
            {
                Debug.Log("10초 남음");

                soundEffect.PlayOneShot(timerEffect);
                soundEffect.volume = SoundManager.timerEffect;

                break;
            }
        }
    }

    public void Timer()
    {
        _sec -= Time.deltaTime;

        HowMuchTime = string.Format("{0:D2} : {1:D2}", _min, (int)_sec);
        _TimerText.text = HowMuchTime;

        if ((int)_sec < 0)
        {
            if (_min != 0)
            {
                _sec = 60;
                _min--;
            }
            else
            {
                GetComponent<CameraMoving03>().isPuzzleGameStart = false;
                GetComponent<StarScore03>().isStarScoreOnTime = true;
                Debug.Log("Time Over");

                _TimerText.text = "00 : 00";
            }
        }

    }
}
