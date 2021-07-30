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
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene(Scene.GameScene.ToString());

    }
}
