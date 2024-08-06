using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSoundManager : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter Thrusting;
    private bool isThrusting;

    void Awake()
    {
        Thrusting = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    

    void Update()
    {
        if (PlayerInputManager.instance.Thrust.ReadValue<float>() > 0 && PlayerStateManager.instance.state == PlayerStateManager.PlayerState.Alive && Time.timeScale != 0) 
        {
            if (!isThrusting)
            {
                isThrusting = true;
                ResetSound(9.714f, isThrusting);
                Thrust();
            }
        }
        else
        {
            StopAllCoroutines();
            isThrusting = false;
            Thrusting.Stop();
        }
    }

    IEnumerator ResetSound(float TimeToWait, bool BoolToSet)
    {
        yield return new WaitForSeconds(TimeToWait);
        BoolToSet = false;
    }

    void Thrust()
    {
        Thrusting.Play();
    }
}
