using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginAndNewUser : MonoBehaviour
{
    //로그인 창의 정보들
    InputField id;
    InputField password;
    string id_Text = "";
    string pw_Text = "";
    public Text test;
    public string url = "http://52.78.20.5/login_json.php";
    bool user = false;
    //회원가입 창의 정보들
    public InputField id2;
    public InputField pw;
    public InputField uname;
    public InputField email;
    public Toggle User;
    public Toggle Owner;

    public string user_id;
    public string user_pw;
    public string user_name;
    public string user_email;
    public string owner = "0";
    public string url2 = "http://52.78.20.5/New_user_json.php";

    //패스워드 숨기기
    string mask = "";
    public Text MaskOutput;
    

    void Start()
    {
        id = gameObject.GetComponent<InputField>();
        password = gameObject.GetComponent<InputField>();
        id2 = gameObject.GetComponent<InputField>();
        pw = gameObject.GetComponent<InputField>();
        uname = gameObject.GetComponent<InputField>();
        email = gameObject.GetComponent<InputField>();
    }

    //로그인
    public void id_input(string id)
    {
        this.id_Text = id;

    }

    public void pw_input(string pw)
    {
        Debug.Log(pw);

        if (pw.Equals(""))
        {  
            mask = "";
        }

        else if (pw.Length==1)
        {
            mask = "*";
        }

        else if (pw_Text.Length > pw.Length)
        {
            mask = mask.Substring(0, mask.Length - 1);
        }

        else
        {
            mask += "*";
        }

        this.pw_Text = pw;
        MaskOutput.text = mask;
    }
    public string getid()
    {
        return id_Text;
    }
    public string getpw()
    {
        return pw_Text;
    }

    //회원가입
    public void idInput(string id)
    {
        this.user_id = id;
        
    }

    public void pwInput(string pw)
    {
        this.user_pw = pw;
    }
    public void nameInput(string name)
    {
        this.user_name = name;
    }

    public void emailInput(string email)
    {
        this.user_email = email;
    }

    /*
    public void choice_User()
    {
        user = true;
        if (User.isOn)
        {
            owner = "0";
            Debug.Log("User Select");
        }
        else
        {
            owner = "1";
            Debug.Log("Owner Select");
        }
    }

    public void choice_Owner()
    {
        user = false;
        if (Owner.isOn)
        {
            owner = "1";
            Debug.Log("Owner Select");
        }
        else
        {
            owner = "0";
            Debug.Log("User Select");
        }
    }
    */
}