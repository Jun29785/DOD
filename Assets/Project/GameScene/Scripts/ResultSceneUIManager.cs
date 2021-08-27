using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneUIManager : UIManager
{
    [SerializeField]
    Text ScoreText;

    [SerializeField]
    Text GetGoldText;
    void Start()
    {
        GetGoldUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();

    }





    void ScoreUpdate()
    {
        ScoreText.text = "Score : " + Mathf.Lerp(0, BattleManager.Score, 3f);
    }

    void GetGoldUpdate()
    {
        SetText<float>(GetGoldText, BattleManager.getGold);
    }
}
