using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AuroraFade : MonoBehaviour
{
    public Material auroraFade;
    public float frameDuration;

    private void Update()
    {
        float halfLoop = 2 * frameDuration;
        float fullLoop = halfLoop * 2;
        float loopPosition = Time.timeSinceLevelLoad % fullLoop;

        if (loopPosition > halfLoop) loopPosition = fullLoop - loopPosition;

        if (loopPosition > frameDuration)
        {
            auroraFade.SetFloat("_SecondStrength", 1);
            auroraFade.SetFloat("_ThirdStrength", (loopPosition - frameDuration) / frameDuration);
        } else
        {
            auroraFade.SetFloat("_SecondStrength", loopPosition / frameDuration);
            auroraFade.SetFloat("_ThirdStrength", 0);
        }
    }
}
