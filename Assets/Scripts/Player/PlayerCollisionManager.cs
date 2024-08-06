using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference bounceSound;
    void Awake()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 3) FMODUnity.RuntimeManager.PlayOneShot(bounceSound, transform.position);
        if(other.gameObject.CompareTag("DeathZone")) PlayerStateManager.instance.UpdateCurrentState(PlayerStateManager.PlayerState.Dead);
        
        if(other.gameObject.CompareTag("WinZone")) PlayerStateManager.instance.UpdateCurrentState(PlayerStateManager.PlayerState.Win);
    }

    void OnCollisionExit2D(Collision2D other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("DeathZone")) PlayerStateManager.instance.UpdateCurrentState(PlayerStateManager.PlayerState.Dead);

        if(other.gameObject.CompareTag("WinZone")) PlayerStateManager.instance.UpdateCurrentState(PlayerStateManager.PlayerState.Win);
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
