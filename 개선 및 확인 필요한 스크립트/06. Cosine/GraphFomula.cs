using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphFomula : MonoBehaviour
{
    public string num;
    public string sign;

    private void OnTriggerEnter(Collider other)
    {
        num = other.gameObject.name;
        sign = other.gameObject.tag;

        Debug.Log("num = " + num);
        Debug.Log("sign = " + sign);
    }

    private void Update()
    {
        if(GameObject.Find("GameManager").GetComponent<GraphMoving06>().StartNum == 1)
        {
            if ((num == "6" && sign == "plus") || (num == "2" && sign == "minus"))
            {
                GameObject.Find("GameManager").GetComponent<GraphMoving06>().isPuzzleClear = true;
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GraphMoving06>().isPuzzleClear = false;
            }
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<GraphMoving06>().isPuzzleClear = false;
        }
    }
}
