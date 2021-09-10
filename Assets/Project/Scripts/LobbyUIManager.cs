using DOD.DB;
using DOD.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    public Image StartButton;
    public Sprite OnStartButton;
    public Sprite OffStartButton;

    public Text Coin;

    public Animator InventoryAnim;
    public GameObject InventoryIcon;

    public Animator Canvas;
    public GameObject Leave;
    public Animator Character;

    public GameObject SkillButtonObj;
    public Transform SkillObjParent;

    public GameObject SkillPanel;
    public GameObject Inventory;

    public GameObject SkillStatPanel;
    public GameObject SkillDescPanel;
    public GameObject SkillStatButton;
    public GameObject SkillDescButton;
    
    private GameObject CurrentSelectedSkill;


    public GameObject nameInput;
    public GameObject quitPanel;
    public GameObject rankPanel;
    public GameObject invenPanel;
    bool isQuitPanel;
    protected override void Awake()
    {
        StartCoroutine("StartScene");
    }

    IEnumerator StartScene()
    {
        Canvas.SetTrigger("LeaveOpen");
        yield return new WaitForSeconds(1.5f);
        Leave.SetActive(false);
        Character.SetBool("IDLE", true);

        nickNameInput();
    }


    void nickNameInput()
    {
        if (!UserDataManager.Instance.isexist)
        {
            nameInput.SetActive(true);
        }
        
    }
    private void Update()
    {
        UpdateText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!rankPanel.activeSelf)
            {
                if (!invenPanel.activeSelf)
                {
                    isQuitPanel = !isQuitPanel;
                    quitPanel.SetActive(isQuitPanel);
                }
                else
                {
                    invenPanel.SetActive(false);
                }
            }
            else
            {
                rankPanel.SetActive(false);
            }
        }
    }

    public void PointerDownStartButton()
    {
        StartButton.sprite = OnStartButton;
    }

    public void PointerUpStartButton()
    {
        StartButton.sprite = OffStartButton;
    }

    public void UpdateText()
    {
        Coin.text = UserDataManager.user.coin.ToString();
    }

    public void PointerDownInventoryButton()
    {
        InventoryAnim.SetTrigger("down");
        InventoryAnim.SetBool("IsClick", true);
        InventoryIcon.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
    }

    public void PointerUpInventoryButton()
    {
        InventoryAnim.SetTrigger("up");
        InventoryAnim.SetBool("IsClick", false);
        InventoryIcon.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void CreateButton()
    {
        for (int i = 0; i < SkillObjParent.childCount; i++)
        {
            Destroy(SkillObjParent.GetChild(i).gameObject);
        }

        foreach (var i in DataBaseManager.Instance.tdSkillDict)
        {
            Debug.Log("In");
            GameObject Create = (GameObject)Instantiate(SkillButtonObj);
            Create.transform.parent = SkillObjParent;
            Create.transform.localScale = new Vector3(1, 1, 1);
            var j = Create.GetComponent<SkillButton>();
            // 스킬 값 추가
            j.SetButton(i.Value.SKey);
            j.SKey = i.Value.SKey;
            j.Name = i.Value.Name;
            j.Command = i.Value.Command;
            j.Mana = i.Value.Fmana + (i.Value.Lmana * j.SkillLevel);
            j.Dmg = i.Value.Fdmg + (i.Value.Ldmg * j.SkillLevel);
            j.Description = i.Value.Description;
            Debug.Log("Get Static Data");
            j.SkillLevel = UserDataManager.user.skill_level[DataBaseManager.Instance.tdSkillDict[(int)i.Value.SKey].Name];
            Debug.Log("Create : " + Create.name);
            Create.name = i.Value.Name;
            if (j.SkillLevel < 1)
                Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + "10000") as Sprite;
            else
                Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + i.Value.SKey) as Sprite;
            Debug.Log("Load Sprite Successful!");
        }
    }

    public void OpenSkillPanel(GameObject Skill)
    {
        CurrentSelectedSkill = Skill;
        SkillPanel.SetActive(true);

        var Get = SkillManager.Instance;
        var Set = Skill.GetComponent<SkillButton>();
        Get.Name = Set.Name;
        Get.SKey = Set.SKey;
        Get.Command = Set.Command;
        Get.Mana = Set.Mana;
        Get.Dmg = Set.Dmg;
        Get.Description = Set.Description;
        Get.SkillLevel = Set.SkillLevel;
        SkillPanel.GetComponent<SkillPanel>().LoadSkillData();
        
        SkillStatPanel.SetActive(true);
    }

    public void CloseSkillPanel()
    {
        SkillPanel.SetActive(false);
    }

    public void OnClickSkillStatButton()
    {
        SkillDescPanel.SetActive(false);
        SkillStatPanel.SetActive(true);
        SkillDescButton.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        SkillStatButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnClickSkillDescButton()
    {
        SkillStatPanel.SetActive(false);
        SkillDescPanel.SetActive(true);
        SkillStatButton.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        SkillDescButton.GetComponent<Image>().color = new Color32(255, 255, 255,255);
    }

    public void OnClickUpgradeButton()
    {
        CurrentSelectedSkill.GetComponent<SkillButton>().SkillLevel += 1;
        UserDataManager.user.skill_level[CurrentSelectedSkill.GetComponent<SkillButton>().Name] += 1;
        GameManager.Instance.StatSetting(); 
        OpenSkillPanel(CurrentSelectedSkill);
        UserDataManager.Instance.Save();
    }

    public void OnClickExitPanelButton()
    {
        SkillPanel.SetActive(false);
    }
}
