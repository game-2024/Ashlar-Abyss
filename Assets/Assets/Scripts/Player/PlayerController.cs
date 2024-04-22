using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    Transform PlayerCamTransformReference;
    [SerializeField] private CharacterController PlayerCharacterController;

    [Space]
    [Header("Player Equipment")]
    [SerializeField] LanternScript Lantern;
    [SerializeField] PlayerWeaponScript PlayerWeapon;
    [SerializeField] DieselManagerScript DieselManager;
    public DieselManagerScript DieselManagerProperty
    {
        get { return DieselManager; }
    }


    [Space]
    [Header("Player Movement Feel Fields")]
    [SerializeField] private float SmoothingTime;
    [SerializeField] private float Speed;
    [SerializeField] private float Gravity = -9.18f;
    [SerializeField] private float TerminalVelocity = -50f;
    private float TargetRotation;
    private float _rotationVelocity;

    //Events
    public UnityAction onInteractPressed;

    Vector3 InputAxis;
    Vector3 Velocity = Vector3.zero;

    enum PlayerDieselState
    {
        None = 0,
        Lantern = 1,
        Sword = 2
    };


    PlayerDieselState dieselState = PlayerDieselState.None;



    private void Start()
    {
        PlayerCamTransformReference = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void Update()
    {

        InputAxis = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));


        if(Input.GetButtonDown("Interact"))
        {
            onInteractPressed?.Invoke();
        }


        //Player Movement and Rotations
        HandleMovement();
        HandlePlayerRotation();


        if(DieselManager.CurrentDiesel <= 0)
        {
            dieselState= PlayerDieselState.None;
            Lantern.TurnOffLantern();
            PlayerWeapon.TurnOffSword();
        }
        else
        {
            DieselSwitchState();
        }


    }



    private void DieselSwitchState()
    {
        switch (dieselState)
        {

            //Case if Player isnt using diesel equipment. Look for player input to change state
            case PlayerDieselState.None:

                #region Case None Logic
                if (Input.GetKeyDown(KeyCode.F))
                {
                    dieselState = PlayerDieselState.Lantern;
                    Lantern.ToggleLantern();
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    dieselState = PlayerDieselState.Sword;
                    PlayerWeapon.ToggleWeaponHeated();
                }

                Debug.Log("In None State");
                #endregion

                break;

            //Case if Player is using Lantern. Look for player input to change exit Lantern state
            case PlayerDieselState.Lantern:

                #region Case Lantern Logic
                if (Input.GetKeyDown(KeyCode.F))
                {
                    dieselState = PlayerDieselState.None;
                    Lantern.ToggleLantern();
                }

                Debug.Log(" In Lantern State");
                #endregion

                break;

            //Case if Player is using Sword. Look for player input to change exit Sword state
            case PlayerDieselState.Sword:

                #region Case Sword Logic
                if (Input.GetKeyDown(KeyCode.G))
                {
                    dieselState = PlayerDieselState.None;
                    PlayerWeapon.ToggleWeaponHeated();
                }

                Debug.Log("In Sword State");
                #endregion

                break;

            default:
                dieselState = PlayerDieselState.None;
                break;

        }
    }


    #region Player Movement And Rotation Functions

    private void HandleMovement()
    {
        //Condition if We want player to not be able to move after falling
        ////if (PlayerCharacterController.isGrounded == true)

        #region X and Z Movement
        Vector3 forwardCamRelativeMovement = PlayerCamTransformReference.forward;
        Vector3 horizontalCamRelativeMovement = PlayerCamTransformReference.right;

        forwardCamRelativeMovement.y = 0f;
        horizontalCamRelativeMovement.y = 0f;

        forwardCamRelativeMovement.Normalize();
        horizontalCamRelativeMovement.Normalize();


        Vector3 finalInputCamRelativeMove = forwardCamRelativeMovement * InputAxis.z + horizontalCamRelativeMovement * InputAxis.x;
        finalInputCamRelativeMove.y = 0f;
        finalInputCamRelativeMove.Normalize();
        #endregion
        //X and Z Movement Move Call on Controller
        PlayerCharacterController.Move(Speed * Time.deltaTime * finalInputCamRelativeMove);

        #region Gravity/Y Velocity Movement
        if (PlayerCharacterController.isGrounded && Velocity.y <= 0)
        {
            Velocity.y = -0.5f;
        }
        Velocity.y = Velocity.y >= TerminalVelocity ? Gravity * Time.deltaTime + Velocity.y : TerminalVelocity;
        #endregion
        //Gravity/Y Velocity Move Call on Controller
        PlayerCharacterController.Move(Velocity * Time.deltaTime);

    }

    private void HandlePlayerRotation()
    {
        if (InputAxis != Vector3.zero)
        {

            TargetRotation = Mathf.Atan2(InputAxis.x, InputAxis.z) * Mathf.Rad2Deg + PlayerCamTransformReference.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(PlayerCharacterController.transform.eulerAngles.y, TargetRotation,
                                                    ref _rotationVelocity, SmoothingTime);

            PlayerCharacterController.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }
    }

    #endregion


   

}
