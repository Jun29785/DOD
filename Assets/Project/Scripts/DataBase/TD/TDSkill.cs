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
    public string Command_string; // 같이 수정 필요
    public List<int> Command;
    public int Fmana;
    public int Lmana;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        
        SKey = Int32.Parse(key);
        Name = info["Name"].Value<string>();
        Command_string = info["Command"].Value<string>();
        Command = new List<int>(Array.ConvertAll(Command_string.Split(','), int.Parse));
        Fmana = info["Fmana"].Value<int>();
        Lmana = info["Lmana"].Value<int>();
        Debug.Log(SKey);
    }
    
}
