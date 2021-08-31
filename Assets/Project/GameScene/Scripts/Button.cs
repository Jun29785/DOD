﻿using System.Collections;
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

    public void Viewit(GameObject viewObj)
    {
        viewObj.SetActive(true);
    }
    public void Hideit(GameObject viewObj)
    {
        viewObj.SetActive(false);
    }
    public void Pause(GameObject viewObject)
    {
        Time.timeScale = 0;
        viewObject.SetActive(true);
    }

    public void Pose(GameObject viewObject)
    {
        Time.timeScale = 1;
        viewObject.SetActive(false);
    }


    //겜씬

    public void gameSceneQuit(GameObject viewObject)
    {
        Time.timeScale = 1;
        viewObject.SetActive(false);
        BattleManager.Instance.isEnd = true;
    }


}
