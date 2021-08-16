﻿using System.Collections;
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
        Coin.text = GameManager.Instance.gold.ToString("0");
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
}
