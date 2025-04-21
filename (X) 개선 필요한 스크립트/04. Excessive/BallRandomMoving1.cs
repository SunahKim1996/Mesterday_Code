using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomMoving1 : MonoBehaviour
{
    Vector3 ballPosition;
    int randomInt;

    public bool isBallMoving = false;


    Animator m_animator;

    void Start()
    {
        ballPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
        StartCoroutine(BallMoveLogic());
        StartCoroutine(MovingSet());

        m_animator = GetComponent<Animator>();
    }

    IEnumerator MovingSet()
    {
        while(true)
        {
            randomInt = Random.Range(0, 2); // 0 : 공 멈춤, 1 : 공 움직임
            ballPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
                        
            if(randomInt == 0)
            {
                isBallMoving = false;

                yield return new WaitForSeconds(3f);
            }
            else
            {
                isBallMoving = true;

                yield return new WaitForSeconds(1f);

                m_animator.SetTrigger("Moving");
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "wall")
        {
            ballPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
            Debug.Log("부딪힘!");
        }
    }

    IEnumerator BallMoveLogic()
    {
        while (true)
        {
            yield return null;

            if (GameObject.Find("GameManager").GetComponent<PauseButton>().isPauseScreenOn == false)
            {
                if (randomInt == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, ballPosition, Time.deltaTime * 3f);

                    
                    if (Vector3.Distance(ballPosition, transform.position) >= 0.1f)
                    {
                        transform.LookAt(new Vector3(ballPosition.x, 0f, ballPosition.z));
                    }
                    

                    if (randomInt == 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    }
                }

                else if (randomInt == 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }
            }            
        }
    }
}