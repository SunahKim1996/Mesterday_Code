using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCamera : MonoBehaviour
{
    public Text FadeOutText_title;
    public Text FadeOutText_thanks;

    public Text goBackText;

    public bool isTitleOn = false;
    string playerName;

    private UserDataInfo userData;

    private void Start()
    {
        userData = UserData.instance.userData;

        StartCoroutine(TitleTxtFadeOut(FadeOutText_title));
        StartCoroutine(GoBackMainScreen());
        StartCoroutine(LoadScene());

        playerName = userData.nickName;
        FadeOutText_thanks.text = playerName + "님, 플레이 해주셔서 감사합니다";
    }

    IEnumerator TitleTxtFadeOut(Text FadeOutObject)
    {
        while(true)
        {
            yield return null;

            if (isTitleOn == true)
            {
                StartCoroutine(Wait());
                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < 10; i++)
                {
                    Color color = FadeOutObject.GetComponent<Text>().color;

                    float f = i / 10.0f;
                    color.a = f;
                    FadeOutObject.GetComponent<Text>().color = color;

                    yield return new WaitForSeconds(0.1f);
                }

                isTitleOn = false;
                break;
            }
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(TitleTxtFadeOut(FadeOutText_thanks));

        yield return new WaitForSeconds(3f);

        isGoBackTime = true;
    }

    bool isGoBackTime = false;

    IEnumerator GoBackMainScreen()
    {
        while (true)
        {
            yield return null;

            if(isGoBackTime == true)
            {
                goBackText.gameObject.SetActive(true);

                for (int i = 5; i < 11; i++)
                {
                    Color color = goBackText.GetComponent<Text>().color;

                    float f = i / 10.0f;
                    color.a = f;
                    goBackText.GetComponent<Text>().color = color;

                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 11; i > 5; i--)
                {
                    Color color = goBackText.GetComponent<Text>().color;

                    float f = i / 10.0f;
                    color.a = f;
                    goBackText.GetComponent<Text>().color = color;

                    yield return new WaitForSeconds(0.1f);
                }
                
            }
        }
    }

    IEnumerator LoadScene()
    {
        while (true)
        {
            yield return null;

            if(isGoBackTime == true)
            {
                if (Input.GetMouseButtonDown(0) == true)
                {
                    //DataBaseSave.instance.writeClearData_ending(AnonymousLogin.user.UserId, true);
                    UserData.instance.SetUserDataInfo("clearEnding", true);
                    GetComponent<GoToMainScene>().isReadyToGoToMain = true;
                }
            }
        }
    }
}
