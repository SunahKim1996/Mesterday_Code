using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float _sec; //제한시간
    int _min; 

    [SerializeField] Text timerText;
    string timeText;


    void Start()
    {
        _sec = 0;
        _min = 1;

        timerText.text = "00 : 00";
    }

    public void StartTimer()
    {
        StartCoroutine(CheckTimer());
    }

    IEnumerator CheckTimer()
    {
        while ((int)_sec >= 0) 
        {
            _sec -= Time.deltaTime;

            timeText = string.Format("{0:D2} : {1:D2}", _min, (int)_sec);
            timerText.text = timeText;

            if (timeText == "00 : 10")
                SoundManager.instance.PlaySFX(SoundClip.timerSFX);

            yield return null;
        }

        // 분 빼기 
        if (_min != 0)
        {
            _sec = 60;
            _min--;

            StartCoroutine(CheckTimer());

            yield break;
        }

        // Timer 종료 
        else
        {
            //TODO: 게임 종료 알림 
        }
    }
}
