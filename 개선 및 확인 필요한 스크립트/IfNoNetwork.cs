using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfNoNetwork : MonoBehaviour
{
    public GameObject networkPopup;
    bool isCoolTime = false;

    // Update is called once per frame
    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if(isCoolTime == false)
            {
                networkPopup.SetActive(true);
            }            
        }
        else
        {
            networkPopup.SetActive(false);
        }
    }

    public void ConfirmButton()
    {
        networkPopup.SetActive(false);

        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        isCoolTime = true;

        yield return new WaitForSeconds(1.5f);

        isCoolTime = false;

    }

}
