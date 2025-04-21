using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryNote_FlipCard : MonoBehaviour
{
    public float waitTime;
    public float x, y, z;

    public GameObject cardBack;
    bool cardBackIsActive;
    bool isCardMoving;
    int timer;

    public AudioSource soundEffect;
    public AudioClip paperEffect;

    private void Start()
    {
        cardBackIsActive = false;
        isCardMoving = false;
    }

    public void StartFlip()
    {
        if (isCardMoving == false)
        {
            soundEffect.PlayOneShot(paperEffect);
            soundEffect.volume = SoundManager.paperEffect;

            StartCoroutine(CalculatedFlip());
            isCardMoving = true;
        }
    }

    public void Flip()
    {
        if (cardBackIsActive == true)
        {
            cardBack.SetActive(false);
            cardBackIsActive = false;
        }
        else
        {
            cardBack.SetActive(true);
            cardBackIsActive = true;
        }
    }


    IEnumerator CalculatedFlip()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(waitTime);
            transform.Rotate(new Vector3(x, y, z));
            timer++;

            if (timer == 3 || timer == -3)
            {
                Flip();
            }
        }

        timer = 0;
        isCardMoving = false;
    }

    public void CardReset() //요약노트 닫을 때 카드 원상복귀
    {
        StopCoroutine(CalculatedFlip());

        cardBackIsActive = false;
        isCardMoving = false;
        
        cardBack.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        
        timer = 0;
    }
}


