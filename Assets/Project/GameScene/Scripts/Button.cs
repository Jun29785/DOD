using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.Define;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

    public GameObject inputName;
    public Text overlapName;
    /// <summary>
    /// 로비로
    /// </summary>
    public void ToLobbyScene()
    {
        LoadingSceneController.LoadScene(Scenes.LobbyScene.ToString());
        Time.timeScale = 1;
   }

    public void TitleToLobby()
    {
        LoadingSceneController.LoadScene(Scenes.LobbyScene.ToString());
        Time.timeScale = 1;
    }


    /// <summary>
    /// 게임씬으로
    /// </summary>
    public void ToGameScene()
    {
        LoadingSceneController.LoadScene(Scenes.GameScene.ToString());
        GameManager.Instance.StatSetting();

        Time.timeScale = 1;
        UserDataManager.Instance.Save();

    }

    // 로비
    public void OpenInventory()
    {
        var i = LobbyUIManager.Instance;
        i.Inventory.SetActive(true);
        GameManager.Instance.CharSetting();
        GameManager.Instance.StatSetting();
        i.CreateButton();
        i.InvenCharacter.GetComponent<InvenChar>().LoadData(i.InvenCharKey);
    }

    public void CloseInventory()
    {
        LobbyUIManager.Instance.Inventory.SetActive(false);
        Debug.Log("Close Inventory Panel");
        GameManager.Instance.StatSetting();

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

    public void enter_nickName(Text text)
    {
        if (text.text != "")
        {
            UserDataManager.user.nickname = text.text;
            Debug.Log(UserDataManager.user.nickname);
            UserDataManager.Instance.Sendnick();
            Debug.Log("닉넴 입력");
            Invoke("nickNameCheck", 1f);
        }
        Debug.Log(UserDataManager.user.nickname);
    }

    public void nickNameCheck()
    {
        if (UserDataManager.Instance.nickExist)
        {
            Debug.Log("닉네임 중복");
            StartCoroutine(OverlapName());
        }
        else
        {
            inputName.SetActive(false);
            GameManager.Instance.isnameInput = true;
            UserDataManager.Instance.Init();

        }
    }
    
    IEnumerator OverlapName()
    {
        overlapName.gameObject.SetActive(true);
        while (true) {
            yield return null;
            overlapName.color = new Color(overlapName.color.r, overlapName.color.g, overlapName.color.b, overlapName.color.a - 0.01f);
            if (overlapName.color.a <= 0)
            {
                overlapName.gameObject.SetActive(false);
                overlapName.color = overlapName.color = new Color(overlapName.color.r, overlapName.color.g, overlapName.color.b, 1);
                break;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
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
    