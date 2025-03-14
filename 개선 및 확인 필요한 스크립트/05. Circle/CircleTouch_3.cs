using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircleTouch_3 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject circle;
    public GameObject player;
    float xStartPoint;
    float xEndPoint;
    float xPrePoint;

    public Text fomulaTxt;
    int randomInt;

    float screenCenterX;

    public void OnBeginDrag(PointerEventData eventData)
    {
        screenCenterX = Screen.width / 2f;

        GameObject.Find("GameManager").GetComponent<PuzzleHint>().howMuchUserMove++;

        xStartPoint = Input.mousePosition.x;

        GameObject.FindWithTag("playerAxis").GetComponent<PlayerRotation3>().isCircleMoving = true;
    }

    void ChangeScale(float value)
    {
        float clamp = Mathf.Clamp(circle.transform.localScale.x + value, 0.7f, 2.7f);
        circle.transform.localScale = new Vector3(clamp, clamp, circle.transform.localScale.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        xEndPoint = Input.mousePosition.x;

        //Debug.Log($"Center {screenCenterX} // Start : {xStartPoint} / End {xEndPoint}");

        //왼쪽 화면에서 시작 
        if (xStartPoint < screenCenterX)
        {
            // 왼 -> 오 (줄어듦)
            if (xEndPoint > xPrePoint)
                ChangeScale(-0.01f);

            // 오 -> 왼 (커짐)
            else if (xEndPoint < xPrePoint)
                ChangeScale(0.01f);
        }
        
        //오른쪽 화면에서 시작 
        else
        {
            // 오 -> 왼 (줄어듦)
            if (xEndPoint < xPrePoint)
                ChangeScale(-0.01f);

            // 왼 -> 오 (커짐)
            else if (xEndPoint > xPrePoint)
                ChangeScale(0.01f);
        }

        xPrePoint = xEndPoint;

        GameObject.FindWithTag("playerAxis").GetComponent<PlayerRotation3>().isCircleBigger = false;

        randomInt = Random.Range(11, 100);
        fomulaTxt.text = randomInt.ToString();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (circle.GetComponent<RectTransform>().localScale.x >= 0 && circle.GetComponent<RectTransform>().localScale.x <= 1.0)
        {
            fomulaTxt.text = "  1";
        }
        else if (circle.GetComponent<RectTransform>().localScale.x > 1.0 && circle.GetComponent<RectTransform>().localScale.x <= 1.3)
        {
            fomulaTxt.text = "  2";
        }
        else if (circle.GetComponent<RectTransform>().localScale.x > 1.3 && circle.GetComponent<RectTransform>().localScale.x <= 1.6)
        {
            fomulaTxt.text = "  3";
        }
        else if (circle.GetComponent<RectTransform>().localScale.x > 1.6 && circle.GetComponent<RectTransform>().localScale.x <= 1.9)
        {
            fomulaTxt.text = "  4";
        }
        else if (circle.GetComponent<RectTransform>().localScale.x > 1.9 && circle.GetComponent<RectTransform>().localScale.x <= 2.2)
        {
            fomulaTxt.text = "  5";
        }
        else if (circle.GetComponent<RectTransform>().localScale.x > 2.2 && circle.GetComponent<RectTransform>().localScale.x <= 2.5)
        {
            fomulaTxt.text = "  6";
        }
        else if (circle.GetComponent<RectTransform>().localScale.x > 2.5 && circle.GetComponent<RectTransform>().localScale.x <= 3.0)
        {
            fomulaTxt.text = "  7";
        }

        GameObject.FindWithTag("playerAxis").GetComponent<PlayerRotation3>().isCircleMoving = false;
    }
}
