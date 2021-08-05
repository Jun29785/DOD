using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    enum Scene
    {
        LobbyScene,
        GameScene,
    }
    public void ToLobbyScene()
    {
        SceneManager.LoadScene(Scene.LobbyScene.ToString());
        Time.timeScale = 1;
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene(Scene.GameScene.ToString());
        Time.timeScale = 1;

    }
}
