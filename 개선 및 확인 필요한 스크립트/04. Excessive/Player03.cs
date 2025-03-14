using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player03 : MonoBehaviour
{
    public int howMuchItem = 0;

    public GameObject speechBubble;
    public Camera puzzleCam;

    public List<GameObject> balls = new List<GameObject>();
    int ballInt = 0;

    public AudioSource soundEffect;
    public AudioClip na_submitEffect;
    public AudioClip getEffect;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "item")
        {
            if (other.gameObject.GetComponent<BallRandomMoving>().isBallMoving == true)
            {
                StartCoroutine(SpeechBubbleOn());
            }
            else
            {
                soundEffect.PlayOneShot(getEffect);
                soundEffect.volume = SoundManager.getEffect;

                balls[ballInt].SetActive(false);
                ballInt++;

                Destroy(other.gameObject);
                howMuchItem++;
            }
        }
    }

    IEnumerator SpeechBubbleOn()
    {
        soundEffect.PlayOneShot(na_submitEffect);
        soundEffect.volume = SoundManager.na_submitEffect;

        speechBubble.SetActive(true);
        yield return new WaitForSeconds(1f);
        speechBubble.SetActive(false);
    }

    private void Update()
    {
        speechBubble.GetComponent<RectTransform>().position = puzzleCam.WorldToScreenPoint(this.GetComponent<Transform>().position + new Vector3(-1.5f, 0, 2f));

        if (howMuchItem == 5)
        {
            GameObject.Find("GameManager").GetComponent<CameraMoving03>().isPuzzleGameStart = false;
            GameObject.Find("GameManager").GetComponent<StarScore03>().isStarScoreOnTime = true;
        }

    }

}
