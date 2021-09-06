using DOD.DB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    /*List<int> command = ;
    int SKey = */
    public Text Name;
    public Image SkillIcon;
    public Text SkillLevel;
    public Text Description;

    public void LoadSkillData()
    {
        var i = SkillManager.Instance;
        Name.text = i.Name;
        SkillIcon.sprite = Resources.Load<Sprite>("SkillIcon/" + i.SKey) as Sprite;
        SkillLevel.text = i.SkillLevel.ToString("Lv : 0");
        Description.text = i.Description;
        PlayerPrefs.SetInt(i.SKey + "SkillLevel", i.SkillLevel);
    }
}
