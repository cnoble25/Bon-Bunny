using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSway : MonoBehaviour
{
    private const float TwoPi = Mathf.PI * 2;

    public float radiusY = 0.5f;
    public float radiusX = 1f;
    public float angle = Mathf.PI / 2; // in radians
    public float angularVelocity = Mathf.PI / 3;

    private float centerX;
    private float centerY;

    // Start is called before the first frame update
    void Start()
    {
        centerX = gameObject.transform.position.x - Mathf.Sin(angle) * radiusX;
        centerY = gameObject.transform.position.y - Mathf.Cos(angle) * radiusY;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(
            centerX + Mathf.Sin(angle) * radiusX,
            centerY + Mathf.Cos(angle) * radiusY);
        angle += angularVelocity * Time.deltaTime;
        
        if (angle >= TwoPi) angle -= TwoPi;
    }
}
