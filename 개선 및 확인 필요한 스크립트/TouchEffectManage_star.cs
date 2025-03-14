using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectManage_star : MonoBehaviour
{
    public Camera firstCam;
    public Camera secondCam;

    public static bool isSecondCamOn = false;

    public GameObject starPrefab;
    float spawnsTime;
    public float defaultTime = 0.05f;

    Vector2 point;
    
    public RectTransform panel;
    public Canvas effectCanvas;

    private void Start()
    {
        panel = panel.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && spawnsTime >= defaultTime)
        {
            if(isSecondCamOn == false)
            {
                effectCanvas.worldCamera = firstCam;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, firstCam, out point);


                GameObject star = Instantiate(starPrefab, point, Quaternion.identity, effectCanvas.transform);
                star.GetComponent<RectTransform>().localPosition = point;


                spawnsTime = 0;
            }
            else
            {
                effectCanvas.worldCamera = secondCam;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, secondCam, out point);


                GameObject star = Instantiate(starPrefab, point, Quaternion.identity, effectCanvas.transform);
                star.GetComponent<RectTransform>().localPosition = point;


                spawnsTime = 0;
            }            
        }
        spawnsTime += Time.deltaTime;
    }
}
