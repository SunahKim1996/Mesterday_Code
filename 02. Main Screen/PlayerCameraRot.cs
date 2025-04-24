using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCameraRot : MonoBehaviour, IDragHandler
{
    public Transform targetObjTrans;

    float xAngle = 0f;
    float yAngle = 0f;

    /// <summary>
    /// 화면 드래그 시, 카메라 회전 동작 
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        xAngle = eventData.delta.x * Time.deltaTime;

        if (xAngle < 0)
        {
            if (targetObjTrans.transform.localEulerAngles.y >= 180)
            {
                yAngle = targetObjTrans.transform.localEulerAngles.y - 360f;

                if (yAngle <= 75f)
                    targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
            }
            else
            {
                if (targetObjTrans.transform.localEulerAngles.y <= 75f)
                    targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
            }

        }
        else if (xAngle > 0)
        {
            if (targetObjTrans.transform.localEulerAngles.y >= 180)
            {
                yAngle = targetObjTrans.transform.localEulerAngles.y - 360f;

                if (yAngle >= -50f)
                    targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
            }
            else
                targetObjTrans.Rotate(0, -xAngle, 0, Space.World);
        }
    }
}