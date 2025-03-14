using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentMoving02_2 : MonoBehaviour
{
    public GameObject movePos;
        
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GameManager").GetComponent<CameraMoving02>().isMainCamOpen == true)
        {
            if(GameObject.Find("GameManager").GetComponent<StarScore02>().howMuchStarScore != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, movePos.transform.position, Time.deltaTime * 3f);
                transform.LookAt(movePos.transform);

                if(Vector3.Distance(transform.position, movePos.transform.position) < 0.1f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, movePos.transform.rotation, Time.deltaTime * 2f);
                }
            }
        }        
    }
}
