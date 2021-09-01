using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public enum Stage 
    {
        forest,
        GoblinViliage,
    }

    public Stage currentmap;
    public static BattleManager Instance;
    public int getGold = 0;
    public float Score = 0;

    public int purpose;
    public int BossInterval;
    public int loopCount;

    public bool isBoss;
    public bool isContact = false;
    public bool isUseSkill = false;
    public bool isStop = false;
    public bool isEnd = false;
    public bool PatternInputEnd = false;
    public bool isStart;

    public bool isDash;
    public List<int> Pattern_id = new List<int>();

    public Character character;
    public GameObject Panel;
    public GameObject gameoverPanel;
    public MonsterGenerater monsterGenerater;

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
        character.Damaged(value);
    }

    public void GetGold(int value)
    {
        getGold += value;
    }

    public void StartPanel()
    {
        BattleManager.Instance.isEnd = false;
        character.Hp = character.MaxHp;
        character.Mp = character.MaxMp;
        isStart = false;
        purpose = 20;
        BossInterval = purpose;
        Pattern_id.Clear();
        Score = 0;
        getGold = 0;
        StartCoroutine(StartLerp());
        Panel.GetComponent<Animator>().SetTrigger("isStart");
        isStart = true;
        gameoverPanel.SetActive(false);

    }


    public IEnumerator StartLerp()
    {
        while (character.transform.position.x <= -1.81)
        {
            character.transform.position = Vector2.Lerp(character.transform.position, new Vector2(-1.81f, character.transform.position.y), 4 * Time.deltaTime);
            yield return null;
        }

        character.transform.position = new Vector2(-1.81f, character.transform.position.y);
    }

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

    public void SetmonsterGenInterval(float value)
    {
        monsterGenerater.SpawnInterval = value;
        monsterGenerater.currentTime = 0;
    }
}
