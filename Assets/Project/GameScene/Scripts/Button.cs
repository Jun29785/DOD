using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.Define;
public class Button : MonoBehaviour
{

    


    
    public void ToLobbyScene()
    {
        SceneManager.LoadScene(Scenes.LobbyScene.ToString());
        Time.timeScale = 1;
    }

    public void ToGameScene()
    {
        LoadingSceneController.LoadScene(Scenes.GameScene.ToString());
        Time.timeScale = 1;

    }

    public void ToInventoryScene()
    {
        SceneManager.LoadScene(Scenes.InventoryScene.ToString());
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
