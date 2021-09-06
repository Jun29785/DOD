using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillButton : MonoBehaviour
{
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

    public GameObject Skill;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        string nameKey = DataBaseManager.Instance.tdSkillDict[this.SKey].Name;

        Debug.Log("name : " + nameKey);

    }    

    public void OnClickSkillButton()
    {
        LobbyUIManager.Instance.OpenSkillPanel(Skill);
    }
}
