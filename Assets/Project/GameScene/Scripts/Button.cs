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
        Debug.Log(BattleManager.getGold);
        UserDataManager.user.Coin += BattleManager.getGold;
    }

    public void ToGameScene()
    {
        LoadingSceneController.LoadScene(Scenes.GameScene.ToString());
        Time.timeScale = 1;
        Debug.Log(BattleManager.getGold);
        UserDataManager.user.Coin += BattleManager.getGold;

    }

    // 로비
    public void OpenInventory()
    {
        LobbyUIManager.Instance.Inventory.SetActive(true);
        Debug.Log("Loading Skill Button");
        LobbyUIManager.Instance.CreateButton();
        Debug.Log("Successful Load Skill Button");
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
