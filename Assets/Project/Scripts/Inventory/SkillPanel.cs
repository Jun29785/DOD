using DOD.DB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Text Name;
    public Image SkillIcon;
    public Text SkillLevel;
    public Text Description;

    public Text ATKText;
    public Text ManaText;
    public Text CTimeText;
    public Text CommandText;
    public Text DescriptionText;
    public Text UpgradeCostText; // 업그레이드 비용 계산 필요 (고정값 * 스킬 레벨)

    public void LoadSkillData()
    {
        var i = SkillManager.Instance;
        string cmd = "";
        foreach (int j in i.Command)
        {
            cmd += j+1+" ";
        }
        Name.text = i.Name;
        if (i.SkillLevel < 1)
            SkillIcon.sprite = Resources.Load<Sprite>("SkillIcon/" + "10000") as Sprite;
        else
        {
            SkillIcon.sprite = Resources.Load<Sprite>("SkillIcon/" + i.SKey) as Sprite;
            SkillLevel.text = i.SkillLevel.ToString("Lv : 0");
            Description.text = i.Description;
            PlayerPrefs.SetInt(i.SKey + "SkillLevel", i.SkillLevel);
        }
        ATKText.text = i.Dmg.ToString();
        ManaText.text = i.Mana.ToString();
        CommandText.text = cmd;
        CTimeText.text = i.Ctime.ToString("00"+"초");

        DescriptionText.text = i.Description;
        UpgradeCostText.text = i.UpgrateCost.ToString();
    }
}
