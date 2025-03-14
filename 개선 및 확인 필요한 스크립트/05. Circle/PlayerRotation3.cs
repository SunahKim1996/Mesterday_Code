using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotation3 : MonoBehaviour
{
    public GameObject circle;
    public float speed;

    public bool isCircleMoving = false;
    public bool isCircleBigger = false;

    private void Start()
    {
        
    }

    public int scale;

    private void Update()
    {
        //transform.GetChild(0).gameObject.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);

        if (isCircleMoving == true)
        {
            //transform.GetChild(0).gameObject.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);

            Vector3 Axis = new Vector3(0, 0, 1);
            this.GetComponent<RectTransform>().Rotate(Axis, speed * Time.fixedDeltaTime);

            this.GetComponent<Transform>().localScale = new Vector3
                    (circle.GetComponent<Transform>().localScale.x, circle.GetComponent<Transform>().localScale.y, circle.GetComponent<Transform>().localScale.z);

            transform.GetChild(0).gameObject.GetComponent<Transform>().localScale = new Vector3
                (transform.GetChild(0).gameObject.GetComponent<Transform>().localScale.x + 0.01f, transform.GetChild(0).gameObject.GetComponent<Transform>().localScale.y - 0.01f, circle.GetComponent<Transform>().localScale.z);
        }        
    }
}