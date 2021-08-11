﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int gold = 0;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(720, 1280, true);
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
