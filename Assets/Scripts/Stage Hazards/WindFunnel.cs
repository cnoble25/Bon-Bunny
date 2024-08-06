using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFunnel : MonoBehaviour
{
   [SerializeField] private float Magnitude;
   private void OnTriggerStay2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         float angle = transform.rotation.eulerAngles.z + 180f;
         Vector2 direction = new Vector2(Mathf.Cos(angle*Mathf.PI/180), Mathf.Sin(angle*Mathf.PI/180));
         other.GetComponent<Rigidbody2D>().AddForce(direction*Magnitude);
      }
   }
}
