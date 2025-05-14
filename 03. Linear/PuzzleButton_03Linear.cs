using UnityEngine;
using UnityEngine.UI;

public class PuzzleButton_03Linear : PuzzleButton
{
    [SerializeField] Text fomulaTxt_Y;   
    [SerializeField] Transform graph;

    int startNum_Y = 1;
    
    /*
    IEnumerator SpeechBubbleOn()
    {
        speechBubble.SetActive(true);

        int randomInt = Random.Range(0, 2);

        //if (dragPoint.GetComponent<GraphDrag02_2>().fomulaTxt_x.text != "1")
        {
            if(randomInt == 0)
            {
                bubbleTxt.text = "다시 해보자";
            }
            else
            {
                bubbleTxt.text = "기울기가 안맞아";
            }
        }
        else
        {
            bubbleTxt.text = "다시 해보자";
        }

        yield return new WaitForSeconds(1.5f);

        speechBubble.SetActive(false);
    }
    */

    protected override void PlusButton()
    {
        if (startNum_Y >= 3)
            return;

        startNum_Y++;
        RefreshGraph();
    }

    protected override void MinusButton()
    {
        if (startNum_Y <= -1)
            return;

        startNum_Y--;
        RefreshGraph();
    }

    void RefreshGraph()
    {
        string yText = (startNum_Y == -1) ? $"{startNum_Y}" : $"+{startNum_Y}";
        fomulaTxt_Y.text = yText;

        float z = 2.2f + startNum_Y * 5.4f;
        graph.position = new Vector3(graph.position.x, graph.position.y, z);
    }
}
