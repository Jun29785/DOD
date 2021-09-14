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
    public int SkillLevel;
    [SerializeField]
    public int UpgrateCost;

    public GameObject Skill;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        //string nameKey = DataBaseManager.Instance.tdSkillDict[this.SKey].Name; ?? 

        this.Name = DataBaseManager.Instance.tdSkillDict[this.SKey].Name;
        this.Command = DataBaseManager.Instance.tdSkillDict[this.SKey].Command;
        this.Description = DataBaseManager.Instance.tdSkillDict[this.SKey].Description;
        this.UpgrateCost = DataBaseManager.Instance.tdSkillDict[this.SKey].UpgradeCost;
        this.Mana = DataBaseManager.Instance.tdSkillDict[this.SKey].Tmana;
        this.Dmg = DataBaseManager.Instance.tdSkillDict[this.SKey].Tdmg;
        this.SkillLevel = UserDataManager.user.skill_level[DataBaseManager.Instance.tdSkillDict[this.SKey].Name];

        //Debug.Log("name : " + nameKey);

    }

private void Update()
    {
        if (SkillLevel < 1 && isOpenSkill)
        {
            UpgrateCost = 1000;
        }
    }

    public void OnClickSkillButton()
    {
        LobbyUIManager.Instance.OpenSkillPanel(Skill);
    }
}
