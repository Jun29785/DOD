using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DtoMonster : MonoBehaviour
{
    public string Name;
    public float Hp;
    public float Speed;
    public float AttackDelay;
    public float AttackDistance;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;

    public DtoMonster(string name, float hp, float speed, float atkDelay, float atkDistance, float addScore, int addCoin)
    {
        name = this.Name;
        hp = this.Hp;
        speed = this.Speed;
        atkDelay = this.AttackDelay;
        atkDistance = this.AttackDistance;
        addScore = this.AddScore;
        addCoin = this.AddCoin;
    }
}
