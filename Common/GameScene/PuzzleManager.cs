using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{   
    // PuzzleManager 사용 시, Scene 에 맞게 조정되어야 하는 것
    // 1. PuzzleSetting Inspector 에 오브젝트
    // 2. Puzzle Manager 하위에 CamOriginTrans 와 CamPuzzleTrans 위치 및 회전값

    [SerializeField] Transform camOriginTrans;
    [SerializeField] Transform camPuzzleTrans;

    [SerializeField] Image fadeOutImg;
    [SerializeField] GameObject commonPuzzleUI;
    float camDistance = 999f;

    PuzzleSetting puzzleSetting;

    // Start is called before the first frame update
    void Start()
    {
        puzzleSetting = GetComponent<PuzzleSetting>();

        fadeOutImg.gameObject.SetActive(false);
        TogglePuzzleObj(false);

        Vector3 graphPos = puzzleSetting.graph.position;
        puzzleSetting.graph.position = new Vector3(graphPos.x, -2.37f, graphPos.z);
    }

    /// <summary>
    /// 퍼즐 게임 시작
    /// </summary>
    public void StartGame()
    {
        SoundManager.instance.PlaySFX(SoundClip.cameraSFX);

        Action endCallback = () =>
        {
            SoundManager.instance.PlayBGM(SoundClip.GameBGM);
            //TODO: 타이머 시작
        };

        StartCoroutine(StartDirect(true, camPuzzleTrans, endCallback));
    }

    public void EndGame()
    {
        Action endCallback = () =>
        {
            SoundManager.instance.PlaySFX(SoundClip.clearSFX);
            //TODO: 결과 UI & 결과 대사 
        };

        StartCoroutine(StartDirect(false, camOriginTrans));
    }
        

    IEnumerator StartDirect(bool isStart, Transform targetCam, Action endCallback = null)
    {
        Vector3 targetCamPos = targetCam.position;
        Quaternion targetCamRot = targetCam.rotation;

        // 카메라 연출 
        while (camDistance >= 4f)
        {
            puzzleSetting.mainCam.position = Vector3.Lerp(puzzleSetting.mainCam.position, targetCamPos, Time.deltaTime);
            puzzleSetting.mainCam.rotation = Quaternion.Slerp(puzzleSetting.mainCam.rotation, targetCamRot, Time.deltaTime);

            camDistance = (targetCam.transform.position - puzzleSetting.mainCam.position).magnitude;

            yield return null;
        }

        // 페이드 아웃
        fadeOutImg.gameObject.SetActive(true);

        for (int i = 0; i <= 10; i++)
        {
            ChangeImageAlpha(i);
            yield return new WaitForSeconds(0.01f);
        }

        // 오브젝트 처리 
        TogglePuzzleObj(isStart);
        //mainCam.gameObject.SetActive(!isStart);

        puzzleSetting.mainCam.position = targetCamPos;
        puzzleSetting.mainCam.rotation = targetCamRot;
        camDistance = 999f;

        float y = (isStart) ? 15f : -2.37f;
        Vector3 graphPos = puzzleSetting.graph.position;
        puzzleSetting.graph.position = new Vector3(graphPos.x, y, graphPos.z); //TODO

        yield return new WaitForSeconds(0.5f);

        // 페이드 인 
        for (int i = 10; i > 0; i--)
        {
            ChangeImageAlpha(i);
            yield return new WaitForSeconds(0.01f);
        }

        fadeOutImg.gameObject.SetActive(false);

        if (endCallback != null)
            endCallback();
    }

    /// <summary>
    /// 퍼즐용 & 일반 오브젝트 On Off
    /// </summary>
    void TogglePuzzleObj(bool isPuzzleTime)
    {
        for (int i = 0; i < puzzleSetting.normalObjs.Length; i++)
            puzzleSetting.normalObjs[i].SetActive(!isPuzzleTime);

        for (int i = 0; i < puzzleSetting.puzzleObjs.Length; i++)
            puzzleSetting.puzzleObjs[i].SetActive(isPuzzleTime);

        puzzleSetting.mainCam.gameObject.SetActive(!isPuzzleTime);
        commonPuzzleUI.SetActive(isPuzzleTime);
    }

    void ChangeImageAlpha(int index)
    {
        Color color = fadeOutImg.color;

        float f = index / 10.0f;
        color.a = f;
        fadeOutImg.color = color;
    }
}
