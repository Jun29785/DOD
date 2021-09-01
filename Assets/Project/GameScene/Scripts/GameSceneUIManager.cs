using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : UIManager
{

    public static GameSceneUIManager Instance;

    [Header("GamePlaying")]
    public Character character; // 캐릭터 스크립트
    public Text ScoreText; // 점수
    public Text WarningText; // 마나없습니다 등등 표기 텍스트
    public GameObject Content; // 스킬 쿨타임오브젝트의 부모객체
    public GameObject Skill_CoolTime_Prefab; // 스킬쿨타임 오브젝트 프리팹

    [Header("GameOVerPanel")]
    public GameObject GameoverPanel;
    public Text GOScoreText;
    public Text GetGoldText;

    
    void Start()
    {
        Instance = this;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
        GameOverCheck();
    }



    void ScoreUpdate()
    {
        SetText<float>(ScoreText, BattleManager.Instance.Score);
    }

    public void GetScore(float _score)
    {
        BattleManager.Instance.Score += _score;
    }

    void GameOverCheck()
    {
        if(BattleManager.Instance.isEnd)
        {
            character.Hp = 0;
            Invoke("Gameover", 1);
            
            
        }
        
    }


    void Gameover()
    {
        GOScoreText.text = BattleManager.Instance.Score.ToString();
        GetGoldText.text = BattleManager.Instance.getGold.ToString();
        GameoverPanel.SetActive(true);
        UserData.Coin += BattleManager.Instance.getGold;
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
            case 3:
                StartCoroutine(SetWarningText("스킬을 이미 사용중입니다."));
                break;
            default:
                break;
        }
    }

    public void Create_SkillCoolTimeObject(string name, float cooltime)
    {
        var obj = Instantiate(Skill_CoolTime_Prefab);
        obj.GetComponent<SkillCoolTimeObject>().skillName = name;
        obj.GetComponent<SkillCoolTimeObject>().CoolTime = cooltime;
        obj.gameObject.transform.parent = Content.transform;
        obj.gameObject.transform.localPosition = Vector2.zero;  
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
