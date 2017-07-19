using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Net;
using System;

using UnityEngine.SceneManagement;

public class phplogin : MonoBehaviour {
    LoginAndNewUser LData;
    Coroutine login;
    
    public void Login()
    {
        LData = FindObjectOfType<LoginAndNewUser>();

        Debug.Log("로그인합니다.");

        string id = LData.getid();
        string pw = LData.getpw();

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("user_pw", pw);

        resultFunction rf = new resultFunction(LoginResult);
        ConnectManager.getInst().SendData("http://52.78.20.5/login_db.php", form, rf);
    }

    public void LoginResult()
    {

        Debug.Log(ConnectManager.getInst()._result);

        if (ConnectManager.getInst()._result != "")
        {
            StopCoroutine(login);

            SceneManager.LoadScene(4);
        }
    }
	
}
