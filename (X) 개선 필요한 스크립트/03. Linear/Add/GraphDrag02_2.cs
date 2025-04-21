using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphDrag02_2 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //public GameObject graph;

    float yStartPoint;
    float yEndPoint;
        
    public Text fomulaTxt_x;

    public GameObject denominator; //분모
    public GameObject denominator_line;
    public GameObject fomulaTxt_x_int; //정수일 때 위치
    public GameObject fomulaTxt_x_fraction; //분수일 때 위치

    int randomInt;

    public GameObject graph;

    public float moveSpeed;

    public AudioSource soundEffect;
    public AudioClip dragEffect;

    public void OnBeginDrag(PointerEventData eventData)
    {        
        yStartPoint = Input.mousePosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(soundEffect.isPlaying == false)
        {
            soundEffect.PlayOneShot(dragEffect);
            soundEffect.volume = SoundManager.dragEffect;
        }
        

        yEndPoint = Input.mousePosition.y;

        denominator.SetActive(false);
        denominator_line.SetActive(false);
        fomulaTxt_x.transform.position = fomulaTxt_x_int.transform.position;
        fomulaTxt_x.fontSize = 65;
        randomInt = Random.Range(1, 10);
        fomulaTxt_x.text = randomInt.ToString();

        if (yStartPoint - yEndPoint >= 0) // 아래로
        {
            Debug.Log("아래로");
            yStartPoint = yEndPoint;            

            if (graph.transform.eulerAngles.y - 360 < -15f)
            {
                graph.transform.Rotate(new Vector3(0f, moveSpeed, 0f) * Time.deltaTime);
            }
        }
        else if (yStartPoint - yEndPoint < 0) //위로
        {
            Debug.Log("위로");
            yStartPoint = yEndPoint;

            if (graph.transform.eulerAngles.y - 360 > -75f)
            {
                graph.transform.Rotate(new Vector3(0f, -moveSpeed, 0f) * Time.deltaTime);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log((Mathf.Round(Mathf.Abs(Mathf.Tan(-15f * Mathf.Deg2Rad))*10f))+ "/10");

        float slope = Mathf.Abs(Mathf.Tan((graph.transform.eulerAngles.y - 360) * Mathf.Deg2Rad));

        if ((Mathf.Round(slope * 10f)) / 10f == 1 || (Mathf.Round(slope * 10f)) / 10f == 2 || (Mathf.Round(slope * 10f)) / 10f == 3) //정수일 때
        {
            denominator.SetActive(false);
            denominator_line.SetActive(false);
            fomulaTxt_x.transform.position = fomulaTxt_x_int.transform.position;
            fomulaTxt_x.fontSize = 65;
            fomulaTxt_x.text = (Mathf.Round(slope)).ToString();
        }
        else //분수일 때
        {
            denominator.SetActive(true);
            denominator_line.SetActive(true);
            fomulaTxt_x.transform.position = fomulaTxt_x_fraction.transform.position;
            fomulaTxt_x.fontSize = 54;
            fomulaTxt_x.text = (Mathf.Round(slope * 10f)).ToString();
        }       

    }
}
