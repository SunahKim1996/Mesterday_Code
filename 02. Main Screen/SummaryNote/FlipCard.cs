using System.Collections;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float x, y, z;

    bool cardBackIsActive = false;
    bool isCardMoving = false;
    int timer;
    Coroutine coroutine;

    public void OnStartFlip(GameObject cardBackObj)
    {
        if (isCardMoving)
            return;

        isCardMoving = true;
        SoundManager.instance.PlaySFX(SoundClip.peopleSFX, 0.4f);
        coroutine = StartCoroutine(Flip(cardBackObj));
    }

    IEnumerator Flip(GameObject cardBackObj)
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(waitTime);
            transform.Rotate(new Vector3(x, y, z));
            timer++;

            if (timer >= 3)
            {
                cardBackObj.SetActive(!cardBackIsActive);
                cardBackIsActive = !cardBackIsActive;
            }
        }

        StopFlip();
    }

    /// <summary>
    /// Flip 종료 
    /// </summary>
    void StopFlip()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        cardBackIsActive = false;
        isCardMoving = false;
        timer = 0;
    }

    /// <summary>
    /// 요약노트 닫을 때 카드 원상복귀
    /// </summary>
    public void InitNote()
    {
        cardBackIsActive = false;
        StopFlip();
    }
}


