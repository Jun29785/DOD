using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{

    public static GameSceneUIManager Instance;

    public RectTransform PlayerTransform;
    public Character character;
    public Slider Hpbar;
    public Slider Mpbar;
    public Text ScoreText;
    public Text Hpstate;
    public Text Mpstate;

    public Text WarningText;
    
    


    // 겜 오버 시 나오는것
    public GameObject GameOverPanel;
    public Text getCoinAmountText;
    public Text LastScore;

    void Start()
    {
        Hpbar.maxValue = character.MaxHp;
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
        ScoreText.text = BattleManager.Instance.Score.ToString();
    }

    public void GetScore(float _score)
    {
        BattleManager.Instance.Score += _score;
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
        WarningText.gameObject.SetActive(false);
        getCoinAmountText.text = "+ " + BattleManager.Instance.getGold;
        LastScore.text = "Score : " + BattleManager.Instance.Score;
        GameOverPanel.SetActive(true);
    }

    public void ApearWarningText(int index)
    {
        switch (index)
        {
            case 1:
                StartCoroutine(SetWarningText("마나가 부족합니다."));
                break;

            case 2:
                StartCoroutine(SetWarningText("스킬이 쿨타임입니다."));
                break;

            default:
                break;
        }
    }

    IEnumerator SetWarningText(string str)
    {
        WarningText.text = str;

        WarningText.gameObject.SetActive(true);
        WarningText.gameObject.GetComponent<Animator>().SetTrigger("Set");


        yield return new WaitForSeconds(0.9f);

        WarningText.gameObject.SetActive(false);
    }
}
