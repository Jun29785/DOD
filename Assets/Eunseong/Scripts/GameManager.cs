using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public void ToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void InGame()
    {
        SceneManager.LoadScene("EunSeong");
    }
}
