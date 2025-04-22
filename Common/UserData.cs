using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
//firebase key 도 같은 이름 
public class UserDataInfo
{
    public string nickName;
    public string schoolName;

    public int stage1_Score;
    public int stage2_Score;
    public int stage3_Score;
    public int stage4_Score;
    public int stage5_Score;
    public int stage6_Score;

    public int playerPos;

    public bool clearTutorial;
    public bool clear01;
    public bool clear02;
    public bool clear03;
    public bool clear04;
    public bool clear05;
    public bool clear06;
    public bool EndingOpen;
    public bool clearEnding;

    public bool NoteChange;
}

public class UserData : MonoBehaviour
{
    public static UserData instance;

    [HideInInspector] public UserDataInfo userData = new UserDataInfo();
    [HideInInspector] public bool isGetSavedData = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InvokeGenerateUserData()
    {
        Invoke("GenerateUserData", 2f);
    }

    public void GenerateUserData()
    {
        FirebaseUser user = AnonymousLogin.user;

        Debug.Log($"GenerateUserData target userid {user.UserId}");

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        try
        {
            reference.Child("users").Child(user.UserId).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Load Cancel");
                }
                else if (task.IsFaulted)
                {
                    Debug.Log("Load Failed");
                }
                else
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log($"snapshot.Exists {snapshot.Exists}");

                    if (!snapshot.Exists)
                        InitUserDataInfo();
                    else
                        GenerateUserDataInfo(snapshot);

                    Debug.Log($"<color=blue>[GenerateUserData] Successly</color>");
                    isGetSavedData = true;
                }
            });
        }
        catch (Exception e) 
        {
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// 데이터 Set 
    /// </summary>
    public void SetUserDataInfo(string keyString, object value)
    {
        // class 정보 수정
        FieldInfo field = typeof(UserDataInfo).GetField(keyString);
        field.SetValue(userData, value);

        // firebase 데이터 저장 
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);

        FirebaseUser user = AnonymousLogin.user;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        Dictionary<string, object> data = new Dictionary<string, object>();
        data[$"/users/{user.UserId}/{keyString}"] = value;

        reference.UpdateChildrenAsync(data)
            .ContinueWith(task => {
                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log("<color=blue>[SetUserDataInfo] successfully !</color>");
                }
                else
                {
                    Debug.Log($"<color=red>[SetUserDataInfo] Failed : {task.Exception}</color>");
                }
            });
    }

    /// <summary>
    /// 현재 로그인한 유저의 데이터가 없다면, 초기 데이터로 기록 
    /// </summary>
    public void InitUserDataInfo()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);

        FirebaseUser user = AnonymousLogin.user;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        UserDataInfo initData = new UserDataInfo();
        string initJsonData = JsonUtility.ToJson(initData);

        reference.Child("users").Child(user.UserId).SetRawJsonValueAsync(initJsonData)
            .ContinueWith(task => {
                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log("<color=blue> [InitUserDataInfo] successfully !</color>");
                    GenerateUserData();
                }
                else
                {
                    Debug.Log($"<color=red>[InitUserDataInfo] Failed : {task.Exception}</color>");
                }
            });
    }

    /// <summary>
    /// 계정 데이터를 class 에 받아옴 
    /// </summary>
    void GenerateUserDataInfo(DataSnapshot snapshot)
    {
        userData.nickName = snapshot.Child("nickName").Value.ToString();
        Debug.Log($"userData.nickName: {userData.nickName}");

        userData.schoolName = snapshot.Child("schoolName").Value.ToString();
        Debug.Log($"userData.schoolName: {userData.schoolName}");

        userData.stage1_Score = int.Parse(snapshot.Child("stage1_Score").Value.ToString());
        Debug.Log($"userData.stage1_Score: {userData.stage1_Score}");

        userData.stage2_Score = int.Parse(snapshot.Child("stage2_Score").Value.ToString());
        Debug.Log($"userData.stage2_Score: {userData.stage2_Score}");

        userData.stage3_Score = int.Parse(snapshot.Child("stage3_Score").Value.ToString());
        Debug.Log($"userData.stage3_Score: {userData.stage3_Score}");

        userData.stage4_Score = int.Parse(snapshot.Child("stage4_Score").Value.ToString());
        Debug.Log($"userData.stage4_Score: {userData.stage4_Score}");

        userData.stage5_Score = int.Parse(snapshot.Child("stage5_Score").Value.ToString());
        Debug.Log($"userData.stage5_Score: {userData.stage5_Score}");

        userData.stage6_Score = int.Parse(snapshot.Child("stage6_Score").Value.ToString());
        Debug.Log($"userData.stage6_Score: {userData.stage6_Score}");

        userData.playerPos = int.Parse(snapshot.Child("playerPos").Value.ToString());
        Debug.Log($"userData.playerPos: {userData.playerPos}");

        userData.clearTutorial = bool.Parse(snapshot.Child("clearTutorial").Value.ToString());
        Debug.Log($"userData.clearTutorial: {userData.clearTutorial}");

        userData.clear01 = bool.Parse(snapshot.Child("clear01").Value.ToString());
        Debug.Log($"userData.clear01: {userData.clear01}");

        userData.clear02 = bool.Parse(snapshot.Child("clear02").Value.ToString());
        Debug.Log($"userData.clear02: {userData.clear02}");

        userData.clear03 = bool.Parse(snapshot.Child("clear03").Value.ToString());
        Debug.Log($"userData.clear03: {userData.clear03}");

        userData.clear04 = bool.Parse(snapshot.Child("clear04").Value.ToString());
        Debug.Log($"userData.clear04: {userData.clear04}");

        userData.clear05 = bool.Parse(snapshot.Child("clear05").Value.ToString());
        Debug.Log($"userData.clear05: {userData.clear05}");

        userData.clear06 = bool.Parse(snapshot.Child("clear06").Value.ToString());
        Debug.Log($"userData.clear06: {userData.clear06}");

        userData.EndingOpen = bool.Parse(snapshot.Child("EndingOpen").Value.ToString());
        Debug.Log($"userData.EndingOpen: {userData.EndingOpen}");

        userData.clearEnding = bool.Parse(snapshot.Child("clearEnding").Value.ToString());
        Debug.Log($"userData.clearEnding: {userData.clearEnding}");

        userData.NoteChange = bool.Parse(snapshot.Child("NoteChange").Value.ToString());
        Debug.Log($"userData.NoteChange: {userData.NoteChange}");
    }
}
