using System.Collections;
using UnityEngine;
using TMPro;

public class TitleCameraMoving : MonoBehaviour
{
    [SerializeField] TextMeshPro FadeOutText;
    [SerializeField] Transform titleCam;
    [SerializeField] Transform playerCam;

    [SerializeField] DialogMainTutorial dialogMainTutorial;

    UserDataInfo userData;

    void Start()
    {
        userData = UserData.instance.userData;

        if (userData.clearTutorial == false)
            StartCoroutine(TitleTxtFadeOut());
        else
            playerCam.gameObject.SetActive(true);
    }

    /// <summary>
    /// 타이틀 연출
    /// </summary>
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

        StartCoroutine(TitleCamMoving());
    }

    /// <summary>
    /// 카메라 연출
    /// </summary>
    IEnumerator TitleCamMoving()
    {
        Vector3 targetPos = playerCam.position;
        targetPos.y -= 0.02f;

        while (true)
        {
            titleCam.position = Vector3.Lerp(titleCam.position, targetPos, Time.deltaTime * 0.8f);

            float dis = (targetPos - titleCam.position).magnitude;
            if (Mathf.Abs(dis) <= 0.02f)
            {
                yield return new WaitForSeconds(0.5f);
                EndCameraDirect();
                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 카메라 연출 종료
    /// </summary>
    void EndCameraDirect()
    {
        TouchEffectManage_star.isSecondCamOn = true; //TODO
        dialogMainTutorial.StartDialog();

        // Player 카메라로 전환 
        playerCam.gameObject.SetActive(true);
    }
}
