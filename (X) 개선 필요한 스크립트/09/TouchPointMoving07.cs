using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPointMoving07 : MonoBehaviour, IPointerDownHandler
{
    public GameObject player;
    float targetDis;
    public float moveSpeed;

    public Camera cam;
    public GameObject touchEffect;

    RaycastHit hit;

    public static bool isTouchPointOnFloor = false;
    //public static bool isTalking = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(EndingConversation.isConversationOn == false)
        {
            Ray ray = cam.ScreenPointToRay(eventData.position);
            Physics.Raycast(ray, out hit);

            touchEffect.transform.position = hit.point;

            StopCoroutine("PlayerMove");
            isTouchPointOnFloor = false;

            StartCoroutine("PlayerMove");
        }
    }
    

    IEnumerator PlayerMove()
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.transform.tag == "floor")
            {
                touchEffect.SetActive(true);
            }
            else
            {
                touchEffect.SetActive(false);
            }
        }           
            

        while (true)
        {            
            yield return null;

            if (EndingConversation.isConversationOn == false)
            {
                if (isTouchPointOnFloor == true)
                {
                    player.GetComponent<Animator>().SetInteger("anim", 1);
                    targetDis = (hit.point - player.transform.position).magnitude;

                    player.transform.LookAt(hit.point);
                    player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                    moveSpeed = 1;
                    player.GetComponent<Animator>().enabled = true;

                    if (targetDis <= 0.1f)
                    {
                        touchEffect.SetActive(false);
                        player.GetComponent<Animator>().SetInteger("anim", 0);

                        isTouchPointOnFloor = false;
                        StopCoroutine("PlayerMove");

                    }

                    yield return null;
                }
                else
                {
                    touchEffect.SetActive(false);
                    player.GetComponent<Animator>().SetInteger("anim", 0);
                }

            }

        }

    }

}
