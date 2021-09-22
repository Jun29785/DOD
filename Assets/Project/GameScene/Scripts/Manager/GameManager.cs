using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.DB;
using DOD.Define;
public class GameManager : Singleton<GameManager>
{

    public TitleController titleController;
    public bool isnameInput;


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
        foreach (var item in DataBaseManager.Instance.tdSkillDict.Values)
        {
            var i = UserDataManager.user.skill_level[item.SKey.ToString()];

            item.Tdmg = item.Fdmg + (float)(item.Ldmg * (i - 1));
            item.Tmana = item.Fmana + (float)(item.Lmana * (i - 1));
            item.T_Ctime = item.Ctime - (float)(item.L_Ctime * (i - 1));
            item.TUC = item.UpgradeCost + (int)(item.LCU * (i - 1));
            if (item.T_Ctime < item.M_Ctime)
            {
                item.T_Ctime = item.M_Ctime;
            }
        }
    }

    public void CharSetting()
    {
        foreach (var item in DataBaseManager.Instance.tdCharacterDict.Values)
        {
            var i = UserDataManager.user.character_level[item.UnitNo.ToString()];
            item.THP = item.HP + (float)(item.Lhp * (i - 1));
            item.TMP = item.MP + (float)(item.Lmp * (i - 1));
            item.TPOWER = item.POWER + (float)(item.Lpower * (i - 1));
            item.TUC = item.UpgradeCost + (int)(item.LUC * (i - 1));
        }
    }

    private void GooglePlayVersionCheck()

    {
        ////////////////////

        UnsafeSecurityPolicy.Instate(); //이 코드를 안쓸 경우 오류가 뜨더라구요

        string marketVersion = "";

        string url = "https://play.google.com/store/apps/details?id=앱id";

        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);
        yield return doc;

        try
        {
            System.Collections.Generic.IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants("div").Where(d => d.Attributes["class"].Value.Contains("htlgb"));

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='htlgb']"))
            {
                string value = node.InnerText.Trim();



                /////////
                if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{1}\.\d{1}\.\d{1}$"))
                {

                    marketVersion = value;
                    Debug.Log("market version : " + marketVersion.Trim());




                    if (marketVersion.Split('.')[0] == Application.version.Split('.')[0] && marketVersion.Split('.')[1] == Application.version.Split('.')[1]) // . . 형식중에서 앞의 두 숫자가 바뀌는 경우에만 업데이트 요구 -> 업데이트를 강제할 필요없는 경우에는 맨뒤의 숫자만 바꾸자
                    {
                        Debug.Log("업데이트 필요없음");
                        //버전이 같은 경우 
                        IsSameVersion = true; //일단은 그냥 같은 버젼으로 취급해준다
                        IsChecked = true;
                        yield break; //함수 빠져나감 
                    }
                    else
                    {
                        Debug.Log("업데이트 필요함");
                        //버전이 다른 경우 
                        IsSameVersion = false;
                        IsChecked = true;
                        _DeveloperLogoScene.SetActiveRequestUpdateGameOnGooglePlayPanel(true);
                        yield break; //함수 빠져나감 
                    }

                }


            }




        }
        catch
        {

            //구글 플레이 쪽에서 코드를 자주 바꾸다 버전을 못가져 올 수 도 있습니다. 이런 경우를 대비하여 exception이 뜬 경우 그냥 버전이 동일한걸로 취급
            IsSameVersion = true; //일단은 그냥 같은 버젼으로 취급해준다
            IsChecked = true;


        }



    }


}

