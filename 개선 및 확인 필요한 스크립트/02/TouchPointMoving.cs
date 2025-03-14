using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPointMoving : MonoBehaviour, IPointerDownHandler
{
    public GameObject player;
    float targetDis;
    public float moveSpeed;

    public Camera cam;
    public GameObject touchEffect;

    RaycastHit hit;

    public static bool isTouchPointOnFloor = false;
    public static bool isTalking = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isTalking == false)
        {
            if(PlayerTrigger.isDoorOpened == 0)
            {
                Ray ray = cam.ScreenPointToRay(eventData.position);
                Physics.Raycast(ray, out hit);

                touchEffect.transform.position = hit.point;

                StopCoroutine("PlayerMove");
                isTouchPointOnFloor = false;

                StartCoroutine("PlayerMove");

            }
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

            if(PlayerTrigger.isDoorOpened == 0)
            {
                if (isTalking == false)
                {
                    if (isTouchPointOnFloor == true)
                    {
                        player.GetComponent<Animator>().SetInteger("anim", 1);
                        targetDis = (hit.point - player.transform.position).magnitude;

                        player.transform.LookAt(hit.point);                        
                        player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                        if (GameObject.Find("GameManager").GetComponent<PauseButton>().isPauseScreenOn == true)
                        {
                            moveSpeed = 0;
                            player.GetComponent<Animator>().enabled = false;
                        }
                        else
                        {
                            moveSpeed = 2;
                            player.GetComponent<Animator>().enabled = true;
                        }

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
            else if(PlayerTrigger.isDoorOpened == 1) //문 열림
            {
                touchEffect.SetActive(false);
                player.transform.position = player.transform.position;
                
                player.GetComponent<Animator>().SetInteger("anim", 3);

                break;
            }
            else if(PlayerTrigger.isDoorOpened == 2) //문 잠김
            {
                touchEffect.SetActive(false);
                player.transform.position = player.transform.position;
                
                player.GetComponent<Animator>().SetInteger("anim", 0);

                break;
            }

        }

    }

}
