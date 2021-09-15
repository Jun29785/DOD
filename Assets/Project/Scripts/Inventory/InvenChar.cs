﻿using System.Collections;
using UnityEngine;
using DOD.DB;
public class InvenChar : MonoBehaviour
{
    public int CharKey;
    public string CharName;
    public float HP;
    public float MP;
    public float POWER;
    public float attackDistance;
    public float attakDelay;

    public int UpgradeCost;
    public int Level;
    public void SetData(int level)
    {
        UserDataManager.user.skill_level[CharName] = level;
        UserDataManager.Instance.Save();
    }

    public void LoadData(int charkey)
    {
        this.CharKey = charkey;
        var charDict = DataBaseManager.Instance.tdCharacterDict[this.CharKey];

        this.CharName = charDict.Name;
        this.HP = charDict.THP;
        this.MP = charDict.TMP;
        this.POWER = charDict.TPOWER;
        this.attackDistance = charDict.attackDistance;
        this.attakDelay = charDict.attakDelay;
        Debug.Log(charDict.Name);
        this.Level = UserDataManager.user.character_level[charDict.Name];
        this.UpgradeCost = charDict.TUC;
    }

    public void Upgrade()
    {
        var coin = UserDataManager.user.coin;
        if(coin < UpgradeCost)
        {
            StartCoroutine(LobbyUIManager.Instance.NoMoney());
            return;
        }
        UserDataManager.user.coin -= UpgradeCost;
        Level += 1;
        UserDataManager.user.character_level[name] += 1;
    }
}
