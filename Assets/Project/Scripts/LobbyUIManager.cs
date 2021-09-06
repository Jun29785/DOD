using DOD.DB;
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


    private void Awake()
    {
        StartCoroutine("StartScene");
    }

    IEnumerator StartScene()
    {
        Canvas.SetTrigger("LeaveOpen");
        yield return new WaitForSeconds(1.5f);
        Leave.SetActive(false);
        Character.SetBool("IDLE", true);
        
    }

    private void Update()
    {
        UpdateText();    
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
        Coin.text = UserDataManager.user.Coin.ToString();
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

    public void PointerClickInventoryButton()
    {
        CreateButton();
    }

    public void CreateButton()
    {
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
            
            Debug.Log("Create : " + Create.name);
            Create.name = i.Value.Name;
            Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + i.Value.SKey) as Sprite;
            Debug.Log("Load Sprite Successful!");
        }
    }

    public void OnClickSkillButton(GameObject Skill)
    {
        SkillPanel.SetActive(true);

        var Get = SkillManager.Instance;
        var Set = Skill.GetComponent<SkillButton>();
        Get.name = Set.Name;
        Get.SKey = Set.SKey;
        Get.Command = Set.Command;
        Get.Mana = Set.Mana;
        Get.Dmg = Set.Dmg;
        Get.Description = Set.Description;

    }

    public void OnClickUpgradeButton()
    {
        SkillManager.Instance.SkillLevel += 1;
    }

    public void OnClickExitPanelButton()
    {
        SkillPanel.SetActive(false);
    }
}
