using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.Define;
public class Button : MonoBehaviour
{


    /// <summary>
    /// 로비로
    /// </summary>
    public void ToLobbyScene()
    {
        SceneManager.LoadScene(Scenes.LobbyScene.ToString());
        Time.timeScale = 1;
        Debug.Log(BattleManager.getGold);
        UserDataManager.user.Coin += BattleManager.getGold;
        UserDataManager.Instance.Save();
    }


    /// <summary>
    /// 게임씬으로
    /// </summary>
    public void ToGameScene()
    {
        LoadingSceneController.LoadScene(Scenes.GameScene.ToString());
        Time.timeScale = 1;
        Debug.Log(BattleManager.getGold);
        UserDataManager.user.Coin += BattleManager.getGold;
        UserDataManager.Instance.Save();
    }

    // 로비
    public void OpenInventory()
    {
        LobbyUIManager.Instance.Inventory.SetActive(true);
        Debug.Log("Loading Skill Button");
        LobbyUIManager.Instance.CreateButton();
        Debug.Log("Successful Load Skill Button");
    }

    public void CloseInventory()
    {
        LobbyUIManager.Instance.Inventory.SetActive(false);
        Debug.Log("Close Inventory Panel");
    }

    /// <summary>
    /// 오브젝트를 보여주는함수
    /// </summary>
    /// <param name="viewObj">보여줄 오브젝트</param>
    public void Viewit(GameObject viewObj)
    {
        viewObj.SetActive(true);
    }
    /// <summary>
    /// 오브젝트를 끄는함수
    /// </summary>
    /// <param name="viewObj">끌 오브젝트</param>
    public void Hideit(GameObject viewObj)
    {
        viewObj.SetActive(false);
    }


    //겜씬


    /// <summary>
    /// 게임씬에서 멈출 때 사용
    /// </summary>
    /// <param name="viewObject">pausePanel</param>
    public void Pause(GameObject viewObject)
    {
        Time.timeScale = 0;
        viewObject.SetActive(true);
    }


    /// <summary>
    /// 게임씬에서 재시작할때 사용
    /// </summary>
    /// <param name="viewObject">pausePanel</param>
    public void Pose(GameObject viewObject)
    {
        Time.timeScale = 1;
        viewObject.SetActive(false);
    }


    /// <summary>
    /// 게임씬을 중간에 나갈때 사용
    /// </summary>
    /// <param name="viewObject">pasuePanel</param>
    public void gameSceneQuit(GameObject viewObject)
    {
        Time.timeScale = 1;
        viewObject.SetActive(false);
        BattleManager.Instance.isEnd = true;
        UserDataManager.Instance.Save();
    }


}
