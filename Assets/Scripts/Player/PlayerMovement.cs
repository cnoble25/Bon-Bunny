using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 MovementInput;

    public static float Timer;

    private float Angle;

    private float ThrustInput;

    [SerializeField] float MaxSpeed;

    [SerializeField] float ThrustSensitivity;

    [SerializeField] float TorqueSensitivity;

    [SerializeField] private float MaxTorque;

    public Animator anim;

    //Controls Gravity
    public float GasVolume;

    private ParticleSystem ps;

    //This variable is a value that ThrustInput is divided by to determine the speed that the character deflates;
    [SerializeField] float DeltaDeflate;

    [SerializeField] float MaxDefaltion;

    private float localx;

    private bool isPlayingParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ps = GetComponent<ParticleSystem>();
        
        ps.Play();

        localx = transform.localScale.x;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        Angle = transform.rotation.eulerAngles.z+90;
        MovementInput = PlayerInputManager.instance.Move.ReadValue<Vector2>();
        ThrustInput = PlayerInputManager.instance.Thrust.ReadValue<float>();
        PlayerContols();
        ShrinkingBalloonControls();
        }

    void PlayerContols()
    {
        Timer += Time.deltaTime;
        // this is how the particle system activates
        if (ThrustInput > 0.1f)
        {
            anim.SetBool("IsMoving", true);
            if (!isPlayingParticles)
            {
                print("ON");
                ps.Play();
                isPlayingParticles = true;
            }
        }
        else
        {
            anim.SetBool("IsMoving", false);
            ps.Stop();
            isPlayingParticles = false;
        }
        // this is how thrust is handled
        rb.AddForce(new Vector2(Mathf.Cos(Angle*Mathf.PI/180), Mathf.Sin(Angle*Mathf.PI/180))*ThrustInput*ThrustSensitivity);
        //this is how torque is handled
        rb.AddTorque(-MovementInput.x*TorqueSensitivity);

        transform.localScale = new Vector3(Mathf.Lerp(localx,localx * 0.9f,ThrustInput), transform.localScale.y, transform.localScale.z);
        
        if(rb.velocity.magnitude > MaxSpeed) rb.velocity = rb.velocity.normalized * MaxSpeed;
        if (Mathf.Abs(rb.angularVelocity) > MaxTorque) rb.angularVelocity = (rb.angularVelocity > 0 ? 1 : -1) * MaxTorque; 
    }

    void ShrinkingBalloonControls()
    {
        rb.gravityScale = -GasVolume;

        GasVolume -= ThrustInput/DeltaDeflate;

        if(GasVolume < MaxDefaltion) GasVolume = MaxDefaltion;
    }

    //handles all of the inputs for the game that aren't movement related or will break in fixed update
    void InputHandler()
    {
        if(PlayerInputManager.instance.Pause.WasPressedThisFrame())
        {
            UIManager.instance.TogglePause();
            if(UIManager.instance.OptionsScreen.activeSelf)
            {
                UIManager.instance.ToggleOptions();
            }
        }
        if(PlayerInputManager.instance.Restart.WasPressedThisFrame())
        {
            UIManager.instance.RestartLevel();
        }
    }

}
