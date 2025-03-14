using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainScene : MonoBehaviour
{
    public bool isReadyToGoToMain = false;

    // Use this for initialization
    void Update()
    {
        if(isReadyToGoToMain == true)
        {
            LoadingManager.LoadScene(2);
        }        
    }    
}
