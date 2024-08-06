using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillOrb : MonoBehaviour
{
    
    [SerializeField] float refillTime;
    
    bool isRefilling = false;

    private ParticleSystem ps;

    private SpriteRenderer sr;

    private Collider2D col;

    [SerializeField] private Sprite Cooldown;

    private Sprite active;

    [SerializeField] private FMODUnity.EventReference refillSound;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        active = sr.sprite;
        col = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isRefilling)
        {
            other.gameObject.TryGetComponent(out PlayerMovement movement);
            if (movement != null)
            {
                movement.GasVolume = 0;
                StartCoroutine(RechargeTime());
            }
        }
    }

    private IEnumerator RechargeTime()
    {
        isRefilling = true;
        ps.Play();
        FMODUnity.RuntimeManager.PlayOneShot(refillSound, transform.position);
        sr.sprite = Cooldown;
        col.enabled = false;
        yield return new WaitForSeconds(refillTime);
        isRefilling = false;
        sr.sprite = active;
        col.enabled = true;

    }
}
