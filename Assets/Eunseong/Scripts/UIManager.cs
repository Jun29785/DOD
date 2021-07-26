using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    float Score;


    public Character character;
    public Slider Hpbar;
    public Slider Mpbar;
    public Text ScoreText;
    public Text Hpstate;
    public Text Mpstate;
    public GameObject GameOverPanel;

    public float ScoreP {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
        }
    }

    void Start()
    {
        Hpbar.maxValue = character.MaxHp;
        Score = 0;
        Mpbar.maxValue = character.MaxMp;
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
        if(character.Hp <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }
}
