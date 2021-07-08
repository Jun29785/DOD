﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    float AttackDelay = 3;
    float currentDelay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay += Time.deltaTime;


        if (BattleManager.instance.isBattleP) {
            if (currentDelay >= AttackDelay)
            {
                BattleManager.instance.PlayerDamage(1);
                currentDelay = 0;
            }
        }
    }
}
