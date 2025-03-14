using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static int nextScene;

    public Text hint;

    [SerializeField]
    Image progressBar = null;

    public GameObject player;

    private void Start()
    {
        int randomInt = Random.Range(0, 2);

        if(randomInt == 0)
        {
            hint.text = "그래프를 움직이고 싶다면 [드래그]나 [스와이프]를 해보세요";
        }
        else
        {
            hint.text = "로딩중이에요. . .";
        }

        StartCoroutine(LoadScene());
    }

    public static void LoadScene(int sceneIndex)
    {
        nextScene = sceneIndex;
        SceneManager.LoadScene("Loading Scene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;            

            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                /*
                Vector3 player_pos = player.GetComponent<RectTransform>().position;
                player_pos = new Vector3(player_pos.x + 17.571f * progressBar.fillAmount, player_pos.y, player_pos.z);
                Debug.Log(player_pos.x);
                */

                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
