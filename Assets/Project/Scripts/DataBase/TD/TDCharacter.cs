using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class TDCharacter : TableBase
{
    public int UnitNo;
    public string name;
    public float HP;
    public float MP;
    public float POWER;
    public float Lhp;
    public float Lmp;
    public float Lpower;
    public float attackDistance;
    public float attakDelay;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        this.UnitNo = Int32.Parse(key);
        Debug.Log("UnitNo");
        this.name = info["Name"].Value<string>();
        Debug.Log("Name");
        this.HP = info["Hp"].Value<float>();
        Debug.Log("HP");
        this.MP = info["Mp"].Value<float>();
        Debug.Log("MP");
        this.POWER = info["Power"].Value<float>();
        Debug.Log("POWER");
        this.Lhp = info["LHp"].Value<float>();
        Debug.Log("LH");
        this.Lmp = info["LMp"].Value<float>();
        Debug.Log("LM");
        this.Lpower = info["Lpower"].Value<float>();
        Debug.Log("LP");
        this.attackDistance = info["AttackDistance"].Value<float>();
        Debug.Log("AD");
        this.attakDelay = info["AttackDelay"].Value<float>();
        Debug.Log("ADEL");
    }
}
