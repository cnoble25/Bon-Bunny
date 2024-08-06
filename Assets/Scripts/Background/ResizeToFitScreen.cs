using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToFitScreen : MonoBehaviour
{
    private SpriteRenderer objRenderer;

    [Tooltip("Locks edges of image to screen edge: set to 1 to lock bottom/left, set to -1 to lock top/right")]
    public Vector2 sideLock = Vector2.zero;

    private void Awake()
    {
        objRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Adapted from https://discussions.unity.com/t/scaling-my-background-sprite-to-fill-screen-2d/93264/2

        float screenWorldHeight = Camera.main.orthographicSize * 2f;
        float screenWorldWidth = screenWorldHeight / Screen.height * Screen.width;

        float baseHeight = objRenderer.sprite.bounds.size.y, baseWidth = objRenderer.sprite.bounds.size.x;

        float scaleX = screenWorldWidth / baseWidth;
        float scaleY = screenWorldHeight / baseHeight;
        float maxScale;

        Vector2 position = gameObject.transform.position;
        if (scaleX > scaleY)
        {
            maxScale = scaleX;
            
            if(sideLock.y != 0) position.y = sideLock.y * (baseHeight * scaleX - screenWorldHeight) / 2;
        } else
        {
            maxScale = scaleY;

            if(sideLock.x != 0) position.x = sideLock.x * (baseWidth * scaleY - screenWorldWidth) / 2;
        }

        gameObject.transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        gameObject.transform.position = position;
    }
}
