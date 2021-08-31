using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public static int getGold = 0;
    public static float Score = 0;

    public int purpose;
    public int BossInterval;


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

    void Awake()
    {
        purpose = 100;
        BossInterval = purpose;
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


}
