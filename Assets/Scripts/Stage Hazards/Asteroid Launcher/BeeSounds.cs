using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance beeSound;
    void Awake()
    {
        beeSound =  FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Buzz");
        beeSound.setVolume(0.1f);
        beeSound.start();
    }
}
