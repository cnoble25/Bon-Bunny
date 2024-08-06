using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartRealGame : MonoBehaviour
{
   public static StartRealGame instance;

   public GameObject player, playerStateManager, UIManager, GameDataManager, InputManager, otherScreen, buttons;
   
   void Awake()
   {
      if(instance == null) instance = this;
   }
   public void stuff()
   {
      player.SetActive(true);
      playerStateManager.SetActive(true);
      UIManager.SetActive(true);
      GameDataManager.SetActive(true);
      InputManager.SetActive(true);
      gameObject.SetActive(false);
   }


   public void switchtoother()
   {
      otherScreen.SetActive(true);
      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(buttons);
      gameObject.SetActive(false);
      
   }
}
