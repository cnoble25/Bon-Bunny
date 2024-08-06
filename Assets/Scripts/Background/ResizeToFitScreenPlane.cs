using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToFitScreenPlane : MonoBehaviour
{
    public Vector2 aspectRatio = new(59, 41);

    // Update is called once per frame
    void Update()
    {
        float screenWorldHeight = Camera.main.orthographicSize * 2f;
        float screenWorldWidth = screenWorldHeight / Screen.height * Screen.width;

        float aspectRatio = this.aspectRatio.x / this.aspectRatio.y;
        float baseHeight = 10f, baseWidth = 10f * aspectRatio;

        float maxScale = Mathf.Max(screenWorldWidth / baseWidth, screenWorldHeight / baseHeight);

        gameObject.transform.localScale = new Vector3(maxScale * aspectRatio, maxScale, maxScale);
    }
}
