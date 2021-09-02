using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public Text loadingText;
    public GameObject tapTostart;
    public GameObject loadingBar;
    public Image loadingBarGauge;

    public void SetLoadStateDescription(string loadState)
    {
        loadingText.text = $"Load {loadState}...";
    }
        
    public IEnumerator LoadGaugeUpdate(float loadPer)
    {
        while (!Mathf.Approximately(loadingBarGauge.fillAmount, loadPer))
        {
            loadingBarGauge.fillAmount = Mathf.Lerp(loadingBarGauge.fillAmount, loadPer, Time.deltaTime * 2f);

            yield return null;
        }
    }


    public IEnumerator setComplete()
    {

        yield return new WaitForSeconds(0.7f);
        loadingBar.SetActive(false);
        tapTostart.SetActive(true);

    }

}
