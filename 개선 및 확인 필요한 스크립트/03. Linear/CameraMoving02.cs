using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoving02 : MonoBehaviour
{
    public Camera MainCam;
    public Camera goToPuzzleCam;
    public Camera comeBackCam;

    public Image img;

    public GameObject goPuzzleOn;
    public GameObject goPuzzleOn2;
    public GameObject goPuzzleOn3;

    public GameObject goPuzzleOff;
    public GameObject gopuzzleOff2;

    public GameObject graph;
    
    public bool isPuzzleGameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().mute = true;

        img.gameObject.SetActive(false);

        StartCoroutine(CameraSoundOn());
        StartCoroutine(startGoToPuzzleCam());
        StartCoroutine(BackToMainCam());
        StartCoroutine(ScreenChange());

        isCamMoving = false;
        isMainCamOpen = false;

        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());

        goPuzzleOn.SetActive(false);
        goPuzzleOn2.SetActive(false);
        goPuzzleOn3.SetActive(false);

        goPuzzleOff.SetActive(true);
        gopuzzleOff2.SetActive(true);

        graph.transform.position = new Vector3(graph.transform.position.x, -2.37f, graph.transform.position.z);

        distance = Vector3.Distance(MainCam.transform.position, goToPuzzleCam.transform.position);
    }

    public bool isCamMoving = false;
    float distance;

    IEnumerator startGoToPuzzleCam()
    {
        while (true)
        {
            yield return null;

            if (isCamMoving == true)
            {
                Vector3 puzzleCamPos = new Vector3(goToPuzzleCam.transform.position.x, goToPuzzleCam.transform.position.y, goToPuzzleCam.transform.position.z);
                MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, puzzleCamPos, Time.deltaTime);
                MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, goToPuzzleCam.transform.rotation, Time.deltaTime);

                distance = Vector3.Distance(MainCam.transform.position, goToPuzzleCam.transform.position);
            }
        }
    }

    public AudioSource soundEffect;
    public AudioClip cameraEffect;

    IEnumerator CameraSoundOn()
    {
        while (true)
        {
            yield return null;

            if(isCamMoving == true)
            {
                soundEffect.PlayOneShot(cameraEffect);
                soundEffect.volume = SoundManager.cameraEffect;

                break;
            }
        }
    }

    IEnumerator ScreenChange() //퍼즐 게임 시작
    {
        while (true)
        {
            yield return null;

            if (distance < 4f)
            {
                img.gameObject.SetActive(true);
                isFadeOutTime = true;
                yield return new WaitForSeconds(1f);
                isFadeOutTime = false;

                GetComponent<AudioSource>().mute = false;

                MainCam.gameObject.SetActive(false);
                goPuzzleOn.SetActive(true);
                goPuzzleOn2.SetActive(true);
                goPuzzleOn3.SetActive(true);

                TouchEffectManage_star.isSecondCamOn = true;

                goPuzzleOff.SetActive(false);
                gopuzzleOff2.SetActive(false);

                graph.transform.position = new Vector3(graph.transform.position.x, 15f, graph.transform.position.z);

                isFadeInTime = true;
                yield return new WaitForSeconds(0.5f);
                isPuzzleGameStart = true;
                isFadeInTime = false;

                img.gameObject.SetActive(false);
                StartCoroutine(ScreenChange_2());

                isCamMoving = false;

                break;
            }
        }

    }

    IEnumerator ScreenChange_2() //퍼즐 게임 종료
    {
        while (true)
        {
            yield return null;

            if (isPuzzleGameStart == false)
            {
                img.gameObject.SetActive(true);

                isFadeOutTime = true;
                yield return new WaitForSeconds(1f);
                isFadeOutTime = false;

                GetComponent<AudioSource>().mute = true;

                MainCam.gameObject.SetActive(true);
                goPuzzleOn.SetActive(false);
                goPuzzleOn2.SetActive(false);
                goPuzzleOn3.SetActive(false);

                TouchEffectManage_star.isSecondCamOn = false;

                goPuzzleOff.SetActive(true);
                gopuzzleOff2.SetActive(true);

                graph.transform.position = new Vector3(graph.transform.position.x, -2.37f, graph.transform.position.z);


                isFadeInTime = true;
                yield return new WaitForSeconds(0.5f);
                isFadeInTime = false;

                img.gameObject.SetActive(false);

                isMainCamOpen = true;

                break;
            }
        }

    }

    bool isFadeOutTime = false;
    bool isFadeInTime = false;

    IEnumerator FadeOut()
    {
        while (true)
        {
            yield return null;

            if (isFadeOutTime == true)
            {
                Debug.Log("Fade Out");

                for (int i = 0; i <= 10; i++)
                {
                    Color color = img.GetComponent<Image>().color;

                    float f = i / 10.0f;
                    color.a = f;
                    img.GetComponent<Image>().color = color;

                    yield return new WaitForSeconds(0.01f);
                }


                isFadeOutTime = false;
            }
        }
    }

    IEnumerator FadeIn()
    {
        while (true)
        {
            yield return null;

            if (isFadeInTime == true)
            {
                Debug.Log("Fade In");

                for (int i = 10; i > 0; i--)
                {
                    Color color = img.GetComponent<Image>().color;

                    float f = i / 10.0f;
                    color.a = f;
                    img.GetComponent<Image>().color = color;

                    yield return new WaitForSeconds(0.01f);
                }


                isFadeInTime = false;
            }
        }
    }

    public bool isMainCamOpen = false;

    public float distance2;

    IEnumerator BackToMainCam()
    {
        while (true)
        {
            yield return null;

            if (isMainCamOpen == true)
            {
                Vector3 comBackCam = new Vector3(comeBackCam.transform.position.x, comeBackCam.transform.position.y, comeBackCam.transform.position.z);
                MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, comBackCam, Time.deltaTime);
                MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, comeBackCam.transform.rotation, Time.deltaTime);
                
                distance2 = Vector3.Distance(MainCam.transform.position, comeBackCam.transform.position);
            }
        }
    }
}
