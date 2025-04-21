using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchEffect_star : MonoBehaviour
{
    Image img;
    Vector2 direction;
    public float moveSpeed = 0.1f;
    public float minSize = 0.1f;
    public float maxSize = 0.3f;
    public float sizeSpeed = 1;
    public Color[] colors;
    public float colorSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float size = Random.Range(minSize, maxSize);
        GetComponent<RectTransform>().localScale = new Vector2(size, size);
        img.color = colors[Random.Range(0, colors.Length)];

       
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().Translate(direction * moveSpeed);
        GetComponent<RectTransform>().localScale = Vector2.Lerp(GetComponent<RectTransform>().localScale, Vector2.zero, Time.deltaTime * sizeSpeed);

        Color color = img.color;
        color.a = Mathf.Lerp(img.color.a, 0, Time.deltaTime * colorSpeed);
        img.color = color;

        if(img.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
