using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphMoving02_2 : MonoBehaviour
{
    public Text fomulaTxt_Y;    

    public GameObject graph;

    public GameObject dragPoint;

    int StartNum_Y = 1; // y절편 = 1
    
    public AudioSource soundEffect;
    public AudioClip plusEffect;
    public AudioClip a_submitEffect;
    public AudioClip na_submitEffect;

    //함수 게임 + 버튼
    public void UpButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;
        //Debug.Log(StartNum);

        if (StartNum_Y < 3)
        {
            StartNum_Y++;

            if (StartNum_Y == -1)
            {
                fomulaTxt_Y.text = StartNum_Y.ToString();
            }
            else
            {
                fomulaTxt_Y.text = "+" + StartNum_Y.ToString();
            }            
            

            if(StartNum_Y == 3)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 18.47f);
            }
            else if (StartNum_Y == 2)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 13.07f);
            }
            else if (StartNum_Y == 1)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 7.67f);
            }
            else if (StartNum_Y == 0)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 2.2f);
            }
        }
    }

    //함수 게임 -버튼
    public void DownButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;
        //Debug.Log(StartNum);

        if (StartNum_Y > -1)
        {
            StartNum_Y--;

            if (StartNum_Y == -1)
            {
                fomulaTxt_Y.text = StartNum_Y.ToString();
            }
            else
            {
                fomulaTxt_Y.text = "+" + StartNum_Y.ToString();
            }
            
            
            if (StartNum_Y == 2)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 13.07f);
            }
            else if (StartNum_Y == 1)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 7.67f);
            }
            else if (StartNum_Y == 0)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, 2.2f);
            }
            else if (StartNum_Y == -1)
            {
                graph.transform.position = new Vector3(graph.transform.position.x, graph.transform.position.y, -3.2f);
            }
        }
    }

    public void SubmitButton()
    {
        if (StartNum_Y == 0 && dragPoint.GetComponent<GraphDrag02_2>().fomulaTxt_x.text == "1")
        {
            soundEffect.PlayOneShot(a_submitEffect);
            soundEffect.volume = SoundManager.a_submitEffect;

            GetComponent<CameraMoving02>().isPuzzleGameStart = false;
            GetComponent<StarScore02>().isStarScoreOnTime = true;
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

        if (dragPoint.GetComponent<GraphDrag02_2>().fomulaTxt_x.text != "1")
        {
            if(randomInt == 0)
            {
                bubbleTxt.text = "다시 해보자";
            }
            else
            {
                bubbleTxt.text = "기울기가 안맞아";
            }
        }
        else
        {
            bubbleTxt.text = "다시 해보자";
        }

        yield return new WaitForSeconds(1.5f);

        speechBubble.SetActive(false);
    }
}
