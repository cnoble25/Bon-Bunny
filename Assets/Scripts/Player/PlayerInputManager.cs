using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    public PlayerInput playerInput;

    [SerializeField] GameObject Player;

    [HideInInspector] public InputAction Move, Thrust, Pause, Restart, Submit;
    
    void Awake()
    {
        if(instance == null) 
        {
            instance = this;
            if(instance.playerInput == null && Player != null) 
            {
               instance.playerInput = Player.GetComponent<PlayerInput>();
            }
        }
        
    }
    
    void Start()
    {
        if (instance.playerInput == null && Player != null)
        {
            instance.playerInput = Player.GetComponent<PlayerInput>();
        }
    }
    
    void OnEnable()
    {
        Move = instance.playerInput.actions["Move"];
        Move.Enable();
        Restart = instance.playerInput.actions["Restart"];
        Restart.Enable();
        Thrust = instance.playerInput.actions["Thrust"];
        Thrust.Enable();
        Pause = instance.playerInput.actions["Pause"];
        Pause.Enable();
        Submit = instance.playerInput.actions["Submit"];
        Submit.Enable();
    }
    void OnDisable()
    {
        Move.Disable();
        Thrust.Disable();
        Pause.Disable();
    }
}
