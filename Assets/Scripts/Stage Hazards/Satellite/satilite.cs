using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satilite : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float rotateSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 EU = transform.rotation.eulerAngles;

        EU.z += rotateSpeed;
        
        transform.rotation = Quaternion.Euler(EU);
    }
}
