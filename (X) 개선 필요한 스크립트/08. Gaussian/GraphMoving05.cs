using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphMoving05 : MonoBehaviour
{
    public Text fomulaTxt;
    public Text fomulaArea;

    int StartNum = 3;

    public List<GameObject> num = new List<GameObject>();

    public AudioSource soundEffect;
    public AudioClip plusEffect;
    public AudioClip a_submitEffect;
    public AudioClip na_submitEffect;

    //함수 게임 + 버튼
    public void UpButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if(int.Parse(fomulaTxt.text) < 15)
        {
            fomulaTxt.text = (int.Parse(fomulaTxt.text) + 1).ToString();

            if (int.Parse(fomulaTxt.text) >= 10)
            {
                fomulaArea.text = "y = [      x ]";
            }
            else if (int.Parse(fomulaTxt.text) <= -10)
            {
                fomulaArea.text = "y = [         x ]";
            }
            else
            {
                fomulaArea.text = "y = [     x ]";
            }

            StartNum += 1;
            if (StartNum <= 3 && StartNum >= -2)
            {


                if (StartNum == 3)
                {
                    num[0].SetActive(true);
                    num[1].SetActive(false);
                }

                else if (StartNum == 2)
                {
                    num[1].SetActive(true);
                    num[2].SetActive(false);
                }

                else if (StartNum == 1)
                {
                    num[2].SetActive(true);
                    num[3].SetActive(false);
                }

                else if (StartNum == 0)
                {
                    num[3].SetActive(true);
                    num[4].SetActive(false);
                }

                else if (StartNum == (-1))
                {
                    num[4].SetActive(true);
                    num[5].SetActive(false);
                }

                else if (StartNum == (-2))
                {
                    num[5].SetActive(true);
                    num[6].SetActive(false);
                }
            }
        }
        
    }

    //함수 게임 -버튼
    public void DownButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (int.Parse(fomulaTxt.text) > -15)
        {
            fomulaTxt.text = (int.Parse(fomulaTxt.text) - 1).ToString();

            if (int.Parse(fomulaTxt.text) >= 10)
            {
                fomulaArea.text = "y = [      x ]";
            }
            else if (int.Parse(fomulaTxt.text) <= -10)
            {
                fomulaArea.text = "y = [         x ]";
            }
            else
            {
                fomulaArea.text = "y = [     x ]";
            }

            StartNum -= 1;
            if (StartNum >= -3 && StartNum <= 3)
            {


                if (StartNum == 2)
                {
                    num[1].SetActive(true);
                    num[0].SetActive(false);
                }

                else if (StartNum == 1)
                {
                    num[2].SetActive(true);
                    num[1].SetActive(false);
                }

                if (StartNum == 0)
                {
                    num[3].SetActive(true);
                    num[2].SetActive(false);
                }

                else if (StartNum == (-1))
                {
                    num[4].SetActive(true);
                    num[3].SetActive(false);
                }

                else if (StartNum == (-2))
                {
                    num[5].SetActive(true);
                    num[4].SetActive(false);
                }

                else if (StartNum == (-3))
                {
                    num[6].SetActive(true);
                    num[5].SetActive(false);
                }

            }
        }
            
    }
    
    public void SubmitButton()
    {
        if(GetComponent<Timer05>()._sec > 0)
        {
            if (StartNum == 1)
            {
                soundEffect.PlayOneShot(a_submitEffect);
                soundEffect.volume = SoundManager.a_submitEffect;

                GetComponent<CameraMoving05>().isPuzzleGameStart = false;
                GetComponent<StarScore05>().isStarScoreOnTime = true;
            }

            else
            {
                soundEffect.PlayOneShot(na_submitEffect);
                soundEffect.volume = SoundManager.na_submitEffect;

                StartCoroutine(SpeechBubbleOn());
            }
        }
        
    }

    public GameObject speechBubble;
    public Text bubbleTxt;

    IEnumerator SpeechBubbleOn()
    {
        speechBubble.SetActive(true);

        int randomInt = Random.Range(0, 2);

        if(StartNum > 1)
        {
            bubbleTxt.text = "경사가 너무 높아서 친구에게 닿질 않아";
        }
        else if(StartNum < 1)
        {
            bubbleTxt.text = "경사가 너무 낮아";
        }

        yield return new WaitForSeconds(1.5f);

        speechBubble.SetActive(false);
    }
}
