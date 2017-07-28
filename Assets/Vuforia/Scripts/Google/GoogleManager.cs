using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoogleManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                SceneManager.LoadScene(3);
                //tx_Email.text = ((PlayGamesLocalUser)Social.localUser).Email + "-" + PlayGamesPlatform.Instance.GetUserEmail();
            }

            else
            {
                Debug.Log("Login Failed");
            }
            /*
            GooglePlayGames.OurUtils.PlayGamesHelperObject.RunOnGameThread(
                () =>
                {
                    Debug.Log("Local User's email is " + ((PlayGamesLocalUser)Social.localUser).Email);
                });
                */

        });
    }

    /*
    public void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();

    }
    */
}
