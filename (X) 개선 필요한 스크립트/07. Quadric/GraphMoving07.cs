using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphMoving07 : MonoBehaviour
{
    public Text FunctionText;

    public GameObject FunctionText2;
    public GameObject FunctionText3;

    public List<GameObject> graph = new List<GameObject>();         
    public int StartNum = 3;

    public AudioSource soundEffect;
    public AudioClip plusEffect;
    public AudioClip a_submitEffect;
    public AudioClip na_submitEffect;

    public GameObject beforeBall;
    public GameObject afterBall;

    private void Start()
    {
        FunctionText.gameObject.SetActive(false);
        FunctionText2.SetActive(true);
        FunctionText3.SetActive(false);
    }

    //함수 게임 +버튼
    public void UpButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (StartNum < 6)
        {        
            StartNum += 1;

            if ((StartNum - 3) >= 0)
            {
                FunctionText.text = "+ " + (StartNum - 3).ToString();
            }
            else
            {
                FunctionText.text = "- " + Mathf.Abs(StartNum - 3).ToString();
            }

            if (StartNum == 6)
            {
                graph[1].SetActive(false);
                graph[0].SetActive(true);
            }
            else if (StartNum == 5)
            {
                graph[2].SetActive(false);
                graph[1].SetActive(true);
            }
            else if (StartNum == 4)
            {
                FunctionText.gameObject.SetActive(true);
                FunctionText2.SetActive(false);
                FunctionText3.SetActive(true);

                graph[3].SetActive(false);
                graph[2].SetActive(true);
            }
            else if (StartNum == 3)
            {
                FunctionText.gameObject.SetActive(false);
                FunctionText2.SetActive(true);
                FunctionText3.SetActive(false);

                graph[4].SetActive(false);
                graph[3].SetActive(true);
            }
            else if (StartNum == 2)
            {
                graph[5].SetActive(false);
                graph[4].SetActive(true);
            }
            else if (StartNum == 1)
            {
                graph[6].SetActive(false);
                graph[5].SetActive(true);
            }
        }
    }

    //함수 게임 -버튼
    public void DownButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (StartNum > 0)
        {
            StartNum -= 1;

            if ((StartNum - 3) >= 0)
            {
                FunctionText.text = "+ " + (StartNum - 3).ToString();
            }
            else
            {
                FunctionText.text = "- " + Mathf.Abs(StartNum - 3).ToString();
            }            

            if (StartNum == 0)
            {
                graph[5].SetActive(false);
                graph[6].SetActive(true);
            }
            else if (StartNum == 1)
            {
                graph[4].SetActive(false);
                graph[5].SetActive(true);
            }
            else if (StartNum == 2)
            {
                FunctionText.gameObject.SetActive(true);
                FunctionText2.SetActive(false);
                FunctionText3.SetActive(true);

                graph[3].SetActive(false);
                graph[4].SetActive(true);
            }
            else if (StartNum == 3)
            {
                FunctionText.gameObject.SetActive(false);
                FunctionText2.SetActive(true);
                FunctionText3.SetActive(false);

                graph[2].SetActive(false);
                graph[3].SetActive(true);
            }
            else if (StartNum == 4)
            {
                graph[1].SetActive(false);
                graph[2].SetActive(true);
            }
            else if (StartNum == 5)
            {
                graph[0].SetActive(false);
                graph[1].SetActive(true);
            }
        }        
    }

    public bool isSwipeAnswer = false;

    public void SubmitButton()
    {
        if((isSwipeAnswer == true && StartNum == 5) || (isSwipeAnswer == true && StartNum == 6))
        {
            beforeBall.SetActive(false);
            afterBall.SetActive(true);

            soundEffect.PlayOneShot(a_submitEffect);
            soundEffect.volume = SoundManager.a_submitEffect;

            GetComponent<CameraMoving07>().isPuzzleGameStart = false;
            GetComponent<StarScore07>().isStarScoreOnTime = true;
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
            bubbleTxt.text = "공을 놓쳤어. 다시 해보자";
        }
        else
        {
            bubbleTxt.text = "공이 어떻게 날아가야 할까?";
        }

        yield return new WaitForSeconds(1.5f);

        speechBubble.SetActive(false);
    }
}
