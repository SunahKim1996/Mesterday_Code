using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphDrag_03Linear : GraphDrag
{
    float yStartPoint;
    float yEndPoint;
        
    [SerializeField] GameObject denominatorObj; // 분모
    [SerializeField] GameObject slopeLine; // 분수 선

    [SerializeField] Text slopeText;
    [SerializeField] Transform slopeIntTrans; // 기울기가 정수일 때 위치
    [SerializeField] Transform slopeNotIntTrans; // 기울기가 분수일 때 위치

    [SerializeField] GameObject graph;
    [SerializeField] float moveSpeed;

    void Start()
    {
        InitSlope();
    }

    void InitSlope()
    {
        slopeText.transform.position = slopeIntTrans.position;
        slopeText.fontSize = 65;

        ToggleDenominator(false);

        slopeText.text = "1";
    }

    void ToggleDenominator(bool state)
    {
        slopeLine.SetActive(state);
        denominatorObj.SetActive(state);
    }

    protected override void BeginDrag(PointerEventData eventData)
    {        
        yStartPoint = Input.mousePosition.y;

        ToggleDenominator(false);

        slopeText.transform.position = slopeIntTrans.position;
        slopeText.fontSize = 65;
    }

    protected override void Drag(PointerEventData eventData)
    {
        SoundManager.instance.PlaySFX(SoundClip.dragSFX, 0.8f);

        yEndPoint = Input.mousePosition.y;

        int randomInt = Random.Range(1, 10);
        slopeText.text = randomInt.ToString();

        // 그래프 기울기 변경 
        yStartPoint = yEndPoint;

        if (yStartPoint - yEndPoint >= 0)
        {
            Debug.Log("아래로");
                        
            if (graph.transform.eulerAngles.y - 360 < -15f)
                graph.transform.Rotate(new Vector3(0f, moveSpeed, 0f) * Time.deltaTime);
        }
        else if (yStartPoint - yEndPoint < 0)
        {
            Debug.Log("위로");

            if (graph.transform.eulerAngles.y - 360 > -75f)
                graph.transform.Rotate(new Vector3(0f, -moveSpeed, 0f) * Time.deltaTime);
        }
    }

    protected override void EndDrag(PointerEventData eventData)
    {
        Debug.Log((Mathf.Round(Mathf.Abs(Mathf.Tan(-15f * Mathf.Deg2Rad))*10f))+ "/10");

        float slope = Mathf.Abs(Mathf.Tan((graph.transform.eulerAngles.y - 360) * Mathf.Deg2Rad));

        //정수일 때
        if ((Mathf.Round(slope * 10f)) % 1 == 0) 
        {
            slopeText.transform.position = slopeIntTrans.position;
            slopeText.text = (Mathf.Round(slope)).ToString();
        }

        //분수일 때
        else
        {
            ToggleDenominator(true);

            slopeText.transform.position = slopeNotIntTrans.position;
            slopeText.fontSize = 54;
            slopeText.text = (Mathf.Round(slope * 10f)).ToString();
        }       
    }
}
