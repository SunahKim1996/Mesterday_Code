using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomMoving : MonoBehaviour
{
    //Vector3 ballPosition;
    //int randomInt;

    public bool isBallMoving = false;

    Animator m_animator;

    private float curTime = 0;
    private float targetTime;

    void Start()
    {
        targetTime = Random.Range(10, 25) / 10;
        //ballPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
        //StartCoroutine(BallMoveLogic());
        //StartCoroutine(MovingSet());
    }

    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<PauseButton>().isPauseScreenOn == true)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        curTime += Time.deltaTime;

        if (curTime >= targetTime)
        {
            curTime = 0;
            targetTime = Random.Range(10, 25) / 10;

            isBallMoving = !isBallMoving;
            BallMoveLogic();
        }
    }

    IEnumerator MovingSet()
    {
        while(true)
        {
            //randomInt = Random.Range(0, 2); // 0 : 공 멈춤, 1 : 공 움직임
            //ballPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));

            isBallMoving = !isBallMoving;
            Debug.Log("isBallMoving = " + isBallMoving);

            BallMoveLogic();

            float randomTime = Random.Range(10, 25) / 10;
            yield return new WaitForSeconds(randomTime);
        }        
    }

    private void OnCollisionEnter(Collision other)
    {     
        if (other.transform.tag == "wall")
        {
            Debug.Log("Tag Wall-=---------------");
            //ballPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
            GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1, 2), 0f, Random.Range(-1, 2));
            Debug.Log("부딪힘!");
        }
    }

    void BallMoveLogic()
    {
        if (GameObject.Find("GameManager").GetComponent<PauseButton>().isPauseScreenOn == false)
        {
            //공 움직임
            if (isBallMoving)
            {
                //transform.position = Vector3.MoveTowards(transform.position, ballPosition, Time.deltaTime * 3f);
                //isBallMoving = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1, 2), 0f, Random.Range(-1, 2));
                
            }

            //공 멈춤
            else if (!isBallMoving)
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                //isBallMoving = false;
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}