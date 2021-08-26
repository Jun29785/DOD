using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;


    [SerializeField]
    Image Loadingbar;

    
    public static void LoadScene(string name)
    {
        nextScene = name;
        SceneManager.LoadScene("LoadingScene");
        
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        yield return new WaitForSeconds(3);
        float timer = 0;
        while (!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                Loadingbar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                Loadingbar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(Loadingbar.fillAmount>= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}
