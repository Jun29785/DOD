using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DamageText : MonoBehaviour
{
    public float speed;
    public float alphaSpeed;
    public float DestroyTime;
    public TextMeshProUGUI text;
    Color alpha;
    void Start()
    {
        alpha = text.color; 
        Invoke("Destroys", DestroyTime);
    }

    private void OnEnable()
    {
        text.fontSize = 0.03f;
        alpha.a = 1;
        text.color = alpha; 
    }
    void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
        text.fontSize += 0.004f;

    }

    public void Destroys()
    {
        Objectpool.ReturnDamageText(this);
    }
}
