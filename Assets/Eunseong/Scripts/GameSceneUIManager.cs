using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{

    public static GameSceneUIManager Instance;

    public Character character; // 캐릭터 스크립트
    public Text ScoreText; // 점수

    public Text WarningText; // 마나없습니다 등등 표기 텍스트
    public GameObject Content; // 스킬 쿨타임오브젝트의 부모객체

    public GameObject Skill_CoolTime_Prefab;


    public GameObject UI; // 전체 UI

    // 겜 오버 시 나오는것
    public GameObject GameOverPanel;
    public Text getCoinAmountText;
    public Text LastScore;
    
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

            Invoke("Gameover", 0.7f);
            
            
        }
        
    }


    void Gameover()
    {
        UI.SetActive(false);
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
