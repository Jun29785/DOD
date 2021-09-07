using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public TitleController titleController;
   
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    

    private void Start()
    {
        titleController.Initialize();
    }

    /// <summary>
    /// 앱세팅
    /// </summary>
    public void ApplicationSetting()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
    }
    

/*    public void SaveGameData()
    {
        UserData save = new UserData();
        save.Coin = gold;

        SaveManager.Save(save);
    }

    public void LoadGameData()
    {
        UserData save = SaveManager.Load();
        gold = save.Coin;
    }*/

}

