using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    enum Scene
    {
        LobbyScene,
        GameScene,
        InventoryScene,
        LoadingScene
    }

    public void ToLobbyScene()
    {
        SceneManager.LoadScene(Scene.LobbyScene.ToString());
        Time.timeScale = 1;
    }

    public void ToGameScene()
    {
        LoadingSceneController.LoadScene(Scene.GameScene.ToString());
        Time.timeScale = 1;

    }

    public void ToInventoryScene()
    {
        SceneManager.LoadScene(Scene.InventoryScene.ToString());
        Time.timeScale = 1;
    }
}
