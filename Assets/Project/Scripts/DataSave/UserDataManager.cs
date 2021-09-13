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

    
    public bool isexist;
    public bool nickExist;
    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        

    }

    public void Init()
    {
       //TextAsset userData = Resources.Load<TextAsset>("UserDataSample");
        

       //user = JsonConvert.DeserializeObject<UserData>(userData.text);

       StartCoroutine(InitData());
    

    }



    
    public void Save()
    {
        //string save = JsonConvert.SerializeObject(user);
        
        //File.WriteAllText(Application.dataPath + "/Resources/UserDataSample.json", save);
        StartCoroutine(SaveData());   
    }

    public void DeviceCheck()
    {
        StartCoroutine(DeviceChecker());
    }

    public IEnumerator DeviceChecker()
    {

        yield return null;

        WWWForm form = new WWWForm();

        form.AddField("device", SystemInfo.deviceUniqueIdentifier);

        UnityWebRequest www = UnityWebRequest.Post("http://15.165.160.44:3000/device", form); 
        yield return www.Send();

        string a = www.downloadHandler.text;
        Debug.Log(a);
        a = a.Substring(0, a.Length - 1);
        string[] parsing = a.Split(':');

        isexist = (parsing[1] != "false") ? true : false;
        Debug.Log(isexist);


        if (isexist)
        {
            GameManager.Instance.isnameInput = true;
            Init();
        }

    }

    public void Sendnick()
    {
        StartCoroutine(SendnickName());
    }

    public IEnumerator SendnickName()
    {
        yield return null;

        string nick = user.nickname;

        WWWForm form = new WWWForm();

        form.AddField("device", SystemInfo.deviceUniqueIdentifier);
        form.AddField("nickname", nick);
        UnityWebRequest www = UnityWebRequest.Post("http://15.165.160.44:3000/create_name", form); // 
        yield return www.Send();

        string a = www.downloadHandler.text;
        a = a.Substring(0, a.Length - 1);
        string[] parsing = a.Split(':');
        Debug.Log(parsing[1]);
        nickExist = (parsing[1] != "false") ? true : false;

        

    }
    public IEnumerator InitData()
    {

        yield return null;

        WWWForm form = new WWWForm();

        form.AddField("device", SystemInfo.deviceUniqueIdentifier);
        UnityWebRequest www = UnityWebRequest.Post("http://15.165.160.44:3000/dataload", form);

        yield return www.Send();

        string init = www.downloadHandler.text;
        Debug.Log(init);
        user = JsonConvert.DeserializeObject<UserData>(init);


        Debug.Log(user.nickname);
        Debug.Log(user.highscore);
        Debug.Log(user.coin);
            foreach (var item in user.skill_level)
            {
                Debug.Log(item.Key);
                Debug.Log(item.Value);
            }
    }


    IEnumerator SaveData()
    {

        string userdata = JsonConvert.SerializeObject(user);

        string data = userdata;
        yield return new WaitForSeconds(1);
        WWWForm form = new WWWForm();

        form.AddField("userData",userdata.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://15.165.160.44:3000/datasave", form); // 
        yield return www.Send();

        Debug.Log(www.downloadHandler.text);
    }


    public void game_end_Func(int score)
    {
        StartCoroutine(game_end(score));
    }
    IEnumerator game_end(int score)
    {
        WWWForm form = new WWWForm();

        form.AddField("nickname", user.nickname);
        form.AddField("score", score);
        Debug.Log(user.nickname);
        Debug.Log(score);

        UnityWebRequest www = UnityWebRequest.Post("http://15.165.160.44:3000/game_end", form);
        yield return www.Send();

        Debug.Log("게임 점수 보내기 완료");
        Debug.Log(www.downloadHandler.text);

    }
}
