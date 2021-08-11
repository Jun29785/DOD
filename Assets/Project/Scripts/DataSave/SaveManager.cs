using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void Save(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.dataPath, "SaveData.dat");
        FileStream stream = File.Create(path);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Load()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.dataPath, "SaveData.dat");
            FileStream stream = File.OpenRead(path);
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
