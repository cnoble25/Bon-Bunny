using FMODUnity;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector2 direction;
    
    public float Magnitude;
    public float rotationSpeed = 2;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name[0] == 'P')
        {
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
            }
        }
        rb.velocity = direction * Magnitude;
        rb.rotation = Mathf.Atan2(direction.y, direction.x)*180/Mathf.PI;
    }

    void OnTriggerExit2D(Collider2D other)
    { // fade out sound
        if(other.CompareTag("DeathZone")) {
            if (gameObject.TryGetComponent(out StudioEventEmitter emit)) emit.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Destroy(gameObject);
        }
    }
}
