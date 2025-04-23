using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    PlayerTrigger playerTrigger;
    Animator playerAnimator;
    [SerializeField] Camera playerCamera;

    float targetDis;
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject touchEffect;
    RaycastHit hit;
    Coroutine moveCor = null;

    [SerializeField] DialogManager dialogManager;

    void Start()
    {
        playerTrigger = GetComponent<PlayerTrigger>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!playerCamera.gameObject.activeSelf)
                return;

            if (dialogManager.isTalking) //HERE: 여기 체크가 안되는 듯함 확인 필요
            {
                Debug.Log($"Return0 {dialogManager.isTalking}");
                return;
            }

            if (playerTrigger.playerDoorState != PlayerDoorState.None)
            {
                Debug.Log($"Return1 {playerTrigger.playerDoorState}");
                return;
            }

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider == null)
                return;

            bool isActive = (hit.collider.tag == "floor") ? true : false;
            ToggleTouchEffect(isActive);

            if (!isActive)
                return;

            transform.LookAt(hit.point);
            playerAnimator.SetInteger("anim", 1);

            if (moveCor == null)
                moveCor = StartCoroutine(CheckMovePlayer());
        }
    }
    
    void ToggleTouchEffect(bool state)
    {
        if (state)
        {
            Vector3 effectPos = touchEffect.transform.position;
            Vector3 targetPos = new Vector3(hit.point.x, effectPos.y, hit.point.z);

            touchEffect.transform.position = targetPos;
        }

        touchEffect.SetActive(state);
    }

    void MovePlayer()
    {
        Debug.Log("MovePlayer====================");
        Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        targetDis = (targetPos - transform.position).magnitude;

        if (targetDis <= 0.1f)
        {
            ToggleTouchEffect(false);
            playerAnimator.SetInteger("anim", 0);

            StopCoroutine("CheckMovePlayer");
        }
    }

    IEnumerator CheckMovePlayer()
    {
        while (true)
        {
            yield return null;
            MovePlayer();

            if (dialogManager.isTalking)
                yield break;

            switch (playerTrigger.playerDoorState)
            {
                case PlayerDoorState.Opening:
                    HandlePlayerDoorState(3);
                    yield break;

                case PlayerDoorState.Locked:
                    HandlePlayerDoorState(0);
                    yield break;
            }
        }
    }

    void HandlePlayerDoorState(int animValue)
    {
        ToggleTouchEffect(false);
        playerAnimator.SetInteger("anim", animValue);
    }
}
