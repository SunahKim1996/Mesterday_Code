using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FomulaTxt : MonoBehaviour
{
    public Text fomulaTxt;
    int randomInt;

    public bool isAnswer = false;

    // Update is called once per frame
    void Update()
    {
        if(isAnswer == false)
        {
            randomInt = Random.Range(11, 100);

            fomulaTxt.text = randomInt.ToString();
        }       
        else
        {
            fomulaTxt.text = " 1";
        }
    }
}
