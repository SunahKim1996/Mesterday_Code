using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircleTouch_1 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject circle;
    public GameObject player;

    float xStartPoint;
    float xEndPoint;

    float yStartPoint;
    float yEndPoint;

    public Text fomulaTxt;
    int randomInt;
    
    public GameObject pointPos;

    public void Update()
    {
        transform.position = pointPos.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject.Find("GameManager").GetComponent<PuzzleHint>().howMuchUserMove++;

        xStartPoint = Input.mousePosition.x;
        yStartPoint = Input.mousePosition.y;

        GameObject.FindWithTag("playerAxis").GetComponent<PlayerRotation3>().isCircleMoving = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        xEndPoint = Input.mousePosition.x;
        yEndPoint = Input.mousePosition.y;

        if (xStartPoint - xEndPoint > 0 || yStartPoint - yEndPoint < 0)
        {
            xStartPoint = xEndPoint;
            yStartPoint = yEndPoint;

            if (circle.GetComponent<RectTransform>().localScale.x >= 0.7)
            {
                GameObject.FindWithTag("playerAxis").GetComponent<PlayerRotation3>().isCircleBigger = false;
                circle.GetComponent<Transform>().localScale = new Vector3
                (circle.GetComponent<Transform>().localScale.x - 0.01f, circle.GetComponent<Transform>().localScale.y - 0.01f, circle.GetComponent<Transform>().localScale.z);                

                randomInt = Random.Range(11, 100);
                fomulaTxt.text = randomInt.ToString();
            }
        }
        else
        {
            xStartPoint = xEndPoint;
            yStartPoint = yEndPoint;

            if (circle.GetComponent<RectTransform>().localScale.x <= 2.7)
            {
                GameObject.FindWithTag("playerAxis").GetComponent<PlayerRotation3>().isCircleBigger = true;
                circle.GetComponent<Transform>().localScale = new Vector3
                                    (circle.GetComponent<Transform>().localScale.x + 0.01f, circle.GetComponent<Transform>().localScale.y + 0.01f, circle.GetComponent<Transform>().localScale.z);

                randomInt = Random.Range(11, 100);
                fomulaTxt.text = randomInt.ToString();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(circle.GetComponent<RectTransform>().localScale.x >= 0 && circle.GetComponent<RectTransform>().localScale.x <= 1.0)
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
