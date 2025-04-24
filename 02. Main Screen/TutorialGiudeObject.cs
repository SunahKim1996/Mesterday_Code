using UnityEngine;

public class TutorialGiudeObject : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * 180 * Time.deltaTime, 0f, Space.World); 
    }
}
