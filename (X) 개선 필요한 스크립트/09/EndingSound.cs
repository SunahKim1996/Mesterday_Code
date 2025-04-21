using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingSound : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioClip callingEffect;
    public AudioClip getCallEffect;

    public GameObject FadeOutImage;
    public GameObject bg_image;

    // Start is called before the first frame update
    void Start()
    {
        bg_image.SetActive(false);
        StartCoroutine(CallingStart());
    }

    public bool isCallingStart = false;

    IEnumerator CallingStart()
    {
        while (true)
        {
            yield return null;

            if(isCallingStart == true)
            {
                yield return new WaitForSeconds(1.5f);

                soundEffect.PlayOneShot(callingEffect);
                soundEffect.volume = SoundManager.callingEffect;

                yield return new WaitForSeconds(2f);

                soundEffect.PlayOneShot(callingEffect);
                soundEffect.volume = SoundManager.callingEffect;

                yield return new WaitForSeconds(2f);

                soundEffect.PlayOneShot(getCallEffect);
                soundEffect.volume = SoundManager.getCallEffect;

                yield return new WaitForSeconds(1.5f);

                StartCoroutine(FadeIN());

                break;

            }
        }
    }

    IEnumerator FadeIN()
    {
        bg_image.SetActive(true);

        for (int i = 10; i > -2; i--)
        {
            Color color = FadeOutImage.GetComponent<Image>().color;

            float f = i / 10.0f;
            color.a = f;
            FadeOutImage.GetComponent<Image>().color = color;

            PlayerPrefs.SetInt("01_Clear", 1);

            yield return new WaitForSeconds(0.1f);
        }

        GetComponent<TitleCamera>().isTitleOn = true;
        PlayerPrefs.SetInt("EndingClear", 1);
        //SceneManager.LoadScene(0);

        yield return null;
    }
}
