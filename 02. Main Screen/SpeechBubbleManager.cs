using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    [SerializeField] GameObject speechBubble;
    [SerializeField] Text speechBubbleTxt;
    [SerializeField] Camera playerCam;

    public bool isTalking = false;

    Coroutine cor = null;

    void LateUpdate()
    {
        speechBubble.GetComponent<RectTransform>().position
            = playerCam.WorldToScreenPoint(transform.position + new Vector3(0f, 0.9f, 0f));
    }

    public void StartSpeechBubbleGuide(string[] dialogList)
    {
        if (cor != null)
            StopCoroutine(cor);

        cor = StartCoroutine(ShowSpeechBubbleGuide(dialogList));
    }

    IEnumerator ShowSpeechBubbleGuide(string[] dialogList)
    {
        isTalking = true;

        for (int i = 0; i < dialogList.Length; i++)
        {
            speechBubble.SetActive(true);
            speechBubbleTxt.text = dialogList[i];
            yield return new WaitForSeconds(2f);
            speechBubble.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

        isTalking = false;
    }
}
