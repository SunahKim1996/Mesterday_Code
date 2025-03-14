using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePos : MonoBehaviour
{
    public GameObject realHandlePos;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = realHandlePos.transform.position;
    }
}
