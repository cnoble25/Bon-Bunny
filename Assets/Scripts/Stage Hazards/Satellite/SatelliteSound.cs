using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSound : MonoBehaviour
{
   [SerializeField] float timer;

   private bool canBeep;

   [SerializeField] private FMODUnity.EventReference SatelliteBeep;

   private void Awake()
   {
      canBeep = true;
   }

   private void Update()
   {
      if (canBeep)
      {
         canBeep = false;
         StartCoroutine(PlaySound(timer));
         print("hi");
      }
   }

   IEnumerator PlaySound(float time)
   {
      FMODUnity.RuntimeManager.PlayOneShot(SatelliteBeep, transform.position);
      yield return new WaitForSeconds(time);
      canBeep = true;

   }
}
