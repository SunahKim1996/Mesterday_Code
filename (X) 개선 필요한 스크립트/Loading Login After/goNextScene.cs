using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goNextScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GoNextSceneNow());
    }

    //HACK: Start 에서 빨리 호출하면 Scene 전환이 안되어서 2초 후 호출 처리 
    IEnumerator GoNextSceneNow()
    {
        yield return new WaitForSeconds(2f);

        if (AnonymousLogin.isFirstLogin)
        {
            Debug.Log("1111111111");
            SceneManager.LoadScene(1); //튜토리얼
        }
        else if (UserData.instance.userData.nickName == "")
        {
            Debug.Log("2222222222");
            SceneManager.LoadScene(1); //튜토리얼
        }
        else
        {
            Debug.Log("3333333333");
            SceneManager.LoadScene(2); //메인화면
        }
    }
}
