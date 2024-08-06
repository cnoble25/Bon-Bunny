using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenFade : MonoBehaviour
{
    public float pauseTime = 10;
    public float transitionTime = 5;
    public SpriteRenderer lowerLayer;
    public SpriteRenderer upperLayer;
    public Sprite[] frames;

    private float transitionStart = 0f;
    private int frame = 0;
    private int nextFrame = 1;
    private int direction = 1;
    private bool stageA = true;
    private bool paused = true;

    // Start is called before the first frame update
    void Start()
    {
        lowerLayer.sprite = frames[0];
        upperLayer.sprite = frames[1];
        upperLayer.color = Color.clear;

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            // Stage A: Lower Layer: 100%
            //          Upper Layer: 0% -> 100%
            lowerLayer.color = Color.white;
            upperLayer.color = Color.clear;
            paused = true;
            yield return new WaitForSeconds(pauseTime);
            paused = false;
            transitionStart = Time.time;
            stageA = true;
            yield return new WaitForSeconds(transitionTime);

            // Stage B: Lower Layer: 100% -> 0%
            //          Upper Layer: 100%
            upperLayer.color = Color.white;
            lowerLayer.color = Color.white;
            paused = true;
            yield return new WaitForSeconds(pauseTime);
            paused = false;
            transitionStart = Time.time;
            stageA = false;
            yield return new WaitForSeconds(transitionTime);

            // Stage C: Swap Lower & Upper
            NextFrame();
            lowerLayer.sprite = frames[frame];
            upperLayer.sprite = frames[nextFrame];
        }
    }

    private void NextFrame()
    {
        frame = nextFrame;
        if (nextFrame == 0)
        {
            direction = 1;
        } else if (nextFrame == frames.Length - 1)
        {
            direction = -1;
        }
        nextFrame += direction;
    }

    private void Update()
    {
        if (paused) return;
        float progress = (Time.time - transitionStart) / transitionTime;

        if (stageA)
        {
            upperLayer.color = new Color(1, 1, 1, progress);
        } else
        {
            lowerLayer.color = new Color(1, 1, 1, 1 - progress);
        }
    }
}
