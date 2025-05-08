using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMove : MonoBehaviour
{
    PlayerDoor playerDoor;
    Animator playerAnimator;
    [SerializeField] Camera playerCamera;

    [SerializeField] float moveSpeed;

    [SerializeField] GameObject touchEffect;
    RaycastHit hit;
    Coroutine moveCor = null;

    [SerializeField] DialogManager dialogManager;


    void Start()
    {
        playerDoor = GetComponent<PlayerDoor>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Diary.isDiaryOn || SummaryNote.isNoteOn || GamePause.isPauseTime)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!playerCamera.gameObject.activeSelf)
                return;

            if (!dialogManager.isChatPause)
                return;

            if (playerDoor.playerDoorState != PlayerDoorState.None)
                return;

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider == null)
                return;

            Debug.Log($"hit.collider.tag {hit.collider.tag}");

            bool isActive = (hit.collider.tag == "floor") ? true : false;
            ToggleTouchEffect(isActive);

            if (!isActive)
                return;

            Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(targetPos, Vector3.up);
            playerAnimator.SetInteger("anim", 1);

            if (moveCor == null)
            {
                Debug.Log("Start Coroutine");
                moveCor = StartCoroutine(CheckMovePlayer());
            }
                
        }
    }
    
    public void ToggleTouchEffect(bool state)
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
        Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        Vector3 dir = (targetPos - transform.position).normalized;
        float targetDis = (targetPos - transform.position).magnitude;

        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        if (targetDis <= 0.5f)
            StopMovePlayer();
    }

    void StopMovePlayer()
    {
        ToggleTouchEffect(false);
        playerAnimator.SetInteger("anim", 0);

        if (moveCor != null)
        {
            StopCoroutine(moveCor);
            moveCor = null;

            Debug.Log("Stop Coroutine");
        }
    }

    IEnumerator CheckMovePlayer()
    {
        while (true)
        {
            yield return null;
            MovePlayer();

            if (hit.collider == null || hit.collider.tag != "floor")
            {
                StopMovePlayer();
                yield break;
            }

            if (dialogManager.isTalking)
            {
                StopMovePlayer();
                yield break;
            }

            switch (playerDoor.playerDoorState)
            {
                case PlayerDoorState.Opening:
                    HandlePlayerDoorState(3);
                    StopMovePlayer();
                    yield break;

                case PlayerDoorState.Locked:
                    HandlePlayerDoorState(0);
                    StopMovePlayer();
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
