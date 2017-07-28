using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void resultFunction();

public class ConnectManager : MonoBehaviour {
    public static ConnectManager inst;
    
    public static ConnectManager getInst()
    {
        if(inst == null)
        {
            inst = new ConnectManager();
        }

        return inst;
    }

    private string result;

    public string _result {
        get
        {
            return result;
        }
    }

    public IEnumerator SendData(string url , WWWForm form , resultFunction resultFunction)
    {
        WWW www;

        www = new WWW(url, form.data);

        yield return www;

        result = www.text;

        www.Dispose();

        resultFunction();
    }

	
}
