using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleHint : MonoBehaviour
{
    public int howMuchUserMove;

    public GameObject speechBubble;
    public Text speechTxt;

    public GameObject handle;

    // Start is called before the first frame update
    void Start()
    {
        handle.SetActive(false);
        howMuchUserMove = 0;
        StartCoroutine(Wait());
    }

    private void Update()
    {
        //Debug.Log("howMuchUserMove = " + howMuchUserMove);
    }

    IEnumerator Wait()
    {
        while (true)
        {
            yield return null;

            if(GetComponent<CameraMoving04>().isPuzzleGameStart == true)
            {
                yield return new WaitForSeconds(10f);

                if (howMuchUserMove == 0)
                {
                    StartCoroutine(Hint_1());
                }

                yield return new WaitForSeconds(10f);

                if (howMuchUserMove == 0)
                {
                    StartCoroutine(Hint_2());
                    handle.SetActive(true);
                    break;
                }

                break;
            }
        }   
    }

    IEnumerator Hint_1()
    {
        speechTxt.text = "원을 줄이거나 늘려보자";
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(3f);
        speechBubble.SetActive(false);

        speechTxt.text = "어딜 잡고 늘려야 할까?";
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(3f);
        speechBubble.SetActive(false);
    }

    IEnumerator Hint_2()
    {
        speechTxt.text = "원의 반지름이 되는 곳에 손을 대고 늘려보자";
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(4f);
        speechBubble.SetActive(false);
    }
}
