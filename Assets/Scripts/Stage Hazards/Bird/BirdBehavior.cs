using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    private ContactFilter2D contactFilter = new();

    public float detectionRadius;
    public float detectionTime;
    public Vector2 flightSpeed;
    [Tooltip("How long until the bird reverses direction - set to value < 0 to disable")]
    public float flightTime;

    public Sprite divingSprite;
    public float divingAcceleration;
    public float maxDivingAngularVelocity;

    private bool isDiving = false;
    private float timeSinceReverse;
    private float timeDetected;
    private bool playerSeenLast = false;

    private bool divingControl = true;

    public bool respawnFromOffscreen = false;

    private void Start()
    {
        timeSinceReverse = Time.time;
        GetComponent<SpriteRenderer>().flipX = flightSpeed.x > 0;
        contactFilter.layerMask = 0;
    }

    private void FixedUpdate()
    {
        if (isDiving)
        {
            if(divingControl) DiveTick();
        } else
        {
            FlightTick();
        }
        transform.localPosition += (Vector3)(flightSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    private void FlightTick()
    {
        if (flightTime > 0 && Time.time - timeSinceReverse > flightTime)
        {
            timeSinceReverse = Time.time;
            flightSpeed = -flightSpeed;

            GetComponent<SpriteRenderer>().flipX = flightSpeed.x > 0;
        }

        if (PlayerSeen())
        {
            if (!playerSeenLast)
            {
                playerSeenLast = true;
                timeDetected = Time.time;
            } else if (Time.time - timeDetected > detectionTime)
            {
                isDiving = true;
                GetComponent<SpriteRenderer>().sprite = divingSprite;
                GetComponent<Animator>().enabled = false;
            }
        } else
        {
            playerSeenLast = false;
        }
    }

    private void DiveTick()
    {
        Vector2 offset = PlayerStateManager.instance.player.transform.position - transform.position;
        offset.Normalize();

        float targetRotation = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        var angles = transform.eulerAngles;
        if (flightSpeed.x <= 0) targetRotation += 180;
        float difference = Mathf.DeltaAngle(targetRotation, angles.z);

        if (difference < 90 && difference > -90)
        {
            if (Mathf.Abs(difference) > 0.01f)
            {
                var max = maxDivingAngularVelocity * Time.fixedDeltaTime;
                angles.z -= Mathf.Clamp(difference, -max, max);
                transform.eulerAngles = angles;

                offset = transform.right;
                if (flightSpeed.x <= 0) offset = -offset;
            }

            var speed = flightSpeed.magnitude;
            speed += Time.fixedDeltaTime * divingAcceleration;
            flightSpeed = offset * speed;
        } else
        {
            divingControl = false;
        }
    }

    private void OnBecameInvisible()
    {
        if (!isDiving && respawnFromOffscreen) { // Doesn't work for going off the top or bottom of the screen.
            if (flightTime < 0)
            {
                var pos = transform.position;
                pos.x = -pos.x;
                transform.position = pos;
            }
        } else
        {
            Destroy(gameObject);
        }
    }

    private bool PlayerSeen()
    {
        Vector2 offset = PlayerStateManager.instance.player.transform.position - transform.position;
        float distance = offset.magnitude;
        if (distance > detectionRadius) return false;
        Vector2 direction = offset.normalized;

        if (offset.y > 0.2f) return false;

        if (flightSpeed.x > 0)
        {
            if (direction.x < 0.3f) return false;
        } else
        {
            if (direction.x > -0.3f) return false;
        }

        var result = Physics2D.Raycast(transform.position, direction, offset.magnitude, 1);

        return result.collider != null && result.collider.gameObject == PlayerStateManager.instance.player;
    }
}
