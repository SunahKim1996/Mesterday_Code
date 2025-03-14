using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentMovement : MonoBehaviour
{
    Vector3 studentPosition;

    public GameObject player;

    void Start()
    {
        studentPosition = new Vector3(Random.Range(-16f, 13f), transform.position.y, Random.Range(-6f, -23f));
        StartCoroutine(StartWait());
    }

    IEnumerator StartWait()
    {
        while (true)
        {
            yield return null;

            if (GameObject.Find("GameManager").GetComponent<CameraMoving04>().isPuzzleGameStart == true)
            {
                StartCoroutine(StudentMoveLogic());
                break;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "wall")
        {
            studentPosition = new Vector3(player.transform.position.x + Random.Range(-3f, 3f), transform.position.y, player.transform.position.z + Random.Range(-3f, 3f));
            
        }
    }

    IEnumerator StudentMoveLogic()
    { 
        while(true)
        {
            yield return null;

            if(GameObject.Find("GameManager").GetComponent<PauseButton>().isPauseScreenOn == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, studentPosition, Time.deltaTime * 3f);
                //this.GetComponent<Animator>().SetInteger("ani", 1);


                if (Vector3.Distance(studentPosition, transform.position) <= 0.2f)
                {
                    studentPosition = new Vector3(Random.Range(-16f, 13f), transform.position.y, Random.Range(-6f, -23f));
                }

                if (GameObject.Find("GameManager").GetComponent<CameraMoving04>().isPuzzleGameStart == false)
                {
                    break;
                }
            }           

        }
    }
}