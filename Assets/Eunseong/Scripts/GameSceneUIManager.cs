﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{
    float Score;

    public static GameSceneUIManager Instance;

    public RectTransform PlayerTransform;
    public Character character;
    public Slider Hpbar;
    public Slider Mpbar;
    public Text ScoreText;
    public Text Hpstate;
    public Text Mpstate;

    public float ScoreP
    {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
        }
    }


    // 겜 오버 시 나오는것
    public GameObject GameOverPanel;
    public Text getCoinAmountText;

    void Start()
    {
        Hpbar.maxValue = character.MaxHp;
        Score = 0;
        Mpbar.maxValue = character.MaxMp;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        HpUpdate();
        MpUpdate();
        ScoreUpdate();
        GameOverCheck();
    }

    void HpUpdate()
    {
       // Hpbar.gameObject.transform.position =  new Vector2 (PlayerTransform.position.x, PlayerTransform.position.y + 0.6f);
        Hpbar.value = character.Hp;
        Hpstate.text = character.Hp + " / " + character.MaxHp;
    }

    void MpUpdate()
    {
        Mpbar.value = character.Mp;
        Mpstate.text = character.Mp + " / " + character.MaxMp;

    }

    void ScoreUpdate()
    {
        ScoreText.text = Score.ToString();
    }

    public void GetScore(float _score)
    {
        Score += _score;
    }

    void GameOverCheck()
    {
        if(BattleManager.Instance.isEnd)
        {
            Invoke("Gameover", 1);
            
            
        }
    }


    void Gameover()
    {
        Time.timeScale = 0;
        getCoinAmountText.text = "+ " + BattleManager.Instance.getGold;
        GameOverPanel.SetActive(true);
    }
}
