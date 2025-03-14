using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCameraMoving : MonoBehaviour, IDragHandler
{
    public Transform targetObjTrans;

    public float xAngle = 0f;
    public float yAngle = 0f;

    float y;

    public void OnDrag(PointerEventData eventData)
    {
        xAngle = eventData.delta.x * Time.deltaTime ;
        //yAngle = eventData.delta.y * Time.deltaTime * 0.5f;

        //Debug.Log("xAngle = " + xAngle);
        //Debug.Log("y = " + targetObjTrans.transform.localEulerAngles.y);
        
        if(TouchPointMoving.isTalking == false)
        {
            if (xAngle < 0)
            {
                if (targetObjTrans.transform.localEulerAngles.y >= 180)
                {
                    y = targetObjTrans.transform.localEulerAngles.y - 360f;

                    if (y <= 75f)
                    {
                        targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
                    }
                }
                else
                {
                    if (targetObjTrans.transform.localEulerAngles.y <= 75f)
                    {
                        targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
                    }
                }

            }
            else if (xAngle > 0)
            {
                if (targetObjTrans.transform.localEulerAngles.y >= 180)
                {
                    y = targetObjTrans.transform.localEulerAngles.y - 360f;

                    if (y >= -50f)
                    {
                        targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
                    }
                }
                else
                {
                    targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
                }

            }
        }

        
    }
}