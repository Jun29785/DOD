using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    
    public bool isContact = false;
    public bool isUseSkill = false;
    public bool isStop = false;
    public bool isEnd = false;
    public bool PatternInputEnd = false;
    public int getGold = 0; 
    public float Score;

    public enum Monster
    {
        Goblin,
        Monster1,
    }

    public List<int> Pattern_id = new List<int>();


    public Character character;
    
    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        Instance = this;
        Pattern_id.Clear();
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
