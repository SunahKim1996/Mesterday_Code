using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    UserDataInfo userData;

    [Header("UI")]
    [SerializeField] GameObject diaryButton;
    [SerializeField] GameObject noteButton;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundClip.MainBGM);

        userData = UserData.instance.userData;
        ToggleMainHUD(userData.playerPos >= 1);
    }

    void ToggleMainHUD(bool state)
    {
        diaryButton.SetActive(state);
        noteButton.SetActive(state);
    }
}
