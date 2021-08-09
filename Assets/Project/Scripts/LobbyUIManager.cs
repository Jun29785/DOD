using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public Image StartButton;
    public Sprite OnStartButton;
    public Sprite OffStartButton;

    public void PointerDownStartButton()
    {
        StartButton.sprite = OnStartButton;
    }

    public void PointerUpStartButton()
    {
        StartButton.sprite = OffStartButton;
    }
}
