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
    public int Lmana;
    public int Fdmg;
    public float Ldmg;
    public int Ctime;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        
        SKey = Int32.Parse(key);
        Name = info["Name"].Value<string>();
        Command = new List<int>(Array.ConvertAll(info["Command"].Value<string>().Split(','), int.Parse));
        Fmana = info["Fmana"].Value<int>();
        Lmana = info["Lmana"].Value<int>();
        Fdmg = info["Fdmg"].Value<int>();
        Ldmg = info["Ldmg"].Value<float>();
        Ctime = info["Ctime"].Value<int>();
    }
    
}
