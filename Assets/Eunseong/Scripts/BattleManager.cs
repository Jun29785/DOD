﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public bool isContact = false;
    public bool isUseSkill = false;
    public bool isStop = false;
    public bool isEnd = false;
    public int getGold = 0;

    public static BattleManager Instance;

    public Character character;
    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        Instance = this;
    }


    public void PlayerDamage(float value)
    {

        character.Damage(value);
    }

    public void GetGold(int value)
    {
        getGold += value;
    }
}
