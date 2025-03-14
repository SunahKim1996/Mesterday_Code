using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    private Transform tr;
    public GameObject titleCam;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        tr.position = new Vector3(target.position.x - 1.1f, tr.position.y, target.position.z - 1.7f);
        titleCam.SetActive(false);
    }
}
