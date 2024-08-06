using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapLocking : MonoBehaviour
{

   public string sceneName;
   void Awake()
   {
      if (SaveGameData.instance.data.MapsCompleted.Contains(sceneName) || MainMenuManager.canAccess)
      {
         GetComponent<UnityEngine.UI.Image>().color = Color.white;
         GetComponent<UnityEngine.UI.Button>().enabled = true;

         
      }
      else
      {
         GetComponent<UnityEngine.UI.Image>().color = Color.red;
         GetComponent<UnityEngine.UI.Button>().enabled = false;
      }
   }

   private void Update()
   {
      if (SaveGameData.instance.data.MapsCompleted.Contains(sceneName) || MainMenuManager.canAccess)
      {
         GetComponent<UnityEngine.UI.Image>().color = Color.white;
         GetComponent<UnityEngine.UI.Button>().enabled = true;

         
      }
      else
      {
         GetComponent<UnityEngine.UI.Image>().color = Color.red;
         GetComponent<UnityEngine.UI.Button>().enabled = false;
      }
   }
}
