using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;


    [SerializeField]
    Slider Loadingbar;

    public AudioClip BgSound;
    public static void LoadScene(string name)
    {
        nextScene = name;
        UserDataManager.Instance.Save();
        SceneManager.LoadScene("LoadingScene");
        
    }

    public static void TitletoLoadScene(string name)
    {
        nextScene = name;
        SceneManager.LoadScene("LoadingScene");

    }

    private void Awake()
    {
        SoundManager.Instance.BgSound(BgSound);
    }
    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0;

        while (!op.isDone)
        {
            yield return null;


            if(op.progress < 0.9f)
            {
                Loadingbar.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                Loadingbar.value = Mathf.Lerp(0.9f, 1f, timer);
                if(Loadingbar.value>= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}
