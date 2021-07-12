using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public float MaxHp;
    public float Hp;
    public Transform attackTransform; //공격 위치
    public float AttackRange; // 공걱 범위
    public float AttackDelay;
    public float StrikingPower;
    private void Start()
    {
        Hp = MaxHp;
    }


    public void Damage(float value) // 데미지 입는함수
    {
        Hp -= value;
        Debug.Log(this.Hp);
    }
}
