using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;


public class UserDataManager : Singleton<UserDataManager>
{
    public static UserData user = new UserData();


    
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void Init()
    {
        TextAsset userData = Resources.Load<TextAsset>("UserDataSample");


        user = JsonConvert.DeserializeObject<UserData>(userData.text);

       //StartCoroutine(InitData());


    }

    public void Save()
    {
        string save = JsonConvert.SerializeObject(user);
        File.WriteAllText(Application.dataPath + "/Resources/UserDataSample.json", save);
        //StartCoroutine(SaveData());
    }



    public IEnumerator InitData()
    {

        yield return new WaitForSeconds(1);

        UnityWebRequest www = UnityWebRequest.Get("http://10.120.74.70:80/dataload");

        yield return www.Send();

        string init = www.downloadHandler.text;
        user = JsonConvert.DeserializeObject<UserData>(init);
    }


    IEnumerator SaveData()
    {

        string userdata = JsonConvert.SerializeObject(user);

        yield return new WaitForSeconds(1);
        WWWForm form = new WWWForm();

        form.AddField("userData", userdata);

        UnityWebRequest www = UnityWebRequest.Post("http://10.120.74.70:80/datasave", form); // 
        yield return www.Send();

        Debug.Log(www.downloadHandler.text);
    }
}
