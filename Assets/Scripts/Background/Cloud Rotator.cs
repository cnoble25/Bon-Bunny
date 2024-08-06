using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotator : MonoBehaviour
{
    public float angularVelocity;
    public float waviness;
    public float waveSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Time.time * angularVelocity - waviness * Mathf.Cos(Time.time * waveSpeed));
    }
}
