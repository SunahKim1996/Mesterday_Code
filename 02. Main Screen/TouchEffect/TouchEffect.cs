using UnityEngine;
using UnityEngine.UI;

public class TouchEffect : MonoBehaviour
{    
    Vector2 direction;
    float moveSpeed = 0.1f;
    float minSize = 0.1f;
    float maxSize = 0.2f;
    float sizeSpeed = 0.5f;

    Image image;
    [SerializeField] Color[] colors;
    float colorSpeed = 5;

    // Start is called before the first frame update
    void Awake()
    {        
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        image = GetComponent<Image>();        
    }

    void OnEnable()
    {
        Color newColor = colors[Random.Range(0, colors.Length)];
        image.color = newColor;

        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);

        
        Color color = image.color;
        color.a = Mathf.Lerp(image.color.a, 0, Time.deltaTime * colorSpeed);
        image.color = color;
        

        if (image.color.a <= 0.01f)
            ObjectPoolManager.instance.Hide(PoolKey.TouchEffect, gameObject);
    }
}
