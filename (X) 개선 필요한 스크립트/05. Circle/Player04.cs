using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player04 : MonoBehaviour
{
    public int howMuchItem = 0;
    public GameObject speechBubble;

    public Camera puzzleCam;

    public AudioSource soundEffect;
    public AudioClip getEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "item")
        {
            howMuchItem++;
            Destroy(other.gameObject);

            StartCoroutine(SpeechBubble());
        }
    }

    IEnumerator SpeechBubble()
    {
        soundEffect.PlayOneShot(getEffect);
        soundEffect.volume = SoundManager.getEffect;

        speechBubble.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        speechBubble.SetActive(false);

    }

    private void Update()
    {
        speechBubble.GetComponent<RectTransform>().position = puzzleCam.WorldToScreenPoint(this.GetComponent<RectTransform>().position + new Vector3(-1.5f, 0, 2f));

        if (howMuchItem == 10)
        {
            GameObject.Find("GameManager").GetComponent<CameraMoving04>().isPuzzleGameStart = false;
            GameObject.Find("GameManager").GetComponent<StarScore04>().isStarScoreOnTime = true;
        }
    }
}
