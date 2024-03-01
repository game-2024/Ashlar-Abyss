using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Gets refernce to player object where said object has a Speed variable
    [SerializeField] PlayerStatsScript player;

    //Reference to Lantern object for player to be able to toggle lantern
    [SerializeField] private LanternScript lantern;

    //Player and Diesel Variables for changing variables
    //FOR TESTING PURPOSES ONLY
    [SerializeField] private PlayerHealthManagerScript healthManager;
    [SerializeField] private DieselManagerScript dieselManager;

    //InputActionMap
    [SerializeField] private PlayerInput playerInputReciever;
    private InputAction playerInputMove;


    //Camera Transform
    [SerializeField] CinemachineVirtualCamera playerPOVCam;
    private Transform cameraTransform;

    //Private Speed variable gotten from PlayerStatsScript to allow player movement
    private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        playerInputMove = playerInputReciever.actions["WASDMovement"];
        cameraTransform = playerPOVCam.transform;
    }

    void Update()
    {
        #region Temp Movement

        float forwardMovement = playerInputMove.ReadValue<Vector2>().y;
        float rightMovement = playerInputMove.ReadValue<Vector2>().x;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camForward.Normalize();

        camRight.y = 0f;
        camRight.Normalize();

        Vector3 forwardRelative = camForward * forwardMovement;
        Vector3 rightRelative = camRight * rightMovement;

        Vector3 finalMovement = forwardRelative + rightRelative;

        player.Move(finalMovement);

        Vector3 adjustedRotation = cameraTransform.eulerAngles;
        adjustedRotation.x = 0f;

        player.transform.eulerAngles = adjustedRotation;



        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    player.transform.Translate(new Vector3(0, 0, 1) * Speed * Time.deltaTime, Space.World);
        //    player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    player.transform.Translate(new Vector3(0, 0, -1) * Speed * Time.deltaTime, Space.World);
        //    player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    //Move the Rigidbody right constantly at the speed you define (the blue arrow axis in Scene view)  
        //    player.transform.Translate(new Vector3(1, 0, 0) * Speed * Time.deltaTime, Space.World);
        //    player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    //Move the Rigidbody left constantly at the speed you define (the blue arrow axis in Scene view)
        //    player.transform.Translate(new Vector3(-1, 0, 0) * Speed * Time.deltaTime, Space.World);
        //    player.transform.rotation = Quaternion.Euler(0f, 270, 0f);
        //}
        #endregion

        #region Toggle Lantern ON or OFF
        if (Input.GetKeyDown(KeyCode.J))
        {
            lantern.ToggleLantern();
        }
        #endregion

        #region Test Health Adjustments Manually

        //Heal Player
        if (Input.GetKeyDown(KeyCode.Y))
        {
            healthManager.HealPlayer();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            healthManager.DamagePlayer(healthManager.damage);
        }

        #endregion

        #region Restore Diesel Fuel Manually

            if (Input.GetKeyDown(KeyCode.H))
            {
                dieselManager.CurrentDiesel = dieselManager.MaxDiesel;
            }

        #endregion


    }

}

