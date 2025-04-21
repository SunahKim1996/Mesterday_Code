using System.Collections;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Transform bg1;
    [SerializeField] Transform bg2;

    // Update is called once per frame

    void Start()
    {
        //StartCoroutine(BackgroundMoving());    
    }

    void Update()
    {
        MoveBG(bg1);
        MoveBG(bg2);
    }

    void MoveBG(Transform targetBG)
    {
        float x = targetBG.position.x - speed * Time.deltaTime;
        targetBG.position = new Vector3(x, targetBG.position.y, 0);

        if (targetBG.position.x <= -38f)
            targetBG.position = new Vector3(38f, 0f, 0f);
    }


    /*
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
    */
}
