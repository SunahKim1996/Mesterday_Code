using System.Collections;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    [SerializeField] float speed = 0.0001f;
    bool cardBackIsActive = false;
    bool isCardMoving = false;
    Coroutine coroutine;

    public void OnStartFlip(GameObject curCard, GameObject cardBackObj, GameObject cardForwardObj)
    {
        if (isCardMoving)
            return;

        isCardMoving = true;
        SoundManager.instance.PlaySFX(SoundClip.PaperSFX, 0.4f);
        coroutine = StartCoroutine(Flip(curCard, cardBackObj, cardForwardObj));
    }

    IEnumerator Flip(GameObject curCard, GameObject cardBackObj, GameObject cardForwardObj)
    {        
        float currentY = curCard.transform.eulerAngles.y;
        float targetY = ((int)Mathf.Abs(currentY) >= 180f) ? 0f : 180f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentY, targetY)) > 0.1f)
        {
            float step = speed * Time.deltaTime * 180f; // 1초에 speed만큼 회전하도록 조정
            currentY = Mathf.MoveTowardsAngle(currentY, targetY, step);
            curCard.transform.rotation = Quaternion.Euler(curCard.transform.eulerAngles.x, currentY, curCard.transform.eulerAngles.z);

            float target = (targetY == 0f) ? 270f : 90f;
            if (Mathf.Abs(Mathf.DeltaAngle(currentY, target)) <= 3f)
            {
                cardBackObj.SetActive(!(targetY == 0f));
                cardForwardObj.SetActive((targetY == 0f));
            }

            yield return null;
        }

        // 보정
        curCard.transform.rotation = Quaternion.Euler(curCard.transform.eulerAngles.x, targetY, curCard.transform.eulerAngles.z);
        cardBackIsActive = !cardBackIsActive;
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


