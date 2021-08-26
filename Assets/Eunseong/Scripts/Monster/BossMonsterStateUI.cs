using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonsterStateUI : MonoBehaviour
{

    public Slider Hp;
    private void Awake()
    {
    }
    void Start()
    {

    }

    private void OnEnable()
    {

        Hp.value = 0;
        StartCoroutine(appear());
    }
    void Update()
    {

    }

    IEnumerator appear()
    {
        while (Hp.value <= 1f)
        {
            yield return null;
            Hp.value = Mathf.Lerp(Hp.value, 1, 3 * Time.deltaTime);
        }
    }

    public void SetHp(float hp, float maxhp)
    {
        Hp.value = Mathf.Lerp(Hp.value, hp / maxhp, 3 * Time.deltaTime);
    }
}
