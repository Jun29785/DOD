using DOD.DB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
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
    public Image SkillIcon;

    public GameObject SkillPanel;
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

    void CreateButton()
    {
        foreach (var i in DataBaseManager.Instance.tdSkillDict)
        {
            Debug.Log("In");
            GameObject Create = (GameObject)Instantiate(SkillButtonObj);
            Create.transform.parent = SkillObjParent;
            Create.transform.localScale = new Vector3(1, 1, 1);
            Create.GetComponent<SkillButton>().SetButton(i.Value.SKey);
            Debug.Log("Create : " + Create.name);
            SkillIcon.sprite = Resources.Load<Sprite>("SkillIcons/" + i.Value.SKey);
            Debug.Log("Load Sprite Successful!");
        }
    }

    public void OnClickSkillButton()
    {
        SkillPanel.SetActive(true);
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
