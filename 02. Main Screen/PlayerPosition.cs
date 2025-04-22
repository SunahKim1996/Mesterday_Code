using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Transform> playerTransList;
    private UserDataInfo userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;

        Debug.Log("playerPos = " + userData.playerPos);
        player.transform.position = 
            (userData.playerPos == 0) ? new Vector3(2.3f, 9.026f, -22f) : playerTransList[userData.playerPos - 1].position;
    }
}
