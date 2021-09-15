﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    

    
    public enum Stage 
    {
        forest,
        GoblinViliage,
    }

    public Stage currentmap;                          // 현재맵
    public static BattleManager Instance;
    public int getGold = 0;                    // 얻은 골드
    public float Score = 0;                           // 현재 점수

    public int purpose; 
    public int BossInterval;
    public int loopCount = 0;

    public bool isBoss;                                 // 보스전중인가?
    public bool isContact = false;                      // 몬스터와 닿았는가? 
    public bool isUseSkill = false;                     // 스킬 사용중인가?
    public bool isStop = false;                         // ?? 쓸모없는 변수인듯?
    public bool isEnd = false;                          // 게임이 끝났는가?
    public bool PatternInputEnd = false;                // 패턴입력이 끝났는가?

    public bool isDash;                                 // 대쉬중인가?
    public List<int> Pattern_id = new List<int>();      // 현재 입력한 커맨드

    public Character character;                         // 플레이어스크립트
    public GameObject startPanel;                            // 게임 시작 판넬 ( 검정색)
    public MonsterGenerater monsterGenerater;           // 몬스터 소환 스크립트

    void Awake()
    {
    }
    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
        Instance = this;
        StartPanel();
    }

    public void PlayerDamage(float value)
    {
        if (!character.isParing)
        {
            character.Damaged(value);
        }
    }

    public void GetGold(int value)
    {
        getGold += value;
    }


    /// <summary>
    /// 게임 시작후 초기화함수
    /// </summary>
    public void StartPanel()
    {
        BattleManager.Instance.isEnd = false;
        character.Hp = character.MaxHp;
        character.Mp = character.MaxMp;
        purpose = 100;
        BossInterval = purpose;
        Pattern_id.Clear();
        Score = 0;
        getGold = 0;
        StartCoroutine(StartLerp());
        startPanel.GetComponent<Animator>().SetTrigger("isStart");

    }


    /// <summary>
    /// 초반 캐릭터 Position Lerp
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartLerp()
    {
        while (character.transform.position.x <= -1.81)
        {
            character.transform.position = Vector2.Lerp(character.transform.position, new Vector2(-1.81f, character.transform.position.y), 4 * Time.deltaTime);
            yield return null;
        }

        character.transform.position = new Vector2(-1.81f, character.transform.position.y);
    }


    /// <summary>
    /// 다음 스테이지
    /// </summary>
    public void nextStage()
    {
        if (currentmap == Stage.GoblinViliage)
        {
            currentmap = Stage.forest;
            loopCount++;
        }
        else
        {
            currentmap++;
        }
    }


    /// <summary>
    /// 보스가 죽고 이 함수로 몬스터 스폰시간을 조정해 판넬이 사라질때까지 시간을 줌
    /// </summary>
    /// <param name="value"></param>
    public void SetmonsterGenInterval(float value)
    {
        monsterGenerater.SpawnInterval = value;
        monsterGenerater.currentTime = 0;
    }
}
