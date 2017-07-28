using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FacebookManager : MonoBehaviour {

    public Text Show_Text;


    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(Initialize, OnHideUnity);
        }
        else
        {

        }
    }

    void Initialize()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Show_Text.text = "Login Failed";
        }
    }

    public void Logout()
    {
        FB.LogOut();

    }

    private void AuthCallbackLogin(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

            Show_Text.text = aToken.UserId;

            foreach (string perm in aToken.Permissions)
            {
                Show_Text.text = perm;
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is loggen in");
        }
        else
        {
            Debug.Log("FB is not loggen in");
        }
        DealWithFBMenus(FB.IsLoggedIn);
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void LogIn()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("email");
        permissions.Add("user_friends");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);

    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
            Debug.Log(result.Error);
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is loggen in");
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                Debug.Log(aToken.UserId);


                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }

                SceneManager.LoadScene(3);
            }
            else
            {
                Debug.Log("FB is not loggen in");
            }

            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/pictures?type=squre&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
    }

    void DisplayUsername(IResult result)
    {


        if (result.Error == null)
        {
            Debug.Log("Login Success");
        }

        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        /*
        if(result.Texture != null)
        {
            Image ProfilePic = UserPic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        */
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
