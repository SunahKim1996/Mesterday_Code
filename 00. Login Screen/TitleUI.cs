using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] List<Text> titleCharList;
    [SerializeField] Button logInBtn;

    bool isSkip = false;

    float timer = 0;
    float waitTime = 1f;

    Coroutine fadeCor = null;
    Coroutine loginButtonCor = null;

    void Start()
    {
        Input.multiTouchEnabled = false;
        fadeCor = StartCoroutine(FadeIn());
    }
    
    void Update()
    {        
        if (timer < waitTime) 
        {
            timer += Time.deltaTime;
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isSkip)
        {
            isSkip = true;           

            if (fadeCor != null)
                StopCoroutine(fadeCor);

            ShowTitleText();
        }
    }

    IEnumerator FadeIn()
    {
        for (int j = 0; j < titleCharList.Count; j++)
        {
            // 타이틀 투명도 서서히 높힘
            for (float i = 0f; i >= 0 & i <= 1; i += 0.1f)
            {
                Color color = new Color(titleCharList[j].color.r, titleCharList[j].color.g, titleCharList[j].color.b, i);
                titleCharList[j].color = color;

                yield return new WaitForSeconds(0.05f);
            }
        }

        if (loginButtonCor == null)
            loginButtonCor = StartCoroutine(ShowLoginButton());
    }

    void ShowTitleText()
    {
        // 타이틀 전부 출력 
        for (int t = 0; t < titleCharList.Count; t++)
        {
            Color color = new Vector4(titleCharList[t].color.r, titleCharList[t].color.g, titleCharList[t].color.b, 1f);
            titleCharList[t].color = color;
        }

        if (loginButtonCor == null)
            loginButtonCor = StartCoroutine(ShowLoginButton());
    }


    IEnumerator ShowLoginButton()
    {
        if (AnonymousLogin.user != null)
            yield break;

        for (float i = 0f; i >= 0 && i <= 1.1; i += 0.1f)
        {
            Color color = logInBtn.GetComponent<Image>().color;
            color.a = i;

            logInBtn.GetComponent<Image>().color = color;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
