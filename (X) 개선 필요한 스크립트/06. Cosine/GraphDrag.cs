using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //public GameObject graph;

    float xStartPoint;
    float xEndPoint;

    public GameObject dragMove;
    public GameObject dragOn;

    public Text fomulaTxt; //분자
    public Text denominator; //분모
    public Text sign; //부호
    public Text line; //함수 선
    public Text randomTxt;
    int randomInt;

    public float moveSpeed;
    public float distance;

    public List<GameObject> graph = new List<GameObject>();

    public GameObject dragMovePos;
    public GameObject dragMovePos2;

    public AudioSource soundEffect;
    public AudioClip dragEffect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOn.gameObject.SetActive(true);
        dragMove.transform.position = new Vector3(dragMovePos.transform.position.x, dragMovePos.transform.position.y, dragMovePos.transform.position.z);
        xStartPoint = Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (soundEffect.isPlaying == false)
        {
            soundEffect.PlayOneShot(dragEffect);
            soundEffect.volume = SoundManager.dragEffect;
        }

        xEndPoint = Input.mousePosition.x;

        randomInt = Random.Range(11, 100);

        randomTxt.gameObject.SetActive(true);
        fomulaTxt.gameObject.SetActive(false);
        denominator.gameObject.SetActive(false);
        sign.gameObject.SetActive(false);
        line.gameObject.SetActive(false);

        randomTxt.text = "+ " + randomInt.ToString();
        
        //fomulaTxt.text = "+ " + randomInt.ToString();

        if (xStartPoint - xEndPoint >= 0) //왼쪽으로
        {
            xStartPoint = xEndPoint;
            

            if (GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num == "2" && GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().sign == "plusInt")
            {
                Debug.Log("못감");
            }
            else
            {
                graph[0].GetComponent<RectTransform>().position = new Vector3
                (graph[0].GetComponent<RectTransform>().position.x - moveSpeed, graph[0].GetComponent<RectTransform>().position.y, graph[0].GetComponent<RectTransform>().position.z);

                graph[1].GetComponent<RectTransform>().position = new Vector3
                (graph[1].GetComponent<RectTransform>().position.x - moveSpeed, graph[1].GetComponent<RectTransform>().position.y, graph[1].GetComponent<RectTransform>().position.z);

                graph[2].GetComponent<RectTransform>().position = new Vector3
                (graph[2].GetComponent<RectTransform>().position.x - moveSpeed, graph[2].GetComponent<RectTransform>().position.y, graph[2].GetComponent<RectTransform>().position.z);

                distance += moveSpeed;
            }
        }
        else if(xStartPoint - xEndPoint < 0) //오른쪽으로
        {
            xStartPoint = xEndPoint;
            

            if (GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num == "2" && GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().sign == "minusInt")
            {
                Debug.Log("못감");
            }
            else
            {
                graph[0].GetComponent<RectTransform>().position = new Vector3
                (graph[0].GetComponent<RectTransform>().position.x + moveSpeed, graph[0].GetComponent<RectTransform>().position.y, graph[0].GetComponent<RectTransform>().position.z);

                graph[1].GetComponent<RectTransform>().position = new Vector3
                (graph[1].GetComponent<RectTransform>().position.x + moveSpeed, graph[1].GetComponent<RectTransform>().position.y, graph[1].GetComponent<RectTransform>().position.z);

                graph[2].GetComponent<RectTransform>().position = new Vector3
                (graph[2].GetComponent<RectTransform>().position.x + moveSpeed, graph[2].GetComponent<RectTransform>().position.y, graph[2].GetComponent<RectTransform>().position.z);

                distance += moveSpeed;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        randomTxt.gameObject.SetActive(false);
        fomulaTxt.gameObject.SetActive(true);
        denominator.gameObject.SetActive(true);
        sign.gameObject.SetActive(true);
        line.gameObject.SetActive(true);

        if (GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num != "0")
        {
            if(GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().sign == "plusInt")
            {
                randomTxt.gameObject.SetActive(true);
                fomulaTxt.gameObject.SetActive(false);
                denominator.gameObject.SetActive(false);
                sign.gameObject.SetActive(false);
                line.gameObject.SetActive(false);

                randomTxt.text = "+ " + GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num;
            }
            else if (GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().sign == "minusInt")
            {
                randomTxt.gameObject.SetActive(true);
                fomulaTxt.gameObject.SetActive(false);
                denominator.gameObject.SetActive(false);
                sign.gameObject.SetActive(false);
                line.gameObject.SetActive(false);

                randomTxt.text = "- " + GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num;
            }
            else if (GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().sign == "minus")
            {
                fomulaTxt.text = GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num;
                sign.text = "-";
            }
            else if (GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().sign == "plus")
            {
                fomulaTxt.text = GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num;
                sign.text = "+";
            }

            //fomulaTxt.text = GameObject.FindWithTag("yAxis").GetComponent<GraphFomula>().num;
        }
        else
        {
            dragMove.transform.position = new Vector3(dragMovePos2.transform.position.x, dragMovePos2.transform.position.y, dragMovePos2.transform.position.z);
            dragOn.gameObject.SetActive(false);
        }
        
    }
}
