using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Database;

public class AnonymousLogin : MonoBehaviour
{
    private FirebaseAuth auth;
    public static FirebaseUser user;
    public static bool isFirstLogin;

    public bool isLogInSuccess = false;
    public bool isAlreadyLogin = false;
    public GameObject logInBtn;

    public GameObject LoginTxt_1;
    public GameObject LoginTxt_2;

    public GameObject networkPopup;

    public AudioSource soundEffect;
    public AudioClip normalButtonEffect;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log($"<color=blue>[Firebase Init] Canceled </color>");
            }
            else if (task.IsFaulted)
            {
                Debug.Log($"<color=blue>[Firebase Init] Failed </color>");
            }
            else
            {
                Debug.Log($"<color=blue>[Firebase Init] Successly</color>");

                FirebaseApp app = FirebaseApp.DefaultInstance;

                auth = FirebaseAuth.DefaultInstance;
                auth.StateChanged += AuthStateChanged;

                StartCoroutine(CheckLogIn());
            }
        });
    }

    public async void DeleteAnonymousAccount()
    {
        if (auth.CurrentUser != null)
        {
            try
            {
                await auth.CurrentUser.DeleteAsync();
                Debug.Log($"계정 삭제 성공");
            }
            catch (System.Exception e)
            {
                Debug.LogError("계정 삭제 중 오류 발생: " + e.Message);
            }
        }
    }

    /// <summary>
    /// Auth 상태 체크 (로그아웃, 로그인(전적있음), 로그인(전적없음))
    /// </summary>
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
                Debug.Log("Log out");

            user = auth.CurrentUser;
            if (user != null)
            {
                isFirstLogin = false;
                OnClickAnnoymously();
                Debug.Log("Already Logined : " + user.UserId);
            }
            else
            {
                isFirstLogin = true;
                Debug.Log("First Login : " + user.UserId);
            }    
        }
    }

    public void OnClickAnnoymously()
    {
        logInBtn.SetActive(false);
        LoginTxt_1.SetActive(true);

        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.Log("<color=red>SignInAnonymouslyAsync was canceled.</color>");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.Log($"<color=red>SignInAnonymouslyAsync encountered an error: {task.Exception}</color>");
                return;
            }
            else if (task.IsCompleted)
            {
                user = task.Result.User;
                Debug.Log($"<color=blue>User signed in: {user.UserId}</color>");

                isLogInSuccess = true;

                //데이터 로드 
                UserData.instance.GenerateUserData();
            }
        });
    }

    IEnumerator CheckLogIn()
    {
        while (!isLogInSuccess || !UserData.instance.isGetSavedData)
        {            
            yield return null;
        }

        if (isLogInSuccess && UserData.instance.isGetSavedData)
        {
            GetComponent<Title>().logInBtn_1.gameObject.SetActive(false);
            GetComponent<Title>().logInBtn_2.gameObject.SetActive(false);

            LoginTxt_1.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            LoginTxt_1.SetActive(false);

            LoginTxt_2.SetActive(true);
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene(11);
        }
    }
}
