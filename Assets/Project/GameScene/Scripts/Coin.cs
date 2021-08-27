using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject goldPanel;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    

    private void OnEnable()
    {
        rigid.AddForce(new Vector2(Random.Range(20, 120), Random.Range(100, 200)));

        Invoke("Coindestroy", 0.6f);
    }
    void Coindestroy()
    {
        Objectpool.ReturnCoin(this);
    }

    /*IEnumerator CoinCroutine()
    {
        rigid.AddForce(new Vector2(Random.Range(20, 120), Random.Range(100, 200)));
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        while(false)
        {
            transform.position = Vector2.Lerp(transform.position, goldPanel.transform.position, 3 * Time.deltaTime);
            
        }

    }*/
}
