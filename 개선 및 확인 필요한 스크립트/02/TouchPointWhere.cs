using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointWhere : MonoBehaviour
{


    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            TouchPointMoving.isTouchPointOnFloor = true;
        }

        else
        {
            TouchPointMoving.isTouchPointOnFloor = false;
        }
    }
}
