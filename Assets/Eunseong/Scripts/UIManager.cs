using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Character character;
    public Slider Hp;
    public Text ScoreText;
    float Score;

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
        Hp.maxValue = character.MaxHp;
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HpBarUpdate();
        ScoreUpdate();
    }

    void HpBarUpdate()
    {
        Hp.value = character.Hp;

    }

    void ScoreUpdate()
    {
        ScoreText.text = Score.ToString();
    }
}
