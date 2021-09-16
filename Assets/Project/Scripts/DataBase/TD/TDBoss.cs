using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TDBoss : TableBase
{
    public int UnitNo;
    public string Name;
    public float HP;
    public float Power;
    public float Speed;
    public float attackDistance;
    public float attakDelay;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;
    public float SkillDelay;



    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        UnitNo = Int32.Parse(key);
        Debug.Log(UnitNo);
        Name = info["Name"].Value<string>();
        Debug.Log(Name);
        HP = info["Hp"].Value<int>();
        Debug.Log(HP);
        Power = info["Power"].Value<float>();
        Debug.Log(Power);
        Speed = info["Speed"].Value<float>();
        Debug.Log(Speed);
        attakDelay = info["AttackDelay"].Value<float>();
        Debug.Log(attakDelay);
        attackDistance = info["AttackDistance"].Value<float>();
        Debug.Log(attackDistance);
        AddScore = info["AddScore"].Value<float>();
        Debug.Log(AddScore);
        AddCoin = info["AddCoin"].Value<int>();
        Debug.Log(AddCoin);
        CoinAmount = info["CoinAmount"].Value<int>();
        Debug.Log(CoinAmount);
        SkillDelay = info["SkillDelay"].Value<float>();
        Debug.Log(SkillDelay);
    }
}
