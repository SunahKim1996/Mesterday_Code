using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour
{
    public GameObject target;
    
    void Update()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;

        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + 0.5f);
    }
}
