using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HereMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * 180 * Time.deltaTime, 0f, Space.World); 
    }
}
