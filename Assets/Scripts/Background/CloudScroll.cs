using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScroll : MonoBehaviour
{
    [Tooltip("The x-velocity (units/sec) that the clouds scroll at.")]
    public float scrollSpeed = 0.5f;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float width = sprite.size.x * gameObject.transform.localScale.x;
        
        Vector3 pos = gameObject.transform.localPosition;
        pos.x += scrollSpeed * Time.deltaTime;

        if (pos.x > width || pos.x < -width) {
            pos.x = -pos.x;
            pos.y = Camera.main.orthographicSize * Random.Range(-0.6f, 0.6f);
        }

        gameObject.transform.localPosition = pos;
    }
}
