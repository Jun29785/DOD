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

     
    public bool isexist = true;
    public bool nickExist;
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void Init()
    {
       TextAsset userData = Resources.Load<TextAsset>("UserDataSample");

        Debug.Log(userData);

       user = JsonConvert.DeserializeObject<UserData>(userData.text);

       //StartCoroutine(InitData());


    }



    
    public void Save()
    {
        string save = JsonConvert.SerializeObject(user);
        
        File.WriteAllText(Application.dataPath + "/Resources/UserDataSample.json", save);
        //StartCoroutine(SaveData());   
    }

    public void DeviceCheck()
    {
        StartCoroutine(DeviceChecker());
    }

    public IEnumerator DeviceChecker()
    {


        string Deviceid = SystemInfo.deviceUniqueIdentifier.ToString();

        yield return new WaitForSeconds(1);
        WWWForm form = new WWWForm();

        form.AddField("device", Deviceid.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://10.120.74.70/device", form); 
        yield return www.Send();

        string a = www.downloadHandler.text;
        a = a.Substring(0, a.Length - 1);
        string[] parsing = a.Split(':');
        Debug.Log(parsing[1]);
        isexist = (parsing[1] != "false") ? true : false;
        Debug.Log(isexist);

    }

    public void Sendnick()
    {
        StartCoroutine(SendnickName());
    }

    public IEnumerator SendnickName()
    {

        string Deviceid = SystemInfo.deviceUniqueIdentifier.ToString();
        string nick = user.nickname.ToString();

        WWWForm form = new WWWForm();

        form.AddField("device", Deviceid.ToString());
        form.AddField("nickname", nick);
        UnityWebRequest www = UnityWebRequest.Post("http://10.120.74.70/create_name", form); // 
        yield return www.Send();

        string a = www.downloadHandler.text;
        a = a.Substring(0, a.Length - 1);
        string[] parsing = a.Split(':');
        Debug.Log(parsing[1]);
        nickExist = (parsing[1] != "false") ? true : false;

        

    }
    public IEnumerator InitData()
    {


        WWWForm form = new WWWForm();

        form.AddField("nickname", user.nickname);
        UnityWebRequest www = UnityWebRequest.Post("http://10.120.74.70:80/dataload",form);

        yield return www.Send();

        string init = www.downloadHandler.text;
        Debug.Log(init);
        user = JsonConvert.DeserializeObject<UserData>(init);
    }


    IEnumerator SaveData()
    {

        string userdata = JsonConvert.SerializeObject(user);

        string data = userdata;
        yield return new WaitForSeconds(1);
        WWWForm form = new WWWForm();

        form.AddField("userData",userdata.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://10.120.74.70:80/datasave", form); // 
        yield return www.Send();

        Debug.Log(www.downloadHandler.text);
    }
}
