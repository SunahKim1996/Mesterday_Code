using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject endingDoor;
    public GameObject endingHandle;
    public GameObject endingPos;

    public GameObject player;
    public Camera playerCam;
    
    float alphaColor = 0f;  //시작은 무("0")에서
    float orizR;
    float orizG;
    float orizB;

    private UserDataInfo userData;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("EndingClear"); //ending 작동 확인용

        endingDoor.SetActive(false);
        endingHandle.SetActive(false);
        endingPos.SetActive(false);
    }

    void Start()
    {
        userData = UserData.instance.userData;

        if (userData.clearEnding)//ending 클리어 기록 보유
        {
            endingDoor.SetActive(true);
            endingHandle.SetActive(true);
            endingPos.SetActive(true);
        }
        else
        {
            if (userData.clear06 && userData.stage6_Score > 0)//1-6 스테이지 별 1개 이상으로 클리어
            {
                endingDoor.SetActive(true);
                endingHandle.SetActive(true);
                endingPos.SetActive(true);

                if (userData.EndingOpen)
                {
                    StartCoroutine(SpeechBubbleOn());
                    
                    /*
                    orizR = endingDoor.GetComponent<Renderer>().material.color.r;
                    orizG = endingDoor.GetComponent<Renderer>().material.color.g;
                    orizB = endingDoor.GetComponent<Renderer>().material.color.b; 
                    endingDoor.GetComponent<Renderer>().material.color = new Color(orizR, orizG, orizB, alphaColor);

                    StartCoroutine(Door());
                    */

                    //DataBaseSave.instance.writeClearData_endingOpen(AnonymousLogin.user.UserId, true);
                    UserData.instance.SetUserDataInfo("EndingOpen", true);
                }
            }
        }
    }

    public GameObject speechBubble;
    public Text bubbleTxt;

    IEnumerator Door()
    {
        while (true)
        {
            yield return null;

            if (alphaColor < 0.98f)
            {
                alphaColor = Mathf.Lerp(alphaColor, 1f, Time.deltaTime * 0.5f);
                endingDoor.GetComponent<Renderer>().material.color = new Color(orizR, orizG, orizB, alphaColor);
            }
        }
    }

    IEnumerator SpeechBubbleOn()
    {       
        /*
        speechBubble.GetComponent<RectTransform>().position 
            = playerCam.WorldToScreenPoint(player.GetComponent<Transform>().position + new Vector3(x, y, z));
        */

        player.transform.LookAt(endingDoor.transform.position);

        speechBubble.SetActive(true);
        bubbleTxt.text = "!";
        yield return new WaitForSeconds(1.5f);
        speechBubble.SetActive(false);
    }

    private void Update()
    {
        speechBubble.GetComponent<RectTransform>().position 
            = playerCam.WorldToScreenPoint(player.GetComponent<Transform>().position + new Vector3(-0.2f, 0.8f, 0f));
    }
}
