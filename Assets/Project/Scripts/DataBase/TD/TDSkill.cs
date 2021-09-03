using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class TDSkill : TableBase
{
    public int SKey;
    public string Name;
    public List<int> Command;
    public int Fmana;
    public float Lmana;
    public int Fdmg;
    public float Ldmg;
    public int Ctime;
    public string Description;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        
        SKey = Int32.Parse(key);
        Debug.Log(4);

        Name = info["Name"].Value<string>();
            Debug.Log(7);
        Command = new List<int>(Array.ConvertAll(info["Command"].Value<string>().Split(','), int.Parse));
            Debug.Log(8);
        Fmana = info["Fmana"].Value<int>();
            Debug.Log(9);
        Lmana = info["Lmana"].Value<float>();
            Debug.Log(10);
        Fdmg = info["Fdmg"].Value<int>();
            Debug.Log(11);
        Ldmg = info["Ldmg"].Value<float>();
            Debug.Log(12);
        Ctime = info["Ctime"].Value<int>();
            Debug.Log(13);
        //Description = info["Description"].Value<string>();
            Debug.Log(14);

    }

}
