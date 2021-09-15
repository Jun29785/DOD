using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillButton : MonoBehaviour
{
    [SerializeField]
    public bool isOpenSkill = false;
    [SerializeField]
    public int SKey;
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<int> Command;
    [SerializeField]
    public float Mana;
    [SerializeField]
    public float Dmg;
    [SerializeField]
    public string Description;
    [SerializeField]
    public float Ctime;
    [SerializeField]
    public int SkillLevel;
    [SerializeField]
    public int UpgradeCost;

    public GameObject Skill;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        //string nameKey = DataBaseManager.Instance.tdSkillDict[this.SKey].Name; ?? 

        this.Name = DataBaseManager.Instance.tdSkillDict[this.SKey].Name;
        this.Command = DataBaseManager.Instance.tdSkillDict[this.SKey].Command;
        this.Description = DataBaseManager.Instance.tdSkillDict[this.SKey].Description;
        this.Mana = DataBaseManager.Instance.tdSkillDict[this.SKey].Tmana;
        this.Dmg = DataBaseManager.Instance.tdSkillDict[this.SKey].Tdmg;
        this.SkillLevel = UserDataManager.user.skill_level[DataBaseManager.Instance.tdSkillDict[this.SKey].Name];
        this.Ctime = DataBaseManager.Instance.tdSkillDict[this.SKey].T_Ctime;
        if (this.SkillLevel < 1 && !isOpenSkill)
        {
            this.UpgradeCost = 1000;
        }
        else
        {
            this.UpgradeCost = DataBaseManager.Instance.tdSkillDict[this.SKey].UpgradeCost;
        }
        //Debug.Log("name : " + nameKey);
    }

    public void OnClickSkillButton()
    {
        LobbyUIManager.Instance.OpenSkillPanel(Skill);
    }
}
