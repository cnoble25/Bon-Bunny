using UnityEngine;

public class PlayerIdleAnimationTrigger : MonoBehaviour
{
    private float delay;
    private Rigidbody2D body;
    private Animator animator;
    private bool delaying = false;

    public float stillnessTimer;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (body.velocity.sqrMagnitude > 0.01f)
        {
            delaying = false;
            animator.enabled = false;
        } else
        {
            if (!delaying)
            {
                delay = Time.time;
                delaying = true;
            }
            else if (Time.time - delay > stillnessTimer) {
                animator.enabled = true;
            }
        }
    }
}
