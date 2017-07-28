using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserData : MonoBehaviour {
    //유저 정보 저장
    //로그인 창의 정보들
    string id;
    string pw;
    string name;
    //회원가입 창의 정보들
    void Start()
    {
    }
    public void setid(string a)
    {
        id = a;
    }
    public void setpw(string a)
    {
        pw = a;
    }
    public void setname(string a)
    {
        name = a;
    }
    public string getid()
    {
        return id;
    }
    public string getpw()
    {
        return pw;
    }
    public string getname()
    {
        return name;
    }
}
