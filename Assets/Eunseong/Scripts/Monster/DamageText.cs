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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;


    }

    public void Destroys()
    {
        Objectpool.ReturnDamageText(this);
    }
}
