using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.DB;
using DOD.Define;
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
    
    public void StatSetting()
    {
        foreach (var item in DataBaseManager.Instance.tdSkillDict)
        {
            item.Value.Fdmg += (int)(item.Value.Ldmg * (UserDataManager.user.skill_level[item.Value.Name]-1));
            item.Value.Fmana += (int)(item.Value.Lmana * (UserDataManager.user.skill_level[item.Value.Name]-1));
        }
    }

    
    
}

