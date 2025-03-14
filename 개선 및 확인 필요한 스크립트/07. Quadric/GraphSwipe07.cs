using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphSwipe07 : MonoBehaviour
{
    public const float SwipeDis = 200;  //스와이프 허용 범위
    private Vector2 StartPos;
    
    public List<GameObject> Axis = new List<GameObject>();
    public Text x_intercept1;
    public Text x_intercept2;

    public AudioSource soundEffect;
    public AudioClip swipeEffect;

    private void Start()
    {
        x_intercept1.text = "1";
        x_intercept2.text = "1";
    }

    private void Update()
    {
        if(GetComponent<PauseButton>().isPauseScreenOn == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPos = Input.mousePosition;
                Debug.Log(StartPos);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 p = (Vector2)Input.mousePosition - StartPos;

                if (Mathf.Abs(p.x) < SwipeDis && Mathf.Abs(p.y) < SwipeDis)
                    GetClick();
                else
                {

                    if (Mathf.Abs(p.x) > Mathf.Abs(p.y))
                    {
                        GetSwipe(StartPos, new Vector2(Mathf.Sign(p.x), 0));  //가로 스와이프
                    }                        
                    else if (Mathf.Abs(p.x) < Mathf.Abs(p.y))
                    {
                        GetSwipe(StartPos, new Vector2(0, Mathf.Sign(p.y))); //세로 스와이프
                    }                        
                }
            }
        }
        
    }

    public virtual void GetClick()
    {
        //Debug.Log(StartPos.x);
        //Debug.Log(StartPos.y);
    }

    public virtual void GetSwipe(Vector2 StartPos, Vector2 dir)
    {
        for (int i = 0; i < Axis.Count; i++)
        {
            if (dir.y < 0) // 스와이프 방향 ↓
            {
                if (Axis[i].transform.localScale == new Vector3(1f, -1f, 1f))
                {
                    Axis[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    x_intercept1.text = "-1";
                    x_intercept2.text = "-1";
                    Debug.Log("위 -> 아래");
                                        
                    soundEffect.PlayOneShot(swipeEffect);
                    soundEffect.volume = 0.1f;

                    GetComponent<GraphMoving07>().isSwipeAnswer = true;
                }
            }
            else // 스와이프 방향 ↑
            {
                if (Axis[i].transform.localScale == new Vector3(1f, 1f, 1f))
                {
                    Axis[i].transform.localScale = new Vector3(1f, -1f, 1f);
                    x_intercept1.text = "1";
                    x_intercept2.text = "1";
                    Debug.Log("아래 -> 위");

                    soundEffect.PlayOneShot(swipeEffect);
                    soundEffect.volume = 0.1f;

                    GetComponent<GraphMoving07>().isSwipeAnswer = false;
                }
            }
        }        
    }
}
