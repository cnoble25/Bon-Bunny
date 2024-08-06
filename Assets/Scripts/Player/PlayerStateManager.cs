using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState state;
    public enum PlayerState
    {
        Alive,
        Dead,
        Win,
        Paused
    };
    public static PlayerStateManager instance;

    public GameObject player;

    private PlayerMovement playerMovement;

    void Awake()
    {
        if(instance == null) instance = this;
        UpdateCurrentState(PlayerState.Alive);
    }

    public void UpdateCurrentState(PlayerState newState)
    {
        state = newState;
        switch(state)
        {
            case PlayerState.Alive:
               player.GetComponent<PlayerCollisionManager>().enabled = true;
                if(PlayerInputManager.instance != null) PlayerInputManager.instance.enabled = true;
                break;
            case PlayerState.Dead:
                player.GetComponent<PlayerMovement>().anim.SetTrigger("Dead");
                player.GetComponent<PlayerCollisionManager>().enabled = false;
                if(PlayerInputManager.instance != null) PlayerInputManager.instance.enabled = false;
                if(player.TryGetComponent<PlayerDeath>(out PlayerDeath playerDeath))
                {
                    playerDeath.enabled = true;
                }
                break;
            case PlayerState.Paused:
                player.GetComponent<PlayerCollisionManager>().enabled = false;
                if(PlayerInputManager.instance != null) PlayerInputManager.instance.enabled = true;
                break;
            case PlayerState.Win:
                player.GetComponent<PlayerCollisionManager>().enabled = false;
                if(PlayerInputManager.instance != null) PlayerInputManager.instance.enabled = false;
                if(player.TryGetComponent<PlayerWin>(out PlayerWin playerWin))
                {
                    playerWin.enabled = true;
                }
                break;
            default:
                player.GetComponent<PlayerCollisionManager>().enabled = false;
                if(PlayerInputManager.instance != null) PlayerInputManager.instance.enabled = true;
                break;
        }
    }


}
