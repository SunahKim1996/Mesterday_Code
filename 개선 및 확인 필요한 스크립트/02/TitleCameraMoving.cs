using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleCameraMoving : MonoBehaviour
{
    public TextMeshPro FadeOutText;
    public Camera titleCam;
    public GameObject playerCam;

    bool isTitleOn = false;

    private UserDataInfo userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;

        if (userData.clearTutorial == false)
        {
            StartCoroutine(TitleCamMoving());
            StartCoroutine(TitleTxtFadeOut());
            StartCoroutine(PlayerCamOn());
        }
        else
        {
            playerCam.SetActive(true);
        }
        
    }

    IEnumerator TitleTxtFadeOut()
    {

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 10; i++)
        {
            Color color = FadeOutText.GetComponent<TextMeshPro>().color;

            float f = i / 10.0f;
            color.a = f;
            FadeOutText.GetComponent<TextMeshPro>().color = color;

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(4f);

        isTitleOn = true;

        yield return null;
    }

    IEnumerator TitleCamMoving()
    {
        while(true)
        {
            Vector3 TargetPos = new Vector3(titleCam.transform.position.x, 9.9f, titleCam.transform.position.z);


            yield return null;

            if(isTitleOn == true)
            {                
                titleCam.transform.position = Vector3.Lerp(titleCam.transform.position, TargetPos, Time.deltaTime * 0.8f);
            }
        }
    }

    IEnumerator PlayerCamOn()
    {
        yield return new WaitForSeconds(16f);

        TouchEffectManage_star.isSecondCamOn = true;

        playerCam.SetActive(true);
        StopCoroutine(TitleTxtFadeOut());
        StopCoroutine(TitleCamMoving());

        yield return null;
    }
}
