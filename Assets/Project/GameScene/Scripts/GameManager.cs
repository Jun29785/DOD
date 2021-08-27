using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int gold = 0;

    protected override void Awake()
    {
        base.Awake();
        ApplicationSetting();
    }


    void ApplicationSetting()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;

    }
    public void GetGold(int value)
    {
        gold += value;
    }

    public void SaveGameData()
    {
        SaveData save = new SaveData();
        save.coin = gold;

        SaveManager.Save(save);
    }

    public void LoadGameData()
    {
        SaveData save = SaveManager.Load();
        gold = save.coin;
    }

}

