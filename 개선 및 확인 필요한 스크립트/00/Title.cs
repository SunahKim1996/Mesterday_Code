using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public List<Text> txt = new List<Text>();
    public bool isTouchOn = false;
    public bool startMenuOn = false;
    public bool isLoginReady = false;

    public Button logInBtn_1;
    public Button logInBtn_2;

    void Start()
    {
        Input.multiTouchEnabled = false;
        StartCoroutine(FadeIn());
        StartCoroutine(StartMenu());
        StartCoroutine(Wait());
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(TouchSense());
        StopCoroutine(Wait());
    }

    IEnumerator TouchSense()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                isTouchOn = true;
            }
        }
    }

    IEnumerator FadeIn()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);            

            if(isTouchOn == false)
            {
                for (int j = 0; j < 9; j += 1)
                {
                    float i = 0f;

                    for (i = 0f; i >= 0 & i <= 1; i += 0.1f)
                    {
                        Color color = new Vector4(txt[j].color.r, txt[j].color.g, txt[j].color.b, i);

                        txt[j].color = color;

                        yield return new WaitForSeconds(0.05f);

                        if (isTouchOn == true)
                        {
                            for (int t = 0; t < 9; t += 1)
                            {
                                Color color2 = new Vector4(txt[t].color.r, txt[t].color.g, txt[t].color.b, 1f);
                                txt[t].color = color2;
                            }

                            startMenuOn = true;

                            break;
                        }
                    }

                    if(isTouchOn == true)
                    {
                        break;
                    }
                }

                startMenuOn = true;
                break;
            }

            else if(isTouchOn == true)
            {

                for (int t = 0; t < 9; t += 1)
                {
                    Color color = new Vector4(txt[t].color.r, txt[t].color.g, txt[t].color.b, 1f);
                    txt[t].color = color;
                }
            }
        }
    }

    
    IEnumerator StartMenu()
    {
        while(true)
        {
            yield return null;

            if (startMenuOn == true)
            {
                if (GetComponent<AnonymousLogin>().isAlreadyLogin == true)
                {
                    isLoginReady = true;
                    break;
                }
                else
                {
                    for (float i = 0f; i >= 0 && i <= 1.1; i += 0.1f)
                    {
                        Color BtnColor_1 = new Vector4(logInBtn_1.GetComponent<Image>().color.r, logInBtn_1.GetComponent<Image>().color.g, logInBtn_1.GetComponent<Image>().color.b, i);

                        logInBtn_1.GetComponent<Image>().color = BtnColor_1;

                        Color BtnColor_2 = new Vector4(logInBtn_2.GetComponent<Image>().color.r, logInBtn_2.GetComponent<Image>().color.g, logInBtn_2.GetComponent<Image>().color.b, i);

                        logInBtn_2.GetComponent<Image>().color = BtnColor_2;

                        yield return new WaitForSeconds(0.05f);

                        if (i >= 1)
                        {
                            startMenuOn = false;
                            isLoginReady = true;
                            break;
                        }
                    }
                }   
            }
        }
    }
}
