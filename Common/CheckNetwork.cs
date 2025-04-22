using System.Collections;
using UnityEngine;

public class CheckNetwork : MonoBehaviour
{
    CommonUI commonUI;
    commonUIType uiType;

    bool isCoolTime = false;

    void Start()
    {
        commonUI = GetComponent<CommonUI>();
        uiType = commonUIType.Network;
    }

    // Update is called once per frame
    void Update()
    {
        if (commonUI == null)
            return;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if(isCoolTime == false)
                commonUI.ToggleUI(uiType, true);
        }
        else
            commonUI.ToggleUI(uiType, false);
    }

    public void OnConfirmButton()
    {
        commonUI.ToggleUI(uiType, false);
        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        isCoolTime = true;
        yield return new WaitForSeconds(1.5f);
        isCoolTime = false;
    }
}
