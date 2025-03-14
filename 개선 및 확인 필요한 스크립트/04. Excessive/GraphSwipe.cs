using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSwipe : MonoBehaviour
{
    public const float SwipeDis = 200;  //스와이프 허용 범위
    private Vector2 StartPos;

    public Transform Axis;

    public GameObject minusSign_inner;
    public GameObject minusSign_outer;

    public AudioSource soundEffect;
    public AudioClip swipeEffect;

    float swipeTimer = 0;
    float swipeCoolTime = 0.5f;

    private void Update()
    {
        if (GetComponent<PauseButton>().isPauseScreenOn)
            return;

        if (swipeTimer > 0)
            swipeTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
            Debug.Log(StartPos);
        }

        if (Input.GetMouseButton(0) && swipeTimer <= 0)
        {
            Vector2 p = (Vector2)Input.mousePosition - StartPos;

            if (Mathf.Abs(p.x) >= SwipeDis || Mathf.Abs(p.y) >= SwipeDis)
            {
                // 가로 스와이프
                if (Mathf.Abs(p.x) > Mathf.Abs(p.y))
                    GetSwipe(StartPos, new Vector2(Mathf.Sign(p.x), 0));

                // 세로 스와이프
                else if (Mathf.Abs(p.x) < Mathf.Abs(p.y))
                    GetSwipe(StartPos, new Vector2(0, Mathf.Sign(p.y))); 
            }
        }
    }

    void RefreshGraph(int quadrantIndex)
    {
        if (quadrantIndex == 1)
        {
            Axis.localScale = new Vector3(1f, 1f, 1f);
            minusSign_inner.SetActive(false);
            minusSign_outer.SetActive(false);
        }
        else if (quadrantIndex == 2)
        {
            Axis.localScale = new Vector3(-1f, 1f, 1f);
            minusSign_inner.SetActive(true);
            minusSign_outer.SetActive(false);
        }
        else if (quadrantIndex == 3)
        {
            Axis.localScale = new Vector3(-1f, -1f, 1f);
            minusSign_inner.SetActive(true);
            minusSign_outer.SetActive(true);
        }
        else if (quadrantIndex == 4)
        {
            Axis.localScale = new Vector3(1f, -1f, 1f);
            minusSign_inner.SetActive(false);
            minusSign_outer.SetActive(true);
        }

        
    }

    public void GetSwipe(Vector2 StartPos, Vector2 dir)
    {
        swipeTimer = swipeCoolTime;

        float screenCenterX = Screen.width * 0.5f;
        float screenCenterY = Screen.height * 0.5f;

        // 가로 스와이프 
        if (dir.y == 0) 
        {
            if (dir.x < 0) // 스와이프 방향  <-
            {
                if (StartPos.y > screenCenterY)
                {
                    RefreshGraph(2);
                    Debug.Log("1 사분면 -> 2 사분면");
                }
                else if(StartPos.y < screenCenterY)
                {
                    RefreshGraph(3);
                    Debug.Log("4 사분면 -> 3 사분면");
                }
            }
            else // 스와이프 방향 ->
            {
                if (StartPos.y > screenCenterY)
                {
                    RefreshGraph(1);
                    Debug.Log("2 사분면 -> 1 사분면");
                }
                else if (StartPos.y < screenCenterY)
                {
                    RefreshGraph(4);
                    Debug.Log("3 사분면 -> 4 사분면");
                }
            }
        }

        // 세로 스와이프
        else if(dir.x == 0) 
        {
            if (dir.y < 0) // 스와이프 방향 ↓
            {
                if (StartPos.x > screenCenterX) 
                {
                    RefreshGraph(4);
                    Debug.Log("1 사분면 -> 4 사분면");
                }
                else if (StartPos.x < screenCenterX) 
                {
                    RefreshGraph(3);
                    Debug.Log("2 사분면 -> 3 사분면");
                }
            }
            else // 스와이프 방향 ↑
            {
                if (StartPos.x > screenCenterX)
                {
                    RefreshGraph(1);
                    Debug.Log("4 사분면 -> 1 사분면");
                }
                else if (StartPos.x < screenCenterX)
                {
                    RefreshGraph(2);
                    Debug.Log("3 사분면 -> 2 사분면");
                }
            }
        }

        soundEffect.PlayOneShot(swipeEffect);
        soundEffect.volume = SoundManager.swipeEffect;
    }
}
