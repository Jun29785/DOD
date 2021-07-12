using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{

    bool isContact = false;

    public bool isContactP
    {
        get
        {
            return isContact;
        }
        set
        {
            isContact = value;
        }
    }

    public Character character;
    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
    }


    public void PlayerDamage(float value)
    {

        character.Damage(value);
    }
}
