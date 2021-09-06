using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public static class UserDataManager
{
    public static UserData user = new UserData();


    public static void Init()
    {
        string userData = File.ReadAllText(Application.dataPath + "/UserDataSample.json");

        user = JsonConvert.DeserializeObject<UserData>(userData);

        Debug.Log(user.Coin);
        Debug.Log(user.nickName);


    }

    public static void Save()
    {
        string save = JsonConvert.SerializeObject(user);
        File.WriteAllText(Application.dataPath + "/UserDataSample.json", save);
    }

}
