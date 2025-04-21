using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointWhere07 : MonoBehaviour
{
    public static bool isPlayerKnowWalking;

    private void Start()
    {
        isPlayerKnowWalking = false;
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            TouchPointMoving07.isTouchPointOnFloor = true;
            isPlayerKnowWalking = true;
        }

        else
        {
            TouchPointMoving07.isTouchPointOnFloor = false;
        }
    }
}
