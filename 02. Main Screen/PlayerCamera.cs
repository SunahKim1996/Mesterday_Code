using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    
    /// <summary>
    /// Player 이동 시 따라오는 카메라 
    /// </summary>
    void LateUpdate()
    {
        Vector3 playerPos = transform.position;

        playerCamera.position 
            = new Vector3(playerPos.x - 1.1f, playerCamera.position.y, playerPos.z - 1.7f);
    }
}
