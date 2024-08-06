using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehavior : MonoBehaviour
{
    [SerializeField] float speed;

    private SpriteRenderer sr;

    void Awake()
    {
        sr =  GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = transform.localScale /speed;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a/speed);
        if(transform.localScale.x < 0.1f) Destroy(gameObject);
    }
}
