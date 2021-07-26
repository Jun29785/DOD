using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    Rigidbody2D rigid;
    float pos;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        pos = Random.Range(-0.1f, 0.4f);
    }
        
    private void Start()
    {
        rigid.AddForce(new Vector2(Random.Range(20, 120), Random.Range(100, 200)));
    }
    void Update()
    {
       
    }
}
