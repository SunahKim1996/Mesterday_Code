using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    //public List<GameObject> playerClone = new List<GameObject>();

    public GameObject playerClone_1;
    public GameObject playerClone_2;
    public GameObject playerClone_3;
    public GameObject playerClone_4;
    public GameObject playerClone_5;
    public GameObject playerClone_6;
    public GameObject playerClone_7;

    private UserDataInfo userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;
        //Debug.Log(PlayerPrefs.GetInt("PlayerWhere"));

        if (userData.playerPos == 0)
        {
            transform.position = new Vector3(2.3f, 9.026f, -22f);
            Debug.Log("playerPos = " + userData.playerPos);
        }
        else if (userData.playerPos == 1)
        {
            transform.position = playerClone_1.transform.position;
            Debug.Log("playerPos = " + userData.playerPos);
        }
        else if (userData.playerPos == 2)
        {
            transform.position = playerClone_2.transform.position;
        }
        else if (userData.playerPos == 3)
        {
            transform.position = playerClone_3.transform.position;
        }
        else if (userData.playerPos == 4)
        {
            transform.position = playerClone_4.transform.position;
        }
        else if (userData.playerPos == 5)
        {
            transform.position = playerClone_5.transform.position;
        }
        else if (userData.playerPos == 6)
        {
            transform.position = playerClone_6.transform.position;
        }
        else if (userData.playerPos == 7)
        {
            transform.position = playerClone_7.transform.position;
        }
    }
}
