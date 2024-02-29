using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
 //Reference Variables
    PlayerInput PlayerInput;
    CharacterController CharacterController;
 //Variables to store player input
    Vector2 CurrentMovementInput;
    Vector3 CurrentMovement;
    bool isMovementPressed;

 //Awake is called earlier in events
    void Awake()
    {
        PlayerInput = new PlayerInput();
        CharacterController = GetComponent<CharacterController>();  

        PlayerInput.CharacterControls.Move.started += context => {
            CurrentMovementInput = context.ReadValue<Vector2>();
            CurrentMovement.x = CurrentMovementInput.x;
            CurrentMovement.z = CurrentMovementInput.y;
            isMovementPressed = CurrentMovementInput.x !=0 || CurrentMovementInput.y != 0;
        };
        PlayerInput.CharacterControls.Move.canceled += context => {
            CurrentMovementInput = context.ReadValue<Vector2>();
            CurrentMovement.x = CurrentMovementInput.x;
            CurrentMovement.z = CurrentMovementInput.y;
            isMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
        };
    }
    
    // Update is called once per frame
    void Update()
    {
        CharacterController.Move(CurrentMovement * Time.deltaTime);
    }
    void OnEnable()
    {
        PlayerInput.CharacterControls.Enable();
    }
    void OnDisable()
    {
        PlayerInput.CharacterControls.Disable();
    }
}
