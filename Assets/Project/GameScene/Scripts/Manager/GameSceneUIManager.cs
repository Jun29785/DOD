using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : UIManager
{

    public static GameSceneUIManager Instance;

    [Header("GamePlaying")]
    public Character character;                 // 캐릭터 스크립트
    public Text ScoreText;                      // 점수
    public Text WarningText;                    // 마나없습니다 등등 표기 텍스트
    public GameObject Content;                  // 스킬 쿨타임오브젝트의 부모객체
    public GameObject Skill_CoolTime_Prefab;    // 스킬쿨타임 오브젝트 프리팹

    [Header("GameOVerPanel")]
    public GameObject GameoverPanel;            // 게임오버 판넬
    public Text GOScoreText;                    // 게임오버시 점수
    public Text GetGoldText;                    // 게임오버시 얻은 골드

    
    void Start()
    {
        Instance = this;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    void Update()
    {
        ScoreUpdate();
        GameOverCheck();
    }



    /// <summary>
    /// 점수 세팅
    /// </summary>
    void ScoreUpdate()
    {
        SetText<float>(ScoreText, BattleManager.Instance.Score);
    }

    /// <summary>
    /// 점수 올리기
    /// </summary>
    /// <param name="_score"></param>
    public void GetScore(float _score)
    {
        BattleManager.Instance.Score += _score;
    }

    /// <summary>
    /// 게임오버 체크
    /// </summary>
    void GameOverCheck()
    {
        if(BattleManager.Instance.isEnd)
        {
            character.Hp = 0;
            Invoke("Gameover", 1);
            
            
        }
        
    }


    
    /// <summary>
    /// 게임 오버시 함수
    /// </summary>
    void Gameover()
    {
        GOScoreText.text = BattleManager.Instance.Score.ToString();
        GetGoldText.text = BattleManager.getGold.ToString();
        GameoverPanel.SetActive(true);
        UserDataManager.user.coin += BattleManager.getGold;
        BattleManager.getGold = 0;
        Debug.Log(BattleManager.getGold);
    }


    /// <summary>
    /// 경고 텍스트 뜨게하는함수
    /// </summary>
    /// <param name="index"></param>
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


    /// <summary>
    /// 쿨타임 오브젝트 생성
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cooltime"></param>
    public void Create_SkillCoolTimeObject(int Skey)
    {
        var obj = Instantiate(Skill_CoolTime_Prefab);
        obj.GetComponent<SkillCoolTimeObject>().skillKey = Skey;
        obj.gameObject.transform.parent = Content.transform;
        obj.gameObject.transform.localPosition = Vector2.zero;  
    }


    /// <summary>
    /// 경고 텍스트 코루틴
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    IEnumerator SetWarningText(string str)
    {

        WarningText.text = str;
        WarningText.gameObject.SetActive(true);

        WarningText.gameObject.GetComponent<Animator>().SetTrigger("Set");
        

        yield return new WaitForSeconds(0.9f);

        WarningText.gameObject.SetActive(false);
    }
}
