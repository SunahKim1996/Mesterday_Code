using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphMoving06 : MonoBehaviour
{
    public Text FunctionText;
    
    public List<GameObject> graph = new List<GameObject>();         
    public int StartNum = 2;

    public AudioSource soundEffect;
    public AudioClip plusEffect;
    public AudioClip a_submitEffect;
    public AudioClip na_submitEffect;

    //함수 게임 +버튼
    public void UpButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (StartNum < 3)
        {
            FunctionText.text = " " + (int.Parse(FunctionText.text) + 1).ToString();

            StartNum += 1;

            if (StartNum == 3)
            {
                graph[0].SetActive(true);
                graph[1].SetActive(false);
            }

            else if (StartNum == 2)
            {
                graph[1].SetActive(true);
                graph[2].SetActive(false);
            }
        }        
    }

    //함수 게임 -버튼
    public void DownButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (StartNum > 1)
        {
            FunctionText.text = " " + (int.Parse(FunctionText.text) - 1).ToString();

            StartNum -= 1;

            if (StartNum == 2)
            {
                graph[1].SetActive(true);
                graph[0].SetActive(false);
            }

            else if (StartNum == 1)
            {
                graph[2].SetActive(true);
                graph[1].SetActive(false);
            }
        }        
    }

    public bool isPuzzleClear = false;

    public void SubmitButton()
    {
        if(isPuzzleClear == true)
        {
            soundEffect.PlayOneShot(a_submitEffect);
            soundEffect.volume = SoundManager.a_submitEffect;

            GetComponent<CameraMoving06>().isPuzzleGameStart = false;
            GetComponent<StarScore06>().isStarScoreOnTime = true;
        }

        else
        {
            soundEffect.PlayOneShot(na_submitEffect);
            soundEffect.volume = SoundManager.na_submitEffect;

            StartCoroutine(SpeechBubbleOn());
        }
    }

    public GameObject speechBubble;
    public Text bubbleTxt;

    IEnumerator SpeechBubbleOn()
    {
        speechBubble.SetActive(true);

        int randomInt = Random.Range(0, 2);

        if (randomInt == 0)
        {
            bubbleTxt.text = "다시 해보자";
        }
        else
        {
            bubbleTxt.text = "파장이 같아야 이상한 소리가 나지 않을 거야";
        }

        yield return new WaitForSeconds(1.5f);

        speechBubble.SetActive(false);
    }
}
