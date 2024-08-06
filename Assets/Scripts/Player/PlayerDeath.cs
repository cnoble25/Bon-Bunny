using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] float TimeToDie;

    [SerializeField] FMODUnity.EventReference DeathSound;
    void OnEnable()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        foreach (CapsuleCollider2D cap in GetComponents<CapsuleCollider2D>())
        {
            cap.enabled = false;
        }
        FMODUnity.RuntimeManager.PlayOneShot(DeathSound, transform.position);
        rb.isKinematic = true;
        rb.freezeRotation = true;
    }


    void Death()
    {
        UIManager.instance.ActivateDeath();
    }
}
