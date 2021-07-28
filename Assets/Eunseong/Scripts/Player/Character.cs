using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public float MaxHp;
    [HideInInspector]
    public float Hp;
    public float MaxMp;
    [HideInInspector]
    public float Mp;
    public Transform attackTransform; //공격 위치
    public float AttackRange; // 공걱 범위
    public float AttackDistance; // 공격 거리
    public float AttackDelay;
    public float StrikingPower;
    private void Start()
    {
        Hp = MaxHp;
        Mp = MaxMp;
    }


    public void Damage(float value) // 데미지 입는함수
    {
        Hp -= value;
        Debug.Log(this.Hp);
    }

    public void UseMp(float value) //마나 깎는 함수
    {
        Mp -= value;

    }


    public void DieCheck() //죽는거 체크
    {
        if (Hp <= 0)
        {
            Debug.Log("Die!");
            return;
        }
    }
}
