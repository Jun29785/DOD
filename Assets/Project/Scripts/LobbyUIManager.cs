﻿using DOD.DB;
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
    public GameObject rankPanel;
    public GameObject nameInput;
    public GameObject quitPanel;


    public GameObject SkillStatPanel;
    public GameObject SkillDescPanel;
    public GameObject SkillStatButton;
    public GameObject SkillDescButton;
    
    
    private GameObject CurrentSelectedSkill;

    bool isQuitPanel = false;

    protected override void Awake()
    {
        StartCoroutine(StartScene());
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
            if (!GameManager.Instance.isnameInput)
            {
                nameInput.SetActive(true);
            }
        }
        
    }
    private void Update()
    {
        UpdateText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (rankPanel.activeSelf)
            {
                rankPanel.SetActive(false);
            }
            else if (Inventory.activeSelf)
            {
                if (SkillPanel.activeSelf)
                {
                    SkillPanel.SetActive(false);

                }
                else
                {
                    Inventory.SetActive(false);
                }
            }
            else
            {
                isQuitPanel = !isQuitPanel;
                quitPanel.SetActive(isQuitPanel);
            }
        }
    }

    #region 스타트 버튼 애니메이션
    public void PointerDownStartButton()
    {
        StartButton.sprite = OnStartButton;
    }

    public void PointerUpStartButton()
    {
        StartButton.sprite = OffStartButton;
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
        SkillDescButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }


    #endregion
    public void UpdateText()
    {
        Coin.text = UserDataManager.user.coin.ToString();
    }


    #region 인벤 버튼 애니메이션
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


    #endregion
 

    #region 스킬 판넬
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
        Get.UpgrateCost = Set.UpgrateCost;
        Get.SkillLevel = Set.SkillLevel;
        SkillPanel.GetComponent<SkillPanel>().LoadSkillData();
        GameManager.Instance.StatSetting();
        SkillStatPanel.SetActive(true);
    }

    public void CloseSkillPanel()
    {
        SkillPanel.SetActive(false);
        GameManager.Instance.StatSetting();
        CreateButton();
    }
    public void OnClickExitPanelButton()
    {
        SkillPanel.SetActive(false);
    }
    #endregion


   public void CreateButton()
    {
        for (int i = 0; i < SkillObjParent.childCount; i++)
        {
            Destroy(SkillObjParent.GetChild(i).gameObject);
        }

        foreach (var i in DataBaseManager.Instance.tdSkillDict.Values)
        {
            Debug.Log("In");
            GameObject Create = (GameObject)Instantiate(SkillButtonObj);
            Create.transform.parent = SkillObjParent;
            Create.transform.localScale = new Vector3(1, 1, 1);
            var j = Create.GetComponent<SkillButton>();
            // 스킬 값 추가
            j.SetButton(i.SKey);
            
            Debug.Log("Get Static Data");
            //j.SkillLevel = UserDataManager.user.skill_level[DataBaseManager.Instance.tdSkillDict[(int)i.Value.SKey].Name];
            Debug.Log("Create : " + Create.name);
            Create.name = i.Name;
            if (j.SkillLevel < 1)
                Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + "10000") as Sprite;
            else
                Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + i.SKey) as Sprite;
            Debug.Log("Load Sprite Successful!");

        }
    }
    public void OnClickUpgradeButton()
    {
        if (!CurrentSelectedSkill.GetComponent<SkillButton>().isOpenSkill)
        {
            CurrentSelectedSkill.GetComponent<SkillButton>().isOpenSkill = true;
        }
        CurrentSelectedSkill.GetComponent<SkillButton>().SkillLevel += 1;
        Debug.Log(UserDataManager.user.skill_level[CurrentSelectedSkill.GetComponent<SkillButton>().Name]);
        UserDataManager.user.skill_level[CurrentSelectedSkill.GetComponent<SkillButton>().Name] += 1;
        GameManager.Instance.StatSetting(); 
        OpenSkillPanel(CurrentSelectedSkill);
        UserDataManager.Instance.Save();
    }

    
}
