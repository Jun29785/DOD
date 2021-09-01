using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void Save(UserData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.dataPath, "SaveData.dat");
        FileStream stream = File.Create(path);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static UserData Load()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.dataPath, "SaveData.dat");
            FileStream stream = File.OpenRead(path);
            UserData data = (UserData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            return default;
        }
    }
}
