using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : UIManager
{

    public static GameSceneUIManager Instance;

    public Character character; // 캐릭터 스크립트
    public Text ScoreText; // 점수

    public Text WarningText; // 마나없습니다 등등 표기 텍스트
    public GameObject Content; // 스킬 쿨타임오브젝트의 부모객체

    public GameObject Skill_CoolTime_Prefab;

    public Slider BossHp;

    
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
        GameOverCheck();
    }



    void ScoreUpdate()
    {
        SetText<float>(ScoreText, BattleManager.Score);
    }

    public void GetScore(float _score)
    {
        BattleManager.Score += _score;
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
        LoadingSceneController.LoadScene("ResultScene");
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
