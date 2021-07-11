using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;
    bool isBattle = false;

    public bool isBattleP
    {
        get
        {
            return isBattle;
        }
        set
        {
            isBattle = value;
        }
    }

    public Character character;
    void Start()
    {
        instance = this;
        character = GetComponentInChildren<Character>();
    }


    public void PlayerDamage(float value)
    {

        character.Damage(value);
    }
}
