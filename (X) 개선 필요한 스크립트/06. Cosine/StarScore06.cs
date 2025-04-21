using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarScore06 : MonoBehaviour
{
    public List<GameObject> c_star = new List<GameObject>();
    //public List<GameObject> nc_star = new List<GameObject>();

    public GameObject starScore;

    public int howMuchStarScore = 0;

    public bool isStarScoreOnTime = false;

    public Text scoreTxt;

    public AudioSource soundEffect;
    public AudioClip clearEffect;

    public GameObject friend;

    // Update is called once per frame
    void Start()
    {
        starScore.SetActive(false);
        StartCoroutine(StarScoreOn());
    }

    IEnumerator StarScoreOn()
    {
        while (true)
        {
            yield return null;

            if (isStarScoreOnTime == true)
            {
                if (GetComponent<Timer06>()._sec >= 35)
                {
                    howMuchStarScore = 3;

                    friend.GetComponent<Animator>().SetInteger("friend", 1);

                    scoreTxt.text = "데카르트도 울고 갈 실력 !";

                    c_star[0].SetActive(true);
                    c_star[1].SetActive(true);
                    c_star[2].SetActive(true);
                    //nc_star[0].SetActive(false);
                    //nc_star[1].SetActive(false);
                    //nc_star[2].SetActive(false);
                }
                else if (GetComponent<Timer06>()._sec >= 20)
                {
                    howMuchStarScore = 2;

                    friend.GetComponent<Animator>().SetInteger("friend", 1);

                    scoreTxt.text = "수학자도 울고 갈 실력 !";

                    c_star[0].SetActive(true);
                    c_star[1].SetActive(true);
                    c_star[2].SetActive(false);
                    //nc_star[0].SetActive(false);
                    //nc_star[1].SetActive(false);
                    //nc_star[2].SetActive(true);
                }
                else if (GetComponent<Timer06>()._sec >= 1)
                {
                    howMuchStarScore = 1;

                    friend.GetComponent<Animator>().SetInteger("friend", 1);

                    scoreTxt.text = "선생님도 울고 갈 실력 !";

                    c_star[0].SetActive(true);
                    c_star[1].SetActive(false);
                    c_star[2].SetActive(false);
                    //nc_star[0].SetActive(false);
                    //nc_star[1].SetActive(true);
                    //nc_star[2].SetActive(true);
                }
                else
                {
                    howMuchStarScore = 0;

                    friend.GetComponent<Animator>().SetInteger("friend", 2);

                    scoreTxt.text = "그냥 울고 갈 실력 !";

                    c_star[0].SetActive(false);
                    c_star[1].SetActive(false);
                    c_star[2].SetActive(false);
                    //nc_star[0].SetActive(true);
                    //nc_star[1].SetActive(true);
                    //nc_star[2].SetActive(true);
                }

                yield return new WaitForSeconds(1f);

                soundEffect.PlayOneShot(clearEffect);
                soundEffect.volume = SoundManager.clearEffect;

                starScore.SetActive(true);
                yield return new WaitForSeconds(4f);
                starScore.SetActive(false);

                break;
            }            
        }
    }
}
