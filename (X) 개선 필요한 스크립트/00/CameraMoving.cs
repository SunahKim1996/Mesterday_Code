using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float speed;
    public GameObject backGround;
    public GameObject backGroundClone;

    // Update is called once per frame

    void Start()
    {
        StartCoroutine(BackgroundMoving());    
    }

    void Update()
    {       
        float x = backGround.transform.position.x - speed * Time.deltaTime;
        backGround.transform.position = new Vector3(x, backGround.transform.position.y, 0);

        float x2 = backGroundClone.transform.position.x - speed * Time.deltaTime;
        backGroundClone.transform.position = new Vector3(x2, backGround.transform.position.y, 0);
             
    }

    IEnumerator BackgroundMoving()
    {
        while(true)
        {
            yield return new WaitForSeconds(55f);
            backGround.transform.position = new Vector3(35f, 0f, 0f);


            yield return new WaitForSeconds(55f);
            backGroundClone.transform.position = new Vector3(35f, 0f, 0f);            
        }
    }
}
