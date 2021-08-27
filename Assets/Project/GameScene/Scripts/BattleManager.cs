using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public static int getGold = 0;
    public static float Score = 0;

    public int purpose;
    public int BossInterval;


    public bool isBoss;
    public bool isContact = false;
    public bool isUseSkill = false;
    public bool isStop = false;
    public bool isEnd = false;
    public bool PatternInputEnd = false;

    public bool isDash;
    public List<int> Pattern_id = new List<int>();

    public Character character;
    

    void Awake()
    {
        Score = 18;
        purpose = 20;
        BossInterval = purpose;
    }
    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        Instance = this;
        Pattern_id.Clear();
        Score = 0;
        getGold = 0;
    }

    public void PlayerDamage(float value)
    {
        character.Damaged(value);
    }

    public void GetGold(int value)
    {
        getGold += value;
    }
}
