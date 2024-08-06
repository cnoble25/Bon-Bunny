using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference WinSound;
    void OnEnable()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        foreach (CapsuleCollider2D cap in GetComponents<CapsuleCollider2D>())
        {
            cap.enabled = false;
        }
        FMODUnity.RuntimeManager.PlayOneShot(WinSound, transform.position);
        rb.gravityScale = 0;
        rb.drag = 0f;
        GetComponent<PlayerMovement>().enabled = false;
        rb.freezeRotation = true;
        UIManager.instance.ActivateWin();
    }
}
