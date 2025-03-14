using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;

public class LoginTest : MonoBehaviour
{
    FirebaseAuth auth;
    public static FirebaseUser user;
    //private FirebaseDatabase database = null;
       

    public Text loginTxt;

    // Start is called before the first frame update
    void Start()
    {
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mesterday-project-a6798-default-rtdb.firebaseio.com");

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        
        //StartCoroutine(AlreadyUser());
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                //isAlreadyLogin = true;
            }
        }
    }

    public void OnClickAnnoymously()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                //loginTxt.text = "SignInAnonymouslyAsync was canceled.";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                //loginTxt.text = "SignInAnonymouslyAsync encountered an error: " + task.Exception;
                return;
            }
            if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newUser = task.Result.User;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                //loginTxt.text = string.Format("User signed in successfully: {0} ({1})",
                //newUser.DisplayName, newUser.UserId);
            }


        });

        //loginTxt.text = user.UserId;

    }
}
