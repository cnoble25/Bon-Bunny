using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloud : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float strikeInterval;
    [SerializeField] private float timeStriking;

    [SerializeField] FMODUnity.EventReference thunderSound;
    private bool isStriking;
    
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStriking)
        {
            StartCoroutine(ActivateLightning());
            isStriking = true;
        }
    }

    IEnumerator ActivateLightning()
    {
        yield return new WaitForSeconds(strikeInterval);
        GetComponent<PolygonCollider2D>().enabled = true;
        sr.enabled = true;
        FMODUnity.RuntimeManager.PlayOneShot(thunderSound, transform.position);
        yield return new WaitForSeconds(timeStriking);
        GetComponent<PolygonCollider2D>().enabled = false;
        sr.enabled = false;
        isStriking = false;


    }
}
