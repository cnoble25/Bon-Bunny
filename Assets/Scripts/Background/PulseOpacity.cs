using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PulseOpacity : MonoBehaviour
{
    public float timeMultiplier;
    public float timeOffset;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        renderer.color = new Color(1, 1, 1, (Mathf.Sin(timeOffset + Time.timeSinceLevelLoad * timeMultiplier) + 1) / 2);
    }
}
