using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphMovingButton : MonoBehaviour
{
    int StartNum = 3;

    public GameObject player;
    public GameObject Axis;

    Transform player_tr;
    Transform Axis_tr;

    public Text fomulaTxt;

    public List<GameObject> graph = new List<GameObject>();
    
    Vector3 startPos;
    Vector3 currentPos;

    public AudioSource soundEffect;
    public AudioClip plusEffect;

    private void Start()
    {
        player_tr = player.GetComponent<Transform>();
        Axis_tr = Axis.GetComponent<Transform>();

        startPos = player_tr.position;
        currentPos = player_tr.position;

        //player_tr.GetComponent<Animator>().SetInteger("Exc", 8);

        //fomulaTxt = GetComponent<Text>();
    }

    private void Update()
    {
        if(GetComponent<PauseButton>().isPauseScreenOn == true)
        {
            player_tr.GetComponent<Animator>().enabled = false;
        }
        else
        {
            player_tr.GetComponent<Animator>().enabled = true;
        }
    }

    public void PlusButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (StartNum < 5)
        {
            StartNum += 1;
            fomulaTxt.text = StartNum.ToString();


            if (StartNum == 2)
            {
                graph[0].SetActive(false);
                graph[1].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 0);

                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -8.89f);
            }

            else if (StartNum == 3)
            {
                graph[1].SetActive(false);
                graph[2].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 1);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -7.79f);
            }

            else if (StartNum == 4)
            {
                graph[2].SetActive(false);
                graph[3].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 2);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -7.09f);
            }

            else if (StartNum == 5)
            {
                graph[3].SetActive(false);
                graph[4].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 3);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -5.81f);
            }
        }


    }

    public void MinusButton()
    {
        soundEffect.PlayOneShot(plusEffect);
        soundEffect.volume = SoundManager.plusEffect;

        if (StartNum > 1)
        {
            StartNum -= 1;
            fomulaTxt.text = StartNum.ToString();

            if (StartNum == 1)
            {
                graph[1].SetActive(false);
                graph[0].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 7);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -9.57f);
            }

            else if (StartNum == 2)
            {
                graph[2].SetActive(false);
                graph[1].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 6);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -8.89f);
            }

            if (StartNum == 3)
            {
                graph[3].SetActive(false);
                graph[2].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 5);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -7.79f);
            }

            else if (StartNum == 4)
            {
                graph[4].SetActive(false);
                graph[3].SetActive(true);

                player_tr.GetComponent<Animator>().SetInteger("Exc", 4);
                //player_tr.position = new Vector3(player_tr.position.x, player_tr.position.y, -7.09f);
            }
        }
    }
}



