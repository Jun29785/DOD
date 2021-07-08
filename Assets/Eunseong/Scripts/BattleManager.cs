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
    public Player player;
    void Start()
    {
        instance = this;
        player = GetComponentInChildren<Player>();
    }

    void Update()
    {
        
    }

    public void PlayerDamage(float value)
    {

        player.Damage(value);
    }
}
